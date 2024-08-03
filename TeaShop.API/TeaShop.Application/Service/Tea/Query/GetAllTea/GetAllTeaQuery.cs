using MediatR;
using TeaShop.Application.DTOs.Tea.Response;
using TeaShop.Application.ResultBehavior;

namespace TeaShop.Application.Service.Tea.Query.GetAllTea
{
    public sealed record GetAllTeaQuery() : IRequest<Result
        <IEnumerable<TeaResponseDto>>>;
}
