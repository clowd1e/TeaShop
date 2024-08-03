using FluentValidation;
using TeaShop.Application.DTOs.Address.Request.Update;
using TeaShop.Application.DTOs.TeaItem.Request.Update;

namespace TeaShop.Application.DTOs.OrderDetails.Request.Update
{
    public class UpdateOrderDetailsRequestDtoValidator : AbstractValidator<UpdateOrderDetailsRequestDto>
    {
        public UpdateOrderDetailsRequestDtoValidator()
        {
            #region Rules
            RuleFor(x => x.Items)
                .NotEmpty()
                .ForEach(x => x.SetValidator(new UpdateTeaItemRequestDtoValidator() as IValidator<UpdateTeaItemRequestDto?>));

            RuleFor(x => x.OrderedAt)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now);

            RuleFor(x => x.ShippingAddress)
                .NotEmpty()
                .SetValidator(new UpdateAddressRequestDtoValidator() as IValidator<UpdateAddressRequestDto?>);

            RuleFor(x => x.Discount)
                .NotEmpty()
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100);

            RuleFor(x => x.Status)
                .NotEmpty();
            #endregion
        }
    }
}
