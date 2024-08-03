using MediatR;
using TeaShop.Application.DTOs.Tea.Request.Remove;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.Tea.Command.RemoveTea
{
    public sealed record RemoveTeaCommand(
        RemoveTeaRequestDto? Tea) : IRequest<Result>;
}
