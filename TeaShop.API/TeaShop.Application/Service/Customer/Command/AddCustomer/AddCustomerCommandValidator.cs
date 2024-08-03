using FluentValidation;
using TeaShop.Application.DTOs.Customer.Request.Add;

namespace TeaShop.Application.Service.Customer.Command.AddCustomer
{
    public sealed class AddCustomerCommandValidator : AbstractValidator<AddCustomerCommand>
    {
        public AddCustomerCommandValidator()
        {
            RuleFor(x => x.Customer)
                .NotNull()
                .SetValidator(new AddCustomerRequestDtoValidator() as IValidator<AddCustomerRequestDto?>);
        }
    }
}
