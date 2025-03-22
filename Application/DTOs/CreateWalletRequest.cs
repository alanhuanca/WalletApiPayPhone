using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    /// <summary>
    /// DTO para la creación de una billetera.
    /// </summary>
    public record CreateWalletRequest(string DocumentId, string Name, decimal Balance);
    public record TransferRequest(int FromWalletId, int ToWalletId, decimal Amount);
}
