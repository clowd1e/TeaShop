using FluentValidation;

namespace TeaShop.Application.DTOs.TeaItem.Request.Add
{
    public sealed class AddTeaItemRequestDtoValidator : AbstractValidator<AddTeaItemRequestDto>
    {
        public AddTeaItemRequestDtoValidator()
        {
            #region Rules
            RuleFor(x => x.TeaId)
                .NotEmpty();

            RuleFor(x => x.Quantity)
                .InclusiveBetween(1, 100)
                .NotEmpty();
            #endregion
        }
    }
}
