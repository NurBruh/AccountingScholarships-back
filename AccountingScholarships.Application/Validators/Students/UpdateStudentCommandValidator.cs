using AccountingScholarships.Application.Commands.Students;
using FluentValidation;

namespace AccountingScholarships.Application.Validators.Students;

public class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID студента обязателен");

        RuleFor(x => x.Student)
            .NotNull().WithMessage("Данные студента обязательны")
            .SetValidator(new UpdateStudentDtoValidator()!);
    }
}
