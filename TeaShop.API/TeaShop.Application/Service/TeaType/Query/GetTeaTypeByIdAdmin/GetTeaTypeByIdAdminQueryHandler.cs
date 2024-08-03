using AutoMapper;
using MediatR;
using TeaShop.Application.DTOs.TeaType.Response;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Domain.Repository;

namespace TeaShop.Application.Service.TeaType.Query.GetTeaTypeByIdAdmin
{
    public sealed class GetTeaTypeByIdAdminQueryHandler : IRequestHandler<GetTeaTypeByIdAdminQuery, Result<TeaTypeAdminResponseDto>>
    {
        private readonly ITeaTypeRepository _teaTypeRepository;
        private readonly IMapper _mapper;

        public GetTeaTypeByIdAdminQueryHandler(
            ITeaTypeRepository teaTypeRepository,
            IMapper mapper)
        {
            _teaTypeRepository = teaTypeRepository;
            _mapper = mapper;
        }

        public async Task<Result<TeaTypeAdminResponseDto>> Handle(GetTeaTypeByIdAdminQuery request, CancellationToken cancellationToken)
        {
            var teaType = await _teaTypeRepository.GetByIdAsync(request.Id);

            var teaTypeMap = _mapper.Map<TeaTypeAdminResponseDto>(teaType);

            return teaType is null
                ? TeaTypeErrors.TeaTypeNotFound
                : teaTypeMap.ToResult();
        }
    }
}
