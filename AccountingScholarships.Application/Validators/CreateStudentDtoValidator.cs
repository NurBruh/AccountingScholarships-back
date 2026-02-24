using AccountingScholarships.Domain.DTO;
using FluentValidation;

namespace AccountingScholarships.Application.Validators;

public class CreateStudentDtoValidator : AbstractValidator<CreateStudentDto>
{
    public CreateStudentDtoValidator()
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

        RuleFor(x => x.IIN)
            .NotEmpty().WithMessage("ИИН обязателен")
            .Length(12).WithMessage("ИИН должен содержать 12 символов")
            .Matches("^[0-9]{12}$").WithMessage("ИИН должен содержать только цифры");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Дата рождения обязательна")
            .LessThan(DateTime.Now).WithMessage("Дата рождения не может быть в будущем");

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
        RuleFor(x => x.iban)
            .NotEmpty().WithMessage("IBAN объязателен")
            .Length(20).WithMessage("IBAN должен содержать 20 символов");

    }
}