using FluentValidation;

namespace TeaShop.Application.DTOs.Tea.Request.Remove
{
    public sealed class RemoveTeaRequestDtoValidator : AbstractValidator<RemoveTeaRequestDto>
    {
        public RemoveTeaRequestDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
