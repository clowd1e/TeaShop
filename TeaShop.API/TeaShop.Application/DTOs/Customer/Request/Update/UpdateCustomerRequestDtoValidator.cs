using FluentValidation;
using TeaShop.Application.DTOs.Address.Request.Update;

namespace TeaShop.Application.DTOs.Customer.Request.Update
{
    public sealed class UpdateCustomerRequestDtoValidator : AbstractValidator<UpdateCustomerRequestDto>
    {
        public UpdateCustomerRequestDtoValidator()
        {
            #region Rules
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Address)
                .NotEmpty()
                .SetValidator(new UpdateAddressRequestDtoValidator() as IValidator<UpdateAddressRequestDto?>);
            #endregion
        }
    }
}
