using MediatR;
using TeaShop.Application.DTOs.Tea.Response;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.TeaType.Query.GetTeaByTeaTypeAdmin
{
    public sealed record GetTeaByTeaTypeAdminQuery(
        Guid TeaTypeId) : IRequest<Result<IEnumerable<TeaAdminResponseDto>>>;
}
