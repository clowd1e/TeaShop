using MediatR;
using TeaShop.Application.DTOs.Order.Response;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.Order.Query.GetAllOrders
{
    public sealed record GetAllOrdersQuery() : IRequest<Result
        <IEnumerable<OrderResponseDto>>>;
}
