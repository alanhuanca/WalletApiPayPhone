using Domain.Entities;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{

    public class TransferFundsCommand
    {
        private readonly WalletRepository _walletRepo;

        public TransferFundsCommand(WalletRepository walletRepo)
        {
            _walletRepo = walletRepo;
        }

        public async Task Execute(int fromWalletId, int toWalletId, decimal amount)
        {
            if (amount <= 0)
                throw new InvalidOperationException("El monto debe ser mayor a cero.");

            // Validar billeteras
            var fromWallet = await _walletRepo.GetByIdAsync(fromWalletId)
                ?? throw new InvalidOperationException("La billetera de origen no existe.");
            var toWallet = await _walletRepo.GetByIdAsync(toWalletId)
                ?? throw new InvalidOperationException("La billetera de destino no existe.");

            // Validar saldo suficiente
            if (fromWallet.Balance < amount)
                throw new InvalidOperationException("Saldo insuficiente en la billetera de origen.");

            // Actualizar saldos
            fromWallet.Balance -= amount;
            toWallet.Balance += amount;

            // Registrar transacciones
            var debit = new Transaction
            {
                WalletId = fromWalletId,
                Amount = amount,
                Type = "Debit",
                CreatedAt = DateTime.UtcNow
            };

            var credit = new Transaction
            {
                WalletId = toWalletId,
                Amount = amount,
                Type = "Credit",
                CreatedAt = DateTime.UtcNow
            };

            await _walletRepo.UpdateWithTransactionAsync(fromWallet, toWallet, debit, credit);
        }
    }
}
