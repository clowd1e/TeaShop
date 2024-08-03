using FluentValidation;

namespace TeaShop.Application.DTOs.Address.Request.Add
{
    public sealed class AddAddressRequestDtoValidator : AbstractValidator<AddAddressRequestDto>
    {
        public AddAddressRequestDtoValidator()
        {
            #region Rules
            RuleFor(x => x.Country)
                .NotEmpty();

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
