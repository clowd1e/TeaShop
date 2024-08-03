using AutoMapper;
using MediatR;
using TeaShop.Application.DTOs.Tea.Response;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Domain.Repository;

namespace TeaShop.Application.Service.Tea.Query.GetAllTea
{
    public sealed class GetAllTeaQueryHandler : IRequestHandler<GetAllTeaQuery, Result<IEnumerable<TeaResponseDto>>>
    {
        private readonly ITeaRepository _teaRepository;
        private readonly IMapper _mapper;

        public GetAllTeaQueryHandler(
            ITeaRepository teaRepository,
            IMapper mapper)
        {
            _teaRepository = teaRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<TeaResponseDto>>> Handle(GetAllTeaQuery request, CancellationToken cancellationToken)
        {
            var tea = await _teaRepository.GetAllAsync();

            var teaMap = _mapper.Map<IEnumerable<TeaResponseDto>>(tea);

            return tea is null
                ? TeaErrors.TeaNotFound
                : teaMap.ToResult();
        }
    }
}
