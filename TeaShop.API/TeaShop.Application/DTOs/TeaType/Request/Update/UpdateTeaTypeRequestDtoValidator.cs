using FluentValidation;

namespace TeaShop.Application.DTOs.TeaType.Request.Update
{
    public sealed class UpdateTeaTypeRequestDtoValidator : AbstractValidator<UpdateTeaTypeRequestDto>
    {
        public UpdateTeaTypeRequestDtoValidator()
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
