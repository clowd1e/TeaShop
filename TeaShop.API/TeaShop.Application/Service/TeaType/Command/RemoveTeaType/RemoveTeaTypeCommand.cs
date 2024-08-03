using MediatR;
using TeaShop.Application.DTOs.TeaType.Request.Remove;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.TeaType.Command.RemoveTeaType
{
    public sealed record RemoveTeaTypeCommand(
        RemoveTeaTypeRequestDto? TeaType) : IRequest<Result>;
}
