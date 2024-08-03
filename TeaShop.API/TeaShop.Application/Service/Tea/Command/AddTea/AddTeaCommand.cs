using MediatR;
using TeaShop.Application.DTOs.Tea.Request.Add;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.Tea.Command.AddTea
{
    public sealed record AddTeaCommand(
        AddTeaRequestDto? Tea) : IRequest<Result>;
}
