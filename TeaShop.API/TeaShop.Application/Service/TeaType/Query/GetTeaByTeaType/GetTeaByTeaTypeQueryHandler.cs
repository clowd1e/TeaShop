using AutoMapper;
using MediatR;
using TeaShop.Application.DTOs.Tea.Response;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Domain.Repository;

namespace TeaShop.Application.Service.TeaType.Query.GetTeaByTeaType
{
    public sealed class GetTeaByTeaTypeQueryHandler : IRequestHandler<GetTeaByTeaTypeQuery, Result
        <IEnumerable<TeaResponseDto>>>
    {
        private readonly ITeaTypeRepository _teaTypeRepository;
        private readonly IMapper _mapper;

        public GetTeaByTeaTypeQueryHandler(
            ITeaTypeRepository teaTypeRepository,
            IMapper mapper)
        {
            _teaTypeRepository = teaTypeRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<TeaResponseDto>>> Handle(GetTeaByTeaTypeQuery request, CancellationToken cancellationToken)
        {
            var teaType = await _teaTypeRepository.GetByIdAsync(request.TeaTypeId);
            if (teaType is null)
                return TeaTypeErrors.TeaTypeNotFound;

            var tea = await _teaTypeRepository.GetTeaByTeaTypeAsync(request.TeaTypeId);

            var teaMap = _mapper.Map<IEnumerable<TeaResponseDto>>(tea);

            return tea is null
                ? TeaTypeErrors.TeaByTeaTypeNotFound
                : teaMap.ToResult();
        }
    }
}
