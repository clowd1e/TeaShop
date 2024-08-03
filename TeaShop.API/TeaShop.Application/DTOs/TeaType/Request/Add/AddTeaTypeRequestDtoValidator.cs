using FluentValidation;

namespace TeaShop.Application.DTOs.TeaType.Request.Add
{
    public sealed class AddTeaTypeRequestDtoValidator : AbstractValidator<AddTeaTypeRequestDto>
    {
        public AddTeaTypeRequestDtoValidator()
        {
            #region Rules
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(3, 60);

            RuleFor(x => x.Description)
                .NotEmpty()
                .Length(10, 500);
            #endregion
        }
    }
}
