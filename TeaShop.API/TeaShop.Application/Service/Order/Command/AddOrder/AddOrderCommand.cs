using MediatR;
using TeaShop.Application.DTOs.Order.Request.Add;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.Order.Command.AddOrder
{
    public sealed record AddOrderCommand(
        AddOrderRequestDto? Order) : IRequest<Result>;
}
