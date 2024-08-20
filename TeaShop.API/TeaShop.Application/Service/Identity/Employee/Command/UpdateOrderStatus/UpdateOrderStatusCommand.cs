using MediatR;
using TeaShop.Application.DTOs.Order.Request.UpdateStatus;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.Identity.Employee.Command.UpdateOrderStatus
{
    public sealed record UpdateOrderStatusCommand(
        Guid? Id,
        UpdateOrderStatusRequestDto? Order) : IRequest<Result>;
}
