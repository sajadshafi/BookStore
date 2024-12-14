using Application.Contracts.Base;
using Application.Contracts.Books;
using Application.IServices;
using Application.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Web.Controllers {
  [Route("api/books")]
  [ApiController]
  public class BooksController : ControllerBase {
    #region Private Fields
    private readonly IBookService _bookService;
    #endregion
    
    #region Constructors

    public BooksController(IBookService bookService) {
      _bookService = bookService;
    }
    #endregion
    
    #region Public book crud endpoints
    [HttpPost]
    public async Task<IActionResult> Create(CreateBookRequest request) {

      (bool isValid, string? message) = BookValidations.IsCreateBookRequestValid(request);
      
      if(!isValid) 
        return BadRequest(message);
      
      return Ok(await _bookService.CreateBookAsync(request));
    }
    
    [HttpPut]
    public async Task<IActionResult> Update(UpdateBookRequest request) {
      return Ok(await _bookService.UpdateBookAsync(request));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid? id) {
      
      if(id == null) {
        return BadRequest();
      }
      
      return Ok(await _bookService.GetBookAsync(id.Value));
    }
    
    [HttpPost("list")]
    public async Task<IActionResult> Get(PaginationRequest request) {

      if(request.PageNumber <= 0) {
        return BadRequest("Page number must be greater than 0");
      }

      if(request.PageSize <= 0) {
        return BadRequest("Page size must be greater than 0");
      }
      
      return Ok(await _bookService.GetBooksAsync(request));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) {
      return Ok(await _bookService.DeleteBookAsync(id));
    }
    #endregion
  }
}