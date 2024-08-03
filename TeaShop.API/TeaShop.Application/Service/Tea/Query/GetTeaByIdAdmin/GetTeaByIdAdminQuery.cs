using MediatR;
using TeaShop.Application.DTOs.Tea.Response;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.Tea.Query.GetTeaByIdAdmin
{
    public sealed record GetTeaByIdAdminQuery(Guid Id) : IRequest<Result<TeaAdminResponseDto>>;
}
