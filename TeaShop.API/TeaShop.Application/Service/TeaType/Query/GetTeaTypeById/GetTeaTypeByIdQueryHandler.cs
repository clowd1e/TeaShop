using AutoMapper;
using MediatR;
using TeaShop.Application.DTOs.TeaType.Response;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Domain.Repository;

namespace TeaShop.Application.Service.TeaType.Query.GetTeaTypeById
{
    public sealed class GetTeaTypeByIdQueryHandler : IRequestHandler<GetTeaTypeByIdQuery, Result<TeaTypeResponseDto>>
    {
        private readonly ITeaTypeRepository _teaTypeRepository;
        private readonly IMapper _mapper;

        public GetTeaTypeByIdQueryHandler(
            ITeaTypeRepository teaTypeRepository,
            IMapper mapper)
        {
            _teaTypeRepository = teaTypeRepository;
            _mapper = mapper;
        }

        public async Task<Result<TeaTypeResponseDto>> Handle(GetTeaTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var teaType = await _teaTypeRepository.GetByIdAsync(request.Id);

            var teaTypeMap = _mapper.Map<TeaTypeResponseDto>(teaType);

            return teaType is null
                ? TeaTypeErrors.TeaTypeNotFound
                : teaTypeMap.ToResult();
        }
    }
}
