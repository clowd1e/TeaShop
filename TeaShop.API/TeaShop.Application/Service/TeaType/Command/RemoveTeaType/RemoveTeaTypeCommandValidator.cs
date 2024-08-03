using FluentValidation;
using TeaShop.Application.DTOs.TeaType.Request.Remove;

namespace TeaShop.Application.Service.TeaType.Command.RemoveTeaType
{
    public sealed class RemoveTeaTypeCommandValidator : AbstractValidator<RemoveTeaTypeCommand>
    {
        public RemoveTeaTypeCommandValidator()
        {
            RuleFor(x => x.TeaType)
                .NotEmpty()
                .SetValidator(new RemoveTeaTypeRequestDtoValidator() as IValidator<RemoveTeaTypeRequestDto?>);
        }
    }
}
