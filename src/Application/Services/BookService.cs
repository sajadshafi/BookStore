using Application.Contracts;
using Application.Contracts.Base;
using Application.Contracts.Books;
using Application.Exceptions;
using Application.Extensions;
using Application.IServices;
using Domain.Features.Books;
using Infrastructure.Context;

namespace Application.Services
{
  public class BookService : IBookService
  {

    #region Private fields
    private readonly BookStoreContext _context;
    #endregion

    #region Constructors
    public BookService(BookStoreContext context)
    {
      _context = context;
    }
    #endregion
    public async Task<Response<Guid>> CreateBookAsync(CreateBookRequest request)
    {
      Book book = Book.Create(
        request.Title,
        request.Description,
        request.Author,
        request.Publisher,
        request.Price,
        request.PublishedOn);
      await _context.Books.AddAsync(book);
      await _context.SaveChangesAsync();

      return new Response<Guid>
      {
        Success = true,
        Data = book.Id,
        Message = "Book created"
      };
    }

    public async Task<Response<Guid>> UpdateBookAsync(UpdateBookRequest request)
    {
      Book book = await _context.Books.FindAsync(request.Id);

      if (book is null)
        throw new NotFoundException("Book not found");
      book.Update(
        request.Title,
        request.Description,
        request.Author,
        request.Publisher,
        request.Price,
        request.PublishedOn);
      return new Response<Guid>
      {
        Success = true,
        Data = book.Id,
        Message = "Book created"
      };
    }

    public async Task<Response<GetBookResponse>> GetBookAsync(Guid id)
    {
      Book book = await _context.Books.FindAsync(id);

      if (book is null)
        throw new NotFoundException("Book not found");
      GetBookResponse bookResponse = new()
      {
        Id = book.Id,
        Title = book.Title,
        Description = book.Description,
        Author = book.Author,
        Publisher = book.Publisher,
        Price = book.Price,

      };
      return new Response<GetBookResponse>()
      {
        Success = true,
        Message = "Book retrieved",
        Data = bookResponse
      };
    }

    public async Task<PaginatedResponse<GetBookResponse>> GetBooksAsync(PaginationRequest request)
    {

      (int count, IEnumerable<Book> books) = await _context.Books
        .ToPaginatedResultAsync(request.PageNumber, request.PageSize);

      IEnumerable<GetBookResponse> booksResponse = books.Select(
        x => new GetBookResponse()
        {
          Id = x.Id,
          Title = x.Title,
          Author = x.Author,
          Publisher = x.Publisher,
          Price = x.Price,
          Description = x.Description,
          PublishedOn = x.PublishedOn
        }
        ).AsEnumerable();

      return new PaginatedResponse<GetBookResponse>()
      {
        Success = true,
        Message = "Books retrieved",
        Data = booksResponse,
        TotalRecords = count
      };
    }

    public async Task<Response<Guid>> DeleteBookAsync(Guid id)
    {
      Book book = await _context.Books.FindAsync(id);

      if (book is null)
        throw new NotFoundException("Book not found");
      _context.Books.Remove(book);
      await _context.SaveChangesAsync();
      return new Response<Guid>()
      {
        Success = true,
        Message = "Book deleted",
        Data = book.Id
      };
    }
  }
}