using AccountingScholarships.Application.Commands.Scholarships;
using FluentValidation;

namespace AccountingScholarships.Application.Validators.Scholarships;

public class CreateScholarshipCommandValidator : AbstractValidator<CreateScholarshipCommand>
{
    public CreateScholarshipCommandValidator()
    {
        RuleFor(x => x.Dto)
            .NotNull().WithMessage("Данные о стипендии обязательны")
            .SetValidator(new CreateScholarshipDtoValidator()!);
    }
}
