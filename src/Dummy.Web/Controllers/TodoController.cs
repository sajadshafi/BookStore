using Dummy.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dummy.Controllers;

[ApiController]
[Route("api/v1/todo")]
public class TodoController(ITodoService todoService) : ControllerBase {
  private readonly ITodoService _todoService = todoService;

  [HttpGet]
  public async Task<IActionResult> Get() {
    var result = await _todoService.GetAsync();
    if(result.Success && result.Data.Count == 0) {
      return NotFound();
    }
    return Ok(result);
  }

  [HttpGet("{id}")]
  public IActionResult Get(int id) {
    return NotFound();
  }
}