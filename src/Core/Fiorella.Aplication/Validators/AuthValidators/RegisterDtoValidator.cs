using Fiorella.Aplication.DTOs.AuthDTOs;
using FluentValidation;

namespace Fiorella.Aplication.Validators.AuthValidators;

public class RegisterDtoValidator:AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(u => u.Fullname)
            .MaximumLength(150);
        RuleFor(u => u.email)
            .EmailAddress()
            .MaximumLength(255)
            .NotEmpty()
            .NotNull();
        RuleFor(u => u.username)
            .MaximumLength(50)
            .NotEmpty()
            .NotNull();
        RuleFor(u => u.password)
            .MaximumLength(150)
            .NotEmpty()
            .NotNull();
    }
}
