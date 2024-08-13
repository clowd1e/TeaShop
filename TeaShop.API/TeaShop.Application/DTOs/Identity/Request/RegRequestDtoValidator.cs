using FluentValidation;
using TeaShop.Application.DTOs.Address.Request.Add;

namespace TeaShop.Application.DTOs.Identity.Request
{
    public sealed class RegRequestDtoValidator : AbstractValidator<RegRequestDto>
    {
        public RegRequestDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .Length(3, 60);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .Length(3, 60);

            // TODO: Add Regex for password validation
            RuleFor(x => x.Password)
                .NotEmpty()
                .Length(8, 32);

            RuleFor(x => x.RepeatPassword)
                .NotEmpty()
                .Equal(x => x.Password);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .Length(3, 60);

            RuleFor(x => x.Address)
                .NotEmpty()
                .SetValidator(new AddAddressRequestDtoValidator() as IValidator<AddAddressRequestDto?>);

            RuleFor(x => x.BirthDate)
                .NotEmpty();
        }
    }
}
