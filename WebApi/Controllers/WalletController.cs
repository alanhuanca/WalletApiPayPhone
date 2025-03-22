using Application.UseCases;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WalletController : ControllerBase
{
    private readonly CreateWalletCommand _createWallet;
    private readonly TransferFundsCommand _transferFunds; 
    private readonly IValidator<TransferRequest> _transferValidator; 
    private readonly IValidator<CreateWalletRequest> _createWalletValidator;


    public WalletController(TransferFundsCommand transferFunds, 
        IValidator<TransferRequest> transferValidator,
        CreateWalletCommand createWallet, IValidator<CreateWalletRequest> createWalletValidator)
    {
        _transferFunds = transferFunds;
        _transferValidator = transferValidator;
        _createWallet = createWallet;
        _createWalletValidator = createWalletValidator;
    }

    /// <summary>
    /// Crea una nueva billetera.
    /// </summary>
    /// <param name="request">Datos de la billetera a crear.</param>
    /// <returns>La billetera creada.</returns>
    [HttpPost]
    
    public async Task<IActionResult> CreateWallet([FromBody] CreateWalletRequest request)
    {
        await _createWalletValidator.ValidateAndThrowAsync(request);
        var wallet = await _createWallet.Execute(request.DocumentId, request.Name, request.Balance);
        return Ok(wallet);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("transfer")]
    [Authorize]
    public async Task<IActionResult> TransferFunds([FromBody] TransferRequest request)
    {
        await _transferValidator.ValidateAndThrowAsync(request);
        await _transferFunds.Execute(request.FromWalletId, request.ToWalletId, request.Amount);
        return Ok(new { Message = "Transferencia realizada con éxito." });
    }
}

public record CreateWalletRequest(string DocumentId, string Name, decimal Balance);
public record TransferRequest(int FromWalletId, int ToWalletId, decimal Amount);
