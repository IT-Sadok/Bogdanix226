using Finly.Models;
using Finly.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finly;

[Authorize]
[ApiController]
[Route("api/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _service;

    public TransactionsController(ITransactionService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Add(
        CreateTransactionModel model,
        CancellationToken cancellationToken)
    {
        var id = await _service.AddTransactionAsync(model,cancellationToken);

        return Created($"api/transactions/{id}", null);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        await _service.DeleteTransactionAsync(
            id,
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
            fromDate,
            cancellationToken);

        return Ok(result);
    }
}