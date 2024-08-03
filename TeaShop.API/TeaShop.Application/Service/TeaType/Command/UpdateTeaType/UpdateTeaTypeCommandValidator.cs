using FluentValidation;
using TeaShop.Application.DTOs.TeaType.Request.Update;

namespace TeaShop.Application.Service.TeaType.Command.UpdateTeaType
{
    public sealed class UpdateTeaTypeCommandValidator : AbstractValidator<UpdateTeaTypeCommand>
    {
        public UpdateTeaTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.TeaType)
                .NotNull()
                .SetValidator(new UpdateTeaTypeRequestDtoValidator() as IValidator<UpdateTeaTypeRequestDto?>);
        }
    }
}
