using AutoMapper;
using MediatR;
using TeaShop.Application.DTOs.TeaType.Response;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Domain.Repository;

namespace TeaShop.Application.Service.TeaType.Query.GetAllTeaTypesAdmin
{
    public sealed class GetAllTeaTypesAdminQueryHandler : IRequestHandler<GetAllTeaTypesAdminQuery, Result
        <IEnumerable<TeaTypeAdminResponseDto>>>
    {
        private readonly ITeaTypeRepository _teaTypeRepository;
        private readonly IMapper _mapper;

        public GetAllTeaTypesAdminQueryHandler(
            ITeaTypeRepository teaTypeRepository,
            IMapper mapper)
        {
            _teaTypeRepository = teaTypeRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<TeaTypeAdminResponseDto>>> Handle(GetAllTeaTypesAdminQuery request, CancellationToken cancellationToken)
        {
            var teaTypes = await _teaTypeRepository.GetAllAsync();

            var teaTypesMap = _mapper.Map<IEnumerable<TeaTypeAdminResponseDto>>(teaTypes);

            return teaTypes is null
                ? TeaTypeErrors.TeaTypeNotFound
                : teaTypesMap.ToResult();
        }
    }
}
