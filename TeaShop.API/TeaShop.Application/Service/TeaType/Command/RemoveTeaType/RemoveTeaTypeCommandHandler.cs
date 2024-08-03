using MediatR;
using TeaShop.Application.Data;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Domain.Repository;

namespace TeaShop.Application.Service.TeaType.Command.RemoveTeaType
{
    public sealed class RemoveTeaTypeCommandHandler : IRequestHandler<RemoveTeaTypeCommand, Result>
    {
        private readonly ITeaTypeRepository _teaTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveTeaTypeCommandHandler(
            ITeaTypeRepository teaTypeRepository,
            IUnitOfWork unitOfWork)
        {
            _teaTypeRepository = teaTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(RemoveTeaTypeCommand request, CancellationToken cancellationToken)
        {
            var validation = new RemoveTeaTypeCommandValidator().Validate(request);
            if (!validation.IsValid)
                return Result.Fail(validation);

            var teaType = await _teaTypeRepository.GetByIdAsync(request.TeaType!.Id);

            if (teaType is null)
                return TeaTypeErrors.TeaTypeNotFound;

            await _teaTypeRepository.DeleteAsync(teaType);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
