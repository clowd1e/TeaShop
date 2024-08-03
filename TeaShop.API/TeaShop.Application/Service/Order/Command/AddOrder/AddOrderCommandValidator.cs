using FluentValidation;
using TeaShop.Application.DTOs.Order.Request.Add;

namespace TeaShop.Application.Service.Order.Command.AddOrder
{
    public sealed class AddOrderCommandValidator : AbstractValidator<AddOrderCommand>
    {
        public AddOrderCommandValidator()
        {
            RuleFor(x => x.Order)
                .NotEmpty()
                .SetValidator(new AddOrderRequestDtoValidator() as IValidator<AddOrderRequestDto?>);
        }
    }
}
