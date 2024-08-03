using MediatR;
using TeaShop.Application.DTOs.Customer.Response;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.Customer.Query.GetAllCustomers
{
    public sealed record GetAllCustomersQuery() : IRequest<Result
        <IEnumerable<CustomerResponseDto>>>;
}
