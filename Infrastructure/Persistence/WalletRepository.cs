using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{

    public class WalletRepository
    {
        private readonly AppDbContext _context;

        public WalletRepository(AppDbContext context) => _context = context;

        public async Task<Wallet?> GetByIdAsync(int id) =>
            await _context.Wallets.FindAsync(id);

        public async Task<List<Wallet>> GetAllAsync() =>
            await _context.Wallets.ToListAsync();

        public async Task AddAsync(Wallet wallet)
        {
            await _context.Wallets.AddAsync(wallet);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Wallet wallet)
        {
            _context.Wallets.Update(wallet);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWithTransactionAsync(Wallet fromWallet, Wallet toWallet, Transaction debit, Transaction credit)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Wallets.Update(fromWallet);
                _context.Wallets.Update(toWallet);
                await _context.Transactions.AddRangeAsync(debit, credit);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
