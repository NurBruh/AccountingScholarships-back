using AccountingScholarships.Application.Commands.Students;
using FluentValidation;

namespace AccountingScholarships.Application.Validators.Students;

public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentCommandValidator()
    {
        RuleFor(x => x.Student)
            .NotNull().WithMessage("Данные студента обязательны")
            .SetValidator(new CreateStudentDtoValidator()!);
    }
}
