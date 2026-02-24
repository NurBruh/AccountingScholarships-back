using AccountingScholarships.Application.Queries.Scholarships;
using FluentValidation;

namespace AccountingScholarships.Application.Validators.Scholarships;

public class GetScholarshipByIdQueryValidator : AbstractValidator<GetScholarshipByIdQuery>
{
    public GetScholarshipByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID стипендии обязателен");
    }
}
