using FluentValidation;
using TeaShop.Application.DTOs.Tea.Request.Update;

namespace TeaShop.Application.Service.Tea.Command.UpdateTea
{
    public sealed class UpdateTeaCommandValidator : AbstractValidator<UpdateTeaCommand>
    {
        public UpdateTeaCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Tea)
                .NotNull()
                .SetValidator(new UpdateTeaRequestDtoValidator() as IValidator<UpdateTeaRequestDto?>);
        }
    }
}
