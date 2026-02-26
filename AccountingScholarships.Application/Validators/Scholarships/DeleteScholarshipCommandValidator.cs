using AccountingScholarships.Application.Commands.Scholarships;
using FluentValidation;

namespace AccountingScholarships.Application.Validators.Scholarships;

public class DeleteScholarshipCommandValidator : AbstractValidator<DeleteScholarshipCommand>
{
    public DeleteScholarshipCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID стипендии обязателен");
    }
}
