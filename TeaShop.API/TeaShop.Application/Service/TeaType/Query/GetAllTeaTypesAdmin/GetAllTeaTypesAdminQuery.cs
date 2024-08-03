using MediatR;
using TeaShop.Application.DTOs.TeaType.Response;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.TeaType.Query.GetAllTeaTypesAdmin
{
    public sealed record GetAllTeaTypesAdminQuery() : IRequest<Result
        <IEnumerable<TeaTypeAdminResponseDto>>>;
}
