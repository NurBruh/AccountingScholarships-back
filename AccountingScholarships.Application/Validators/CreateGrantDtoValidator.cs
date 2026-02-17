using AccountingScholarships.Domain.DTO;
using FluentValidation;

namespace AccountingScholarships.Application.Validators;

public class CreateGrantDtoValidator : AbstractValidator<CreateGrantDto>
{
    public CreateGrantDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Название гранта обязательно")
            .MaximumLength(200).WithMessage("Название не должно превышать 200 символов");

        RuleFor(x => x.Type)
            .MaximumLength(100).WithMessage("Тип не должен превышать 100 символов")
            .When(x => !string.IsNullOrEmpty(x.Type));

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Сумма гранта должна быть больше 0");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Дата начала обязательна");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate).WithMessage("Дата окончания должна быть после даты начала")
            .When(x => x.EndDate.HasValue);

        RuleFor(x => x.StudentId)
            .GreaterThan(0).WithMessage("Необходимо указать студента");
    }
}