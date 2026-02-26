using AccountingScholarships.Application.Commands.Grants;
using FluentValidation;

namespace AccountingScholarships.Application.Validators.Grants;

public class UpdateGrantCommandValidator : AbstractValidator<UpdateGrantCommand>
{
    public UpdateGrantCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID гранта обязателен");

        RuleFor(x => x.Dto)
            .NotNull().WithMessage("Данные о гранте обязательны")
            .SetValidator(new UpdateGrantDtoValidator()!);
    }
}
