using Fiorella.Aplication.DTOs.CategoryDTOs;
using FluentValidation;
using System;

namespace Fiorella.Aplication.Validators.CategoryValidators;

public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateDtoValidator()
    {
        RuleFor(c => c.name).NotNull().NotEmpty().MaximumLength(30)
            .Matches("^[a-zA-Z]+$").WithMessage("Name can only contain letters.");

        RuleFor(c => c.description).MaximumLength(500).Matches("^[a-zA-Z0-9]+$")
            .WithMessage("Description can only contain letters and digits (no symbols)."); 
    }
}