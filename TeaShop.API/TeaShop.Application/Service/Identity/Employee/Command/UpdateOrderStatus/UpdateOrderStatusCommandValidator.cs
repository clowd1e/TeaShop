using FluentValidation;
using TeaShop.Application.DTOs.Order.Request.UpdateStatus;

namespace TeaShop.Application.Service.Identity.Employee.Command.UpdateOrderStatus
{
    public sealed class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
    {
        public UpdateOrderStatusCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Order)
                .NotEmpty()
                .SetValidator(new UpdateOrderStatusRequestDtoValidator() as IValidator<UpdateOrderStatusRequestDto?>);
        }
    }
}
