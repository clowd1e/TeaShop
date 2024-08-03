using AutoMapper;
using MediatR;
using TeaShop.Application.Data;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Domain.Repository;
using Entities = TeaShop.Domain.Entities;

namespace TeaShop.Application.Service.TeaType.Command.UpdateTeaType
{
    public sealed class UpdateTeaTypeCommandHandler : IRequestHandler<UpdateTeaTypeCommand, Result>
    {
        private readonly ITeaTypeRepository _teaTypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTeaTypeCommandHandler(
            ITeaTypeRepository teaTypeRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _teaTypeRepository = teaTypeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateTeaTypeCommand request, CancellationToken cancellationToken)
        {
            var validation = new UpdateTeaTypeCommandValidator().Validate(request);
            if (!validation.IsValid)
                return Result.Fail(validation);

            var oldTeaType = await _teaTypeRepository.GetByIdAsync(request.Id);

            if (oldTeaType is null)
                return TeaTypeErrors.TeaTypeNotFound;

            var updatedTeaType = _mapper.Map<Entities.TeaType>(request.TeaType);

            #region Update properties
            updatedTeaType.Id = (Guid)request.Id!;
            updatedTeaType.CreatedBy = oldTeaType.CreatedBy;
            updatedTeaType.CreatedAt = oldTeaType.CreatedAt;
            #endregion

            await _teaTypeRepository.UpdateAsync(oldTeaType, updatedTeaType);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
