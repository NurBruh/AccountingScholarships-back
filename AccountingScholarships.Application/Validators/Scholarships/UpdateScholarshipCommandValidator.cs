using AccountingScholarships.Application.Commands.Scholarships;
using FluentValidation;

namespace AccountingScholarships.Application.Validators.Scholarships;

public class UpdateScholarshipCommandValidator : AbstractValidator<UpdateScholarshipCommand>
{
    public UpdateScholarshipCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID стипендии обязателен");

        RuleFor(x => x.Dto)
            .NotNull().WithMessage("Данные о стипендии обязательны")
            .SetValidator(new UpdateScholarshipDtoValidator()!);
    }
}
