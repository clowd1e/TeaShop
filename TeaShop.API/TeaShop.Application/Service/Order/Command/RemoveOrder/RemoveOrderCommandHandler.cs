using MediatR;
using TeaShop.Application.Data;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Domain.Repository;

namespace TeaShop.Application.Service.Order.Command.RemoveOrder
{
    public sealed class RemoveOrderCommandHandler : IRequestHandler<RemoveOrderCommand, Result>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveOrderCommandHandler(
            IOrderRepository orderRepository,
            IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(RemoveOrderCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new RemoveOrderCommandValidator().ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Result.Fail(validationResult);

            var order = await _orderRepository.GetByIdAsync(request.Order!.Id);

            if (order is null)
                return OrderErrors.OrderNotFound;

            await _orderRepository.DeleteAsync(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }

    }
}
