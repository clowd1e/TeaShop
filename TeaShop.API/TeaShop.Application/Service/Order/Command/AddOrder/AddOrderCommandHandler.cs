using AutoMapper;
using MediatR;
using TeaShop.Application.Data;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Domain.Repository;
using Entities = TeaShop.Domain.Entities;

namespace TeaShop.Application.Service.Order.Command.AddOrder
{
    public sealed class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, Result>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ITeaRepository _teaRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AddOrderCommandHandler(
            IOrderRepository orderRepository,
            ITeaRepository teaRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _teaRepository = teaRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var validation = new AddOrderCommandValidator().Validate(request);
            if (!validation.IsValid)
                return Result.Fail(validation);

            var order = _mapper.Map<Entities.Order>(request.Order);

            foreach (var item in order.Details.Items)
            {
                var tea = await _teaRepository.GetByIdAsync(item.TeaId);

                if (tea is null)
                    return TeaErrors.TeaNotFound;

                item.Tea = tea;
            }

            await _orderRepository.AddAsync(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
