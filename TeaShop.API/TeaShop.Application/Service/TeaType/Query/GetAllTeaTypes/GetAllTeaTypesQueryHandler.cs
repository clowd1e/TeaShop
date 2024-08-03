using AutoMapper;
using MediatR;
using TeaShop.Application.DTOs.TeaType.Response;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Domain.Repository;

namespace TeaShop.Application.Service.TeaType.Query.GetAllTeaTypes
{
    public sealed class GetAllTeaTypesQueryHandler : IRequestHandler<GetAllTeaTypesQuery, Result<IEnumerable<TeaTypeResponseDto>>>
    {
        private readonly ITeaTypeRepository _teaTypeRepository;
        private readonly IMapper _mapper;

        public GetAllTeaTypesQueryHandler(
            ITeaTypeRepository teaTypeRepository,
            IMapper mapper)
        {
            _teaTypeRepository = teaTypeRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<TeaTypeResponseDto>>> Handle(GetAllTeaTypesQuery request, CancellationToken cancellationToken)
        {
            var teaTypes = await _teaTypeRepository.GetAllAsync();

            var teaTypesMap = _mapper.Map<IEnumerable<TeaTypeResponseDto>>(teaTypes);

            return teaTypes is null
                ? TeaTypeErrors.TeaTypeNotFound
                : teaTypesMap.ToResult();
        }
    }
}
