using MediatR;
using TeaShop.Application.DTOs.Customer.Request.Add;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.Customer.Command.AddCustomer
{
    public sealed record AddCustomerCommand(
        AddCustomerRequestDto? Customer) : IRequest<Result>;
}
