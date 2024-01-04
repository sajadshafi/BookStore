using Dummy.Application.DTOs;
using Dummy.Helpers;
using Dummy.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dummy.Controllers;

[ApiController]
[Route("api/v1/todo")]
public class TodoController(ITodoService todoService) : ControllerBase {
  private readonly ITodoService _todoService = todoService;

  [HttpPut]
  public async Task<IActionResult> Add(TodoDTO model) {
    var isValid = model.IsValidTodo();
    if(!isValid) return BadRequest();

    var result = await _todoService.AddAsync(model);
    return Ok(result);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(int id) {
    var result = await _todoService.DeleteAsync(id);

    if(!result.Success && (result.Data == default)) {
      return NotFound();
    }
    return Ok(result);
  }

  [HttpGet]
  public async Task<IActionResult> Get() {
    var result = await _todoService.GetAsync();
    if(result.Success && result.Data.Count == 0) {
      return NotFound();
    }
    return Ok(result);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> Get(int id) {
    var result = await _todoService.GetAsync(id);
    if(result.Success && result.Data == default) {
      return NotFound();
    }
    return Ok(result);
  }
}