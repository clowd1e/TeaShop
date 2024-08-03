using FluentValidation;

namespace TeaShop.Application.DTOs.Address.Request.Update
{
    public sealed class UpdateAddressRequestDtoValidator : AbstractValidator<UpdateAddressRequestDto>
    {
        public UpdateAddressRequestDtoValidator()
        {
            #region Rules
            RuleFor(x => x.Country)
                .NotNull();

            RuleFor(x => x.Street)
                .NotEmpty()
                .Length(3, 60);

            RuleFor(x => x.City)
                .NotEmpty()
                .Length(3, 50);

            RuleFor(x => x.PostalCode)
                .NotEmpty()
                .Length(5, 10);
            #endregion
        }
    }
}
