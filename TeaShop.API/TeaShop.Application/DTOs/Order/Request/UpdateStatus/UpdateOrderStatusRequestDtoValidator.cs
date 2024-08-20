using FluentValidation;

namespace TeaShop.Application.DTOs.Order.Request.UpdateStatus
{
    public sealed class UpdateOrderStatusRequestDtoValidator : AbstractValidator<UpdateOrderStatusRequestDto>
    {
        public UpdateOrderStatusRequestDtoValidator()
        {
            RuleFor(x => x.NewStatus)
                .NotEmpty()
                .IsInEnum();
        }
    }
}
