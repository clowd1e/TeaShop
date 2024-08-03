using FluentValidation;
using TeaShop.Application.DTOs.Order.Request.Remove;

namespace TeaShop.Application.Service.Order.Command.RemoveOrder
{
    public sealed class RemoveOrderCommandValidator : AbstractValidator<RemoveOrderCommand>
    {
        public RemoveOrderCommandValidator()
        {
            RuleFor(x => x.Order)
                .NotEmpty()
                .SetValidator(new RemoveOrderRequestDtoValidator() as IValidator<RemoveOrderRequestDto?>);
        }
    }
}
