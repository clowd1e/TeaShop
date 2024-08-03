using MediatR;
using TeaShop.Application.DTOs.Order.Response;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.Order.Query.GetOrderById
{
    public sealed record GetOrderByIdQuery(
        Guid? Id) : IRequest<Result<OrderResponseDto>>;
}
