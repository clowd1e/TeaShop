using MediatR;
using TeaShop.Application.DTOs.TeaType.Response;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.TeaType.Query.GetTeaTypeById
{
    public sealed record GetTeaTypeByIdQuery(Guid Id) : IRequest<Result<TeaTypeResponseDto>>;
}
