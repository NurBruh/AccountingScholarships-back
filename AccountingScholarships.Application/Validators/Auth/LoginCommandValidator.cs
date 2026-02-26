using AccountingScholarships.Application.Commands.Auth;
using FluentValidation;

namespace AccountingScholarships.Application.Validators.Auth;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Login.Username)
            .NotEmpty().WithMessage("Имя пользователя обязательно");

        RuleFor(x => x.Login.Password)
            .NotEmpty().WithMessage("Пароль обязателен");
    }
}
