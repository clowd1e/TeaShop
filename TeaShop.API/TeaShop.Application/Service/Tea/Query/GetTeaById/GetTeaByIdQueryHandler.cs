using AutoMapper;
using MediatR;
using TeaShop.Application.DTOs.Tea.Response;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Domain.Repository;

namespace TeaShop.Application.Service.Tea.Query.GetTeaById
{
    public sealed class GetTeaByIdQueryHandler : IRequestHandler<GetTeaByIdQuery, Result<TeaResponseDto>>
    {
        private readonly ITeaRepository _teaRepository;
        private readonly IMapper _mapper;

        public GetTeaByIdQueryHandler(
            ITeaRepository teaRepository,
            IMapper mapper)
        {
            _teaRepository = teaRepository;
            _mapper = mapper;
        }

        public async Task<Result<TeaResponseDto>> Handle(GetTeaByIdQuery request, CancellationToken cancellationToken)
        {
            var tea = await _teaRepository.GetByIdAsync(request.Id);

            var teaMap = _mapper.Map<TeaResponseDto>(tea);

            return tea is null
                ? TeaErrors.TeaNotFound
                : teaMap.ToResult();
        }
    }
}
