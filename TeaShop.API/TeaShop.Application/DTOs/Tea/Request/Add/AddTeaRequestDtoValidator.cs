﻿using FluentValidation;

namespace TeaShop.Application.DTOs.Tea.Request.Add
{
    public sealed class AddTeaRequestDtoValidator : AbstractValidator<AddTeaRequestDto>
    {
        public AddTeaRequestDtoValidator()
        {
            #region Rules
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(3, 60);

            RuleFor(x => x.Description)
                .NotEmpty()
                .Length(10, 1000);

            RuleFor(x => x.Price)
                .NotEmpty()
                .GreaterThan(0)
                .LessThanOrEqualTo(1000);

            RuleFor(x => x.IsInStock)
                .NotEmpty();

            RuleFor(x => x.AvailableStock)
                .NotEmpty()
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(5000);

            RuleFor(x => x.TeaTypeId)
                .NotEmpty();
            #endregion
        }
    }
}
