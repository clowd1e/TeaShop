using MediatR;
using TeaShop.Application.DTOs.TeaType.Response;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.TeaType.Query.GetTeaTypeByIdAdmin
{
    public sealed record GetTeaTypeByIdAdminQuery(Guid Id) : IRequest<Result<TeaTypeAdminResponseDto>>;
}
