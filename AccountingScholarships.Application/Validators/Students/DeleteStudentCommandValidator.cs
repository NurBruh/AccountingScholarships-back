using AccountingScholarships.Application.Commands.Students;
using FluentValidation;

namespace AccountingScholarships.Application.Validators.Students;

public class DeleteStudentCommandValidator : AbstractValidator<DeleteStudentCommand>
{
    public DeleteStudentCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID студента обязателен");
    }
}
