using MediatR;
using TeaShop.Application.DTOs.TeaType.Response;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.TeaType.Query.GetAllTeaTypes
{
    public sealed record GetAllTeaTypesQuery() : IRequest<Result
        <IEnumerable<TeaTypeResponseDto>>>;
}
