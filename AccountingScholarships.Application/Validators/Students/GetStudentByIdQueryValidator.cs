using AccountingScholarships.Application.Queries.Students;
using FluentValidation;

namespace AccountingScholarships.Application.Validators.Students;

public class GetStudentByIdQueryValidator : AbstractValidator<GetStudentByIdQuery>
{
    public GetStudentByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID студента обязателен");
    }
}
