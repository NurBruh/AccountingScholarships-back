using AccountingScholarships.Application.DTOs;
using FluentValidation;

namespace AccountingScholarships.Application.Validators;

public class UpdateStudentDtoValidator : AbstractValidator<UpdateStudentDto>
{
    public UpdateStudentDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Имя обязательно")
            .MaximumLength(100).WithMessage("Имя не должно превышать 100 символов");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Фамилия обязательна")
            .MaximumLength(100).WithMessage("Фамилия не должна превышать 100 символов");

        RuleFor(x => x.MiddleName)
            .MaximumLength(100).WithMessage("Отчество не должно превышать 100 символов")
            .When(x => !string.IsNullOrEmpty(x.MiddleName));

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email обязателен")
            .EmailAddress().WithMessage("Некорректный формат email")
            .MaximumLength(200).WithMessage("Email не должен превышать 200 символов");

        RuleFor(x => x.Phone)
            .MaximumLength(20).WithMessage("Телефон не должен превышать 20 символов")
            .When(x => !string.IsNullOrEmpty(x.Phone));

        RuleFor(x => x.Course)
            .GreaterThan(0).WithMessage("Курс должен быть больше 0")
            .LessThanOrEqualTo(6).WithMessage("Курс не должен превышать 6");
    }
}
