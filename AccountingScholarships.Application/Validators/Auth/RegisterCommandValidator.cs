using AccountingScholarships.Application.Commands.Auth;
using FluentValidation;

namespace AccountingScholarships.Application.Validators.Auth;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Register.Username)
            .NotEmpty().WithMessage("Имя пользователя обязательно")
            .MaximumLength(100).WithMessage("Имя пользователя не должно превышать 100 символов");
            
        RuleFor(x => x.Register.Email)
            .NotEmpty().WithMessage("Email обязателен")
            .EmailAddress().WithMessage("Некорректный формат email")
            .MaximumLength(200).WithMessage("Email не должен превышать 200 символов");

        RuleFor(x => x.Register.Password)
            .NotEmpty().WithMessage("Пароль обязателен")
            .MinimumLength(6).WithMessage("Пароль должен содержать минимум 6 символов");
    }
}
