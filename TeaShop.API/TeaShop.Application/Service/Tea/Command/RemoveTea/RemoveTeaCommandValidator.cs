using FluentValidation;
using TeaShop.Application.DTOs.Tea.Request.Remove;

namespace TeaShop.Application.Service.Tea.Command.RemoveTea
{
    public sealed class RemoveTeaCommandValidator : AbstractValidator<RemoveTeaCommand>
    {
        public RemoveTeaCommandValidator()
        {
            RuleFor(x => x.Tea)
                .NotNull()
                .SetValidator(new RemoveTeaRequestDtoValidator() as IValidator<RemoveTeaRequestDto?>);
        }
    }
}
