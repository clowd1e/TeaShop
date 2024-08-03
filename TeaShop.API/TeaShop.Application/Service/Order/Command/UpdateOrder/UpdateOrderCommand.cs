using MediatR;
using TeaShop.Application.DTOs.Order.Request.Update;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.Order.Command.UpdateOrder
{
    public sealed record UpdateOrderCommand(
        Guid? Id,
        UpdateOrderRequestDto? Order) : IRequest<Result>;
}
