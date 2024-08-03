using FluentValidation;
using TeaShop.Application.DTOs.Address.Request;
using TeaShop.Application.DTOs.Address.Request.Add;

namespace TeaShop.Application.DTOs.Customer.Request.Add
{
    public sealed class AddCustomerRequestDtoValidator : AbstractValidator<AddCustomerRequestDto>
    {
        public AddCustomerRequestDtoValidator()
        {
            #region Rules
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(60);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(60);

            RuleFor(x => x.Address)
                .NotNull()
                .SetValidator(new AddAddressRequestDtoValidator() as IValidator<AddAddressRequestDto?>);
            #endregion
        }
    }
}
