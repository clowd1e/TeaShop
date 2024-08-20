using FluentValidation;
using TeaShop.Application.DTOs.Address.Request.Update;

namespace TeaShop.Application.DTOs.Identity.Request.MainManager
{
    public sealed class UpdateMainManagerInfoRequestDtoValidator : AbstractValidator<UpdateMainManagerInfoRequestDto>
    {
        public UpdateMainManagerInfoRequestDtoValidator()
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
