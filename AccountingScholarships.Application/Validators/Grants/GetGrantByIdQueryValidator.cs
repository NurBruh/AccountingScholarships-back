using AccountingScholarships.Application.Queries.Grants;
using FluentValidation;

namespace AccountingScholarships.Application.Validators.Grants;

public class GetGrantByIdQueryValidator : AbstractValidator<GetGrantByIdQuery>
{
    public GetGrantByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID гранта обязателен");
    }
}
