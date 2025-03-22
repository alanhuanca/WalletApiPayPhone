using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{

    public class CreateWalletValidator : AbstractValidator<CreateWalletRequest>
    {
        public CreateWalletValidator()
        {
            RuleFor(w => w.DocumentId)
                .NotEmpty().WithMessage("El documento de identidad es obligatorio.")
                .Length(8, 12).WithMessage("El documento debe tener entre 8 y 12 caracteres.");

            RuleFor(w => w.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no debe exceder los 100 caracteres.");

            RuleFor(w => w.Balance)
                .GreaterThanOrEqualTo(0).WithMessage("El saldo debe ser mayor o igual a cero.");
        }
    }
}
