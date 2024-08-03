using MediatR;
using TeaShop.Application.DTOs.TeaType.Request.Add;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.TeaType.Command.AddTeaType
{
    public sealed record AddTeaTypeCommand(
        AddTeaTypeRequestDto? TeaType) : IRequest<Result>;
}
