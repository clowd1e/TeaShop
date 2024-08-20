using MediatR;
using TeaShop.Application.DTOs.Order.Response;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.Order.Query.GetCustomerOrders
{
    public sealed record GetCustomerOrdersQuery(
        Guid? Id) : IRequest<Result<IEnumerable<OrderResponseDto>>>;
}
