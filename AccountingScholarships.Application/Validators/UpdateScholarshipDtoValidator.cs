using AccountingScholarships.Domain.DTO;
using FluentValidation;

namespace AccountingScholarships.Application.Validators;

public class UpdateScholarshipDtoValidator : AbstractValidator<UpdateScholarshipDto>
{
    public UpdateScholarshipDtoValidator()
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

        //RuleFor(x => x.EndDate)
        //    .GreaterThan(x => x.StartDate).WithMessage("Дата окончания должна быть после даты начала")
        //    .When(x => x.EndDate.HasValue);

        RuleFor(x => x.LostDate)
            .GreaterThan(x => x.StartDate).WithMessage("Дата лишения должна быть после даты начала")
            .When(x => x.LostDate.HasValue);
        RuleFor(x => x.OrderLostDate)
            .GreaterThan(x => x.StartDate).WithMessage("Дата о приказе лишений стипендий должна быть после даты начала")
            .When(x => x.OrderLostDate.HasValue);
        RuleFor(x => x.OrderCandidateDate)
            .GreaterThan(x => x.StartDate).WithMessage("Дата о приказе кандидата стипендий должна быть после даты начала")
            .When(x => x.OrderCandidateDate.HasValue);
        RuleFor(x => x.Notes)
            .MaximumLength(100).WithMessage("Примечания не должен превышать 100 символов")
            .When(x => !string.IsNullOrEmpty(x.Type));
    }
}