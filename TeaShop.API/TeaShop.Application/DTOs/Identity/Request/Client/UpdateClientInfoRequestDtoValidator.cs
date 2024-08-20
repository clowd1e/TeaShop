using FluentValidation;
using TeaShop.Application.DTOs.Address.Request.Update;

namespace TeaShop.Application.DTOs.Identity.Request.Client
{
    public sealed class UpdateClientInfoRequestDtoValidator : AbstractValidator<UpdateClientInfoRequestDto>
    {
        public UpdateClientInfoRequestDtoValidator()
        {
            RuleFor(x => x.FirstName)
                 .NotEmpty()
                 .Length(3, 60);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .Length(3, 60);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .Length(3, 60);

            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .LessThan(DateTime.UtcNow);

            RuleFor(x => x.Address)
                .NotEmpty()
                .SetValidator(new UpdateAddressRequestDtoValidator() as IValidator<UpdateAddressRequestDto?>);
        }
    }
}
