using FluentValidation;
using TeaShop.Application.DTOs.TeaType.Request.Add;

namespace TeaShop.Application.Service.TeaType.Command.AddTeaType
{
    public sealed class AddTeaTypeCommandValidator : AbstractValidator<AddTeaTypeCommand>
    {
        public AddTeaTypeCommandValidator()
        {
            RuleFor(x => x.TeaType)
                .NotNull()
                .SetValidator(new AddTeaTypeRequestDtoValidator());
        }
    }
}
