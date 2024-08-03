using MediatR;
using TeaShop.Application.DTOs.TeaType.Request.Update;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.TeaType.Command.UpdateTeaType
{
    public sealed record UpdateTeaTypeCommand(
        Guid? Id,
        UpdateTeaTypeRequestDto? TeaType) : IRequest<Result>;
}
