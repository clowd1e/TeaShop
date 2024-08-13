using FluentValidation;

namespace TeaShop.Application.DTOs.Identity.Request
{
    public sealed class AuthRequestDtoValidator : AbstractValidator<AuthRequestDto>
    {
        public AuthRequestDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .Length(3, 60);

            RuleFor(x => x.Password)
                .NotEmpty()
                .Length(8, 32);
        }
    }
}
