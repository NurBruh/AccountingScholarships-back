using AccountingScholarships.Application.Commands.Grants;
using FluentValidation;

namespace AccountingScholarships.Application.Validators.Grants;

public class CreateGrantCommandValidator : AbstractValidator<CreateGrantCommand>
{
    public CreateGrantCommandValidator()
    {
        RuleFor(x => x.Dto)
            .NotNull().WithMessage("Данные о гранте обязательны")
            .SetValidator(new CreateGrantDtoValidator()!);
    }
}
