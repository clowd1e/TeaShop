using FluentValidation;

namespace TeaShop.Application.DTOs.TeaType.Request.Remove
{
    public sealed class RemoveTeaTypeRequestDtoValidator : AbstractValidator<RemoveTeaTypeRequestDto>
    {
        public RemoveTeaTypeRequestDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
