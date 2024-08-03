using FluentValidation;
using TeaShop.Application.DTOs.Tea.Request.Add;

namespace TeaShop.Application.Service.Tea.Command.AddTea
{
    public sealed class AddTeaCommandValidator : AbstractValidator<AddTeaCommand>
    {
        public AddTeaCommandValidator()
        {
            RuleFor(x => x.Tea)
                .NotNull()
                .SetValidator(new AddTeaRequestDtoValidator());
        }
    }
}
