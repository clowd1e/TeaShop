using MediatR;
using TeaShop.Application.DTOs.Tea.Request.Update;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.Tea.Command.UpdateTea
{
    public sealed record UpdateTeaCommand(
        Guid? Id,
        UpdateTeaRequestDto? Tea) : IRequest<Result>;
}
