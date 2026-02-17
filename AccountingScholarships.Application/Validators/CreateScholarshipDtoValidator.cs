
using AccountingScholarships.Domain.DTO;
using FluentValidation;

namespace AccountingScholarships.Application.Validators;

public class CreateScholarshipDtoValidator : AbstractValidator<CreateScholarshipDto>
{
    public CreateScholarshipDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Название стипендии обязательно")
            .MaximumLength(200).WithMessage("Название не должно превышать 200 символов");

        RuleFor(x => x.Type)
            .MaximumLength(100).WithMessage("Тип не должен превышать 100 символов")
            .When(x => !string.IsNullOrEmpty(x.Type));

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Сумма стипендии должна быть больше 0");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Дата начала обязательна");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate).WithMessage("Дата окончания должна быть после даты начала")
            .When(x => x.EndDate.HasValue);

        RuleFor(x => x.StudentId)
            .NotEmpty().WithMessage("ID студента обязателен");
    }
}
