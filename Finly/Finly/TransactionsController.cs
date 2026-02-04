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

    public TransactionsController(ITransactionService service)
    {
        _service = service;
    }

    private int UserId =>int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    [HttpPost]
    public async Task<IActionResult> Add(CreateTransactionModel model)
    {
        await _service.AddTransactionAsync(model, UserId);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteTransactionAsync(id, UserId);
        return Ok();
    }

    [HttpGet("history")]
    public async Task<IActionResult> History()
    {
        return Ok(await _service.GetHistoryAsync(UserId));
    }
}