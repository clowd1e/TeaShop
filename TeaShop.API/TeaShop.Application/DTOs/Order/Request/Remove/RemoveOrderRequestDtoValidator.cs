using FluentValidation;

namespace TeaShop.Application.DTOs.Order.Request.Remove
{
    public sealed class RemoveOrderRequestDtoValidator : AbstractValidator<RemoveOrderRequestDto>
    {
        public RemoveOrderRequestDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
