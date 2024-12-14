#nullable enable
using Application.Contracts.Books;

namespace Application.Validations {
  public class BookValidations {
    public static (bool, string?) IsCreateBookRequestValid(CreateBookRequest request) {
      if(string.IsNullOrEmpty(request.Title))
        return (false, "Title is required");
      
      if(string.IsNullOrEmpty(request.Author))
        return (false, "Author is required");

      if(request.Price < 0)
        return (false, "Price cannot be negative");
      
      if(request.PublishedOn > DateTime.Now)
        return (false, "Published on must be before the current date");
      
      return (true, null);
    }
  }
}