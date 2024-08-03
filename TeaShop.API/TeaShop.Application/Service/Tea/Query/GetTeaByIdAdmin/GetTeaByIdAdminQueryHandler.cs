using AutoMapper;
using MediatR;
using TeaShop.Application.DTOs.Tea.Response;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Domain.Repository;

namespace TeaShop.Application.Service.Tea.Query.GetTeaByIdAdmin
{
    public sealed class GetTeaByIdAdminQueryHandler : IRequestHandler<GetTeaByIdAdminQuery, Result<TeaAdminResponseDto>>
    {
        private readonly ITeaRepository _teaRepository;
        private readonly IMapper _mapper;

        public GetTeaByIdAdminQueryHandler(
            ITeaRepository teaRepository,
            IMapper mapper)
        {
            _teaRepository = teaRepository;
            _mapper = mapper;
        }

        public async Task<Result<TeaAdminResponseDto>> Handle(GetTeaByIdAdminQuery request, CancellationToken cancellationToken)
        {
            var tea = await _teaRepository.GetByIdAsync(request.Id);

            var teaMap = _mapper.Map<TeaAdminResponseDto>(tea);

            return tea is null
                ? TeaErrors.TeaNotFound
                : teaMap.ToResult();
        }
    }
}
