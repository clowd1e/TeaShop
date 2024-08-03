using MediatR;
using TeaShop.Application.DTOs.Tea.Response;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.Tea.Query.GetTeaById
{
    public sealed record GetTeaByIdQuery(Guid Id) : IRequest<Result<TeaResponseDto>>;
}
