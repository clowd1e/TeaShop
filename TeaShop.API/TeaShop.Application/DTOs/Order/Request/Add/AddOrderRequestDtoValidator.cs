using FluentValidation;
using TeaShop.Application.DTOs.Customer.Request.Add;
using TeaShop.Application.DTOs.OrderDetails.Request.Add;

namespace TeaShop.Application.DTOs.Order.Request.Add
{
    public sealed class AddOrderRequestDtoValidator : AbstractValidator<AddOrderRequestDto>
    {
        public AddOrderRequestDtoValidator()
        {
            #region Rules
            RuleFor(x => x.Customer)
                .NotEmpty()
                .SetValidator(new AddCustomerRequestDtoValidator() as IValidator<AddCustomerRequestDto?>);

            RuleFor(x => x.Details)
                .NotEmpty()
                .SetValidator(new AddOrderDetailsRequestDtoValidator() as IValidator<AddOrderDetailsRequestDto?>);
            #endregion
        }
    }
}
