using FluentValidation;
using TeaShop.Application.DTOs.Customer.Request.Update;
using TeaShop.Application.DTOs.OrderDetails.Request.Update;

namespace TeaShop.Application.DTOs.Order.Request.Update
{
    public sealed class UpdateOrderRequestDtoValidator : AbstractValidator<UpdateOrderRequestDto>
    {
        public UpdateOrderRequestDtoValidator()
        {
            #region Rules
            RuleFor(x => x.Customer)
                .NotEmpty()
                .SetValidator(new UpdateCustomerRequestDtoValidator() as IValidator<UpdateCustomerRequestDto?>);

            RuleFor(x => x.Details)
                .NotEmpty()
                .SetValidator(new UpdateOrderDetailsRequestDtoValidator() as IValidator<UpdateOrderDetailsRequestDto?>);
            #endregion
        }
    }
}
