using Microsoft.AspNetCore.Mvc;

namespace Dummy.Controllers;

[ApiController]
[Route("api/v1/todo")]
public class TodoController : ControllerBase {
  [HttpGet]
  public IActionResult Get() {
    return Ok("Working");
  }
}