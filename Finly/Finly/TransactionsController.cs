using System.Security.Claims;
using Finly.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finly;

[Authorize]
[ApiController]
[Route("api/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _service;
    private readonly IUserContext _userContext;

    public TransactionsController(
        ITransactionService service,
        IUserContext userContext)
    {
        _service = service;
        _userContext = userContext;
    }

    [HttpPost]
    public async Task<IActionResult> Add(
        CreateTransactionModel model,
        CancellationToken cancellationToken)
    {
        await _service.AddTransactionAsync(
            model,
            _userContext.UserId,
            cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        await _service.DeleteTransactionAsync(
            id,
            _userContext.UserId,
            cancellationToken);

        return NoContent();
    }

    [HttpGet("history")]
    public async Task<IActionResult> History(
        CancellationToken cancellationToken,
        [FromQuery] int months = 2)
    {
        var fromDate = DateTime.UtcNow.AddMonths(-months);

        var result = await _service.GetHistoryAsync(
            _userContext.UserId,
            fromDate,
            cancellationToken);

        return Ok(result);
    }
}
