using MediatR;
using TeaShop.Application.Data;
using TeaShop.Application.ResultBehavior;
using TeaShop.Application.ResultBehavior.Errors;
using TeaShop.Domain.Repository;

namespace TeaShop.Application.Service.Tea.Command.RemoveTea
{
    public sealed class RemoveTeaCommandHandler : IRequestHandler<RemoveTeaCommand, Result>
    {
        private readonly ITeaRepository _teaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveTeaCommandHandler(
            ITeaRepository teaRepository,
            IUnitOfWork unitOfWork)
        {
            _teaRepository = teaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(RemoveTeaCommand request, CancellationToken cancellationToken)
        {
            var validation = new RemoveTeaCommandValidator().Validate(request);
            if (!validation.IsValid)
                return Result.Fail(validation);

            var tea = await _teaRepository.GetByIdAsync(request.Tea!.Id);

            if (tea is null)
                return TeaErrors.TeaNotFound;

            await _teaRepository.DeleteAsync(tea);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
