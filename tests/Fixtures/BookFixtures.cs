
using Application.Contracts.Books;

namespace Fixtures;

public class BookFixtures {
  public static List<GetBookResponse> GetBookList(int pageNumber = 1, int pageSize = 10) {
    IEnumerable<GetBookResponse> books = [
        new GetBookResponse() { Id = Guid.NewGuid(), Title="Deep work", Description="lorem ipsum", Author = "Jhon Carter", Publisher = "Penguin Publishers", PublishedOn = DateTime.Now, Price = 250},
        new GetBookResponse() { Id = Guid.NewGuid(), Title="Digital Minimalism", Description="lorem ipsum", Author = "Jhon Carter", Publisher = "Penguin Publishers", PublishedOn = DateTime.Now, Price = 250},
        new GetBookResponse() { Id = Guid.NewGuid(), Title="Atomic Habits", Description="lorem ipsum", Author = "Jhon Carter", Publisher = "Penguin Publishers", PublishedOn = DateTime.Now, Price = 250},
        new GetBookResponse() { Id = Guid.NewGuid(), Title="System Design", Description="lorem ipsum", Author = "Jhon Carter", Publisher = "Penguin Publishers", PublishedOn = DateTime.Now, Price = 250},
      ];
    
    return books
      .Skip((pageNumber - 1) * pageSize)
      .Take(pageSize)
      .ToList();
  }

  public static CreateBookRequest GetSingleBook() {
    return new CreateBookRequest() {
      Title = "Cracking the Coding Interview",
      Description = "Book to learn how to crack coding interview",
      Author = "Gayle",
      Publisher = "PHI",
      PublishedOn = DateTime.Now,
      Price = 1250
    };
  }
  
  public static GetBookResponse GetSingleBookResponse() {
    return new GetBookResponse() {
      Id = Guid.NewGuid(),
      Title = "Cracking the Coding Interview",
      Description = "Book to learn how to crack coding interview",
      Author = "Gayle",
      Publisher = "PHI",
      PublishedOn = DateTime.Now,
      Price = 1250
    };
  }
}
