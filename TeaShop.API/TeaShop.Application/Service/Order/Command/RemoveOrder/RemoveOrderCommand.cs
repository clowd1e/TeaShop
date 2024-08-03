using MediatR;
using TeaShop.Application.DTOs.Order.Request.Remove;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.Order.Command.RemoveOrder
{
    public sealed record RemoveOrderCommand(
        RemoveOrderRequestDto? Order) : IRequest<Result>;
}
