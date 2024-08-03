using MediatR;
using TeaShop.Application.DTOs.Customer.Response;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.Customer.Query.GetCustomerById
{
    public sealed record GetCustomerByIdQuery(
        Guid Id) : IRequest<Result<CustomerResponseDto>>;
}
