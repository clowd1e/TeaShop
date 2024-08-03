using AutoMapper;
using MediatR;
using TeaShop.Application.Data;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Domain.Repository;
using Entities = TeaShop.Domain.Entities;

namespace TeaShop.Application.Service.Order.Command.UpdateOrder
{
    public sealed class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Result>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOrderCommandHandler(
            IOrderRepository orderRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var validation = new UpdateOrderCommandValidator().Validate(request);
            if (!validation.IsValid)
                return Result.Fail(validation);

            var oldOrder = await _orderRepository.GetByIdAsync(request.Id);

            if (oldOrder is null)
                return OrderErrors.OrderNotFound;

            var updatedOrder = _mapper.Map<Entities.Order>(request.Order);

            #region Update properties
            updatedOrder.Id = (Guid)request.Id!;
            #endregion

            await _orderRepository.UpdateAsync(oldOrder, updatedOrder);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
