using FluentValidation;
using TeaShop.Application.DTOs.Address.Request.Add;
using TeaShop.Application.DTOs.TeaItem.Request.Add;

namespace TeaShop.Application.DTOs.OrderDetails.Request.Add
{
    public sealed class AddOrderDetailsRequestDtoValidator : AbstractValidator<AddOrderDetailsRequestDto>
    {
        public AddOrderDetailsRequestDtoValidator()
        {
            #region Rules
            RuleFor(x => x.Items)
                .NotEmpty()
                .ForEach(x => x.SetValidator(new AddTeaItemRequestDtoValidator() as IValidator<AddTeaItemRequestDto?>));

            RuleFor(x => x.OrderedAt)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now);

            RuleFor(x => x.ShippingAddress)
                .NotEmpty()
                .SetValidator(new AddAddressRequestDtoValidator() as IValidator<AddAddressRequestDto?>);

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
