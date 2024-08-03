using AutoMapper;
using MediatR;
using TeaShop.Application.DTOs.Tea.Response;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Domain.Repository;

namespace TeaShop.Application.Service.TeaType.Query.GetTeaByTeaTypeAdmin
{
    public sealed class GetTeaByTeaTypeAdminQueryHandler : IRequestHandler<GetTeaByTeaTypeAdminQuery, Result
        <IEnumerable<TeaAdminResponseDto>>>
    {
        private readonly ITeaTypeRepository _teaTypeRepository;
        private readonly IMapper _mapper;

        public GetTeaByTeaTypeAdminQueryHandler(
            ITeaTypeRepository teaTypeRepository,
            IMapper mapper)
        {
            _teaTypeRepository = teaTypeRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<TeaAdminResponseDto>>> Handle(GetTeaByTeaTypeAdminQuery request, CancellationToken cancellationToken)
        {
            var tea = await _teaTypeRepository.GetTeaByTeaTypeAsync(request.TeaTypeId);

            var teaMap = _mapper.Map<IEnumerable<TeaAdminResponseDto>>(tea);

            return tea is null
                ? TeaTypeErrors.TeaTypeNotFound
                : teaMap.ToResult();
        }
    }
}
