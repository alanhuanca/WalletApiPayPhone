using Application.UseCases;
using Domain.Entities;
using Infrastructure.Persistence;
using Moq; 

namespace Tests
{

    public class TransferFundsCommandTests
    {
        private readonly Mock<WalletRepository> _walletRepoMock;
        private readonly TransferFundsCommand _transferCommand;

        public TransferFundsCommandTests()
        {
            _walletRepoMock = new Mock<WalletRepository>(null!);
            _transferCommand = new TransferFundsCommand(_walletRepoMock.Object);
        }

        [Fact]
        public async Task Transfer_ThrowsException_WhenAmountIsZero()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _transferCommand.Execute(1, 2, 0));
        }

        [Fact]
        public async Task Transfer_ThrowsException_WhenWalletNotFound()
        {
            _walletRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Wallet?)null);

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _transferCommand.Execute(1, 2, 100));
        }
    }
}
