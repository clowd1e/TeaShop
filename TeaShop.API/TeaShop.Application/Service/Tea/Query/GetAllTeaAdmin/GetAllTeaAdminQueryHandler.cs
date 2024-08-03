using AutoMapper;
using MediatR;
using TeaShop.Application.DTOs.Tea.Response;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Domain.Repository;

namespace TeaShop.Application.Service.Tea.Query.GetAllTeaAdmin
{
    public sealed class GetAllTeaAdminQueryHandler : IRequestHandler<GetAllTeaAdminQuery, Result
        <IEnumerable<TeaAdminResponseDto>>>
    {
        private readonly ITeaRepository _teaRepository;
        private readonly IMapper _mapper;

        public GetAllTeaAdminQueryHandler(
            ITeaRepository teaRepository,
            IMapper mapper)
        {
            _teaRepository = teaRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<TeaAdminResponseDto>>> Handle(GetAllTeaAdminQuery request, CancellationToken cancellationToken)
        {
            var tea = await _teaRepository.GetAllAsync();

            var teaMap = _mapper.Map<IEnumerable<TeaAdminResponseDto>>(tea);

            return tea is null
                ? TeaErrors.TeaNotFound
                : teaMap.ToResult();
        }
    }
}
