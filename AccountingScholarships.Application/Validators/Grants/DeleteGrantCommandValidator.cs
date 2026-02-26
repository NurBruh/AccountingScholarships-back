using AccountingScholarships.Application.Commands.Grants;
using FluentValidation;

namespace AccountingScholarships.Application.Validators.Grants;

public class DeleteGrantCommandValidator : AbstractValidator<DeleteGrantCommand>
{
    public DeleteGrantCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID гранта обязателен");
    }
}
