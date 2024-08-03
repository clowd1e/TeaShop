using AutoMapper;
using MediatR;
using TeaShop.Application.Data;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Domain.Repository;
using Entities = TeaShop.Domain.Entities;

namespace TeaShop.Application.Service.Tea.Command.UpdateTea
{
    public sealed class UpdateTeaCommandHandler : IRequestHandler<UpdateTeaCommand, Result>
    {
        private readonly ITeaRepository _teaRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTeaCommandHandler(
            ITeaRepository teaRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _teaRepository = teaRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateTeaCommand request, CancellationToken cancellationToken)
        {
            var validation = new UpdateTeaCommandValidator().Validate(request);
            if (!validation.IsValid)
                return Result.Fail(validation);

            var oldTea = await _teaRepository.GetByIdAsync(request.Id);

            if (oldTea is null)
                return TeaErrors.TeaNotFound;

            var updatedTea = _mapper.Map<Entities.Tea>(request.Tea);

            #region Update properties
            updatedTea.Id = (Guid)request.Id!;
            updatedTea.CreatedBy = oldTea.CreatedBy;
            updatedTea.CreatedAt = oldTea.CreatedAt;
            #endregion

            await _teaRepository.UpdateAsync(oldTea, updatedTea);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
