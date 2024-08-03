using FluentValidation;

namespace TeaShop.Application.DTOs.TeaItem.Request.Update
{
    public sealed class UpdateTeaItemRequestDtoValidator : AbstractValidator<UpdateTeaItemRequestDto>
    {
        public UpdateTeaItemRequestDtoValidator()
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
