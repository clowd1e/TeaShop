using FluentValidation;
using TeaShop.Application.DTOs.Order.Request.Update;

namespace TeaShop.Application.Service.Order.Command.UpdateOrder
{
    public sealed class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Order)
                .NotEmpty()
                .SetValidator(new UpdateOrderRequestDtoValidator() as IValidator<UpdateOrderRequestDto?>);
        }
    }
}
