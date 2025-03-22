using Domain.Entities;
using Infrastructure.Persistence;

namespace Application.UseCases
{

    public class CreateWalletCommand
    {
        private readonly WalletRepository _walletRepo;

        public CreateWalletCommand(WalletRepository walletRepo)
        {
            _walletRepo = walletRepo;
        }

        public async Task<Wallet> Execute(string documentId, string name, decimal balance)
        {
            if (balance < 0) throw new InvalidOperationException("El saldo no puede ser negativo.");

            var wallet = new Wallet
            {
                DocumentId = documentId,
                Name = name,
                Balance = balance
            };

            await _walletRepo.AddAsync(wallet);
            return wallet;
        }
    }
}
