using MediatR;
using TeaShop.Application.DTOs.Tea.Response;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.Tea.Query.GetAllTeaAdmin
{
    public sealed record GetAllTeaAdminQuery() : IRequest<Result
        <IEnumerable<TeaAdminResponseDto>>>;
}
