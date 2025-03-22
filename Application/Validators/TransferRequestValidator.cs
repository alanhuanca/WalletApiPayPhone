using Application.DTOs;
using FluentValidation; 

namespace Application.Validators
{

    public class TransferRequestValidator : AbstractValidator<TransferRequest>
    {
        public TransferRequestValidator()
        {
            RuleFor(t => t.FromWalletId)
                .GreaterThan(0).WithMessage("El ID de la billetera origen debe ser mayor que cero.");

            RuleFor(t => t.ToWalletId)
                .GreaterThan(0).WithMessage("El ID de la billetera destino debe ser mayor que cero.")
                .NotEqual(t => t.FromWalletId).WithMessage("No se puede transferir a la misma billetera.");

            RuleFor(t => t.Amount)
                .GreaterThan(0).WithMessage("El monto debe ser mayor que cero.");
        }
    }
}
