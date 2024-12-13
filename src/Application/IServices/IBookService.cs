using Application.Contracts;
using Application.Contracts.Base;
using Application.Contracts.Books;

namespace Application.IServices {
  public interface IBookService {
    Task<Response<Guid>> CreateBookAsync(CreateBookRequest request);
    Task<Response<Guid>> UpdateBookAsync(UpdateBookRequest request);
    Task<Response<GetBookResponse>> GetBookAsync(Guid id);
    Task<PaginatedResponse<GetBookResponse>> GetBooksAsync(PaginationRequest request);
    Task<Response<Guid>> DeleteBookAsync(Guid id);
  }
}