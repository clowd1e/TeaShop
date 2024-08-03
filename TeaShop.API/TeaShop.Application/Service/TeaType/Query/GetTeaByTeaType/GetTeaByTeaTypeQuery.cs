using MediatR;
using TeaShop.Application.DTOs.Tea.Response;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.TeaType.Query.GetTeaByTeaType
{
    public sealed record GetTeaByTeaTypeQuery(Guid TeaTypeId) : IRequest<Result
        <IEnumerable<TeaResponseDto>>>;
}
