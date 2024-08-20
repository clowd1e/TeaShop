using FluentValidation;

namespace TeaShop.Application.DTOs.Identity.Request
{
    public sealed class UpdatePasswordRequestDtoValidator : AbstractValidator<UpdatePasswordRequestDto>
    {
        public UpdatePasswordRequestDtoValidator()
        {
            // TODO: Add Regex for password validation
            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .Length(8, 32);

            RuleFor(x => x.RepeatNewPassword)
                .NotEmpty()
                .Equal(x => x.NewPassword);
        }
    }
}
