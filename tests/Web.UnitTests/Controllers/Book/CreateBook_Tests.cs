using Application.Contracts;
using Application.Contracts.Books;
using Application.IServices;
using Application.Validations;
using Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using Web.Controllers;

namespace Web.Tests.Controllers.Book;

public class CreateBookTests
{

  #region Private fields
  private readonly BooksController _sut;
  private readonly Mock<IBookService> _mockBookService;
  #endregion

  #region Constructors  
  public CreateBookTests()
  {
    _mockBookService = new Mock<IBookService>();
    _sut = new BooksController(_mockBookService.Object);
  }
  #endregion

  #region Test Cases for Create book functionality of BookController

  [Fact]
  public async Task Create_InvokesBookServices_ExactlyOnce()
  {
    // Arrange
    var book = BookFixtures.GetSingleBook();

    // Act
    _mockBookService.Setup(s =>
        s.CreateBookAsync(book))
          .ReturnsAsync(new Response<Guid>()
          {
            Data = Guid.NewGuid(),
            Success = true,
            Message = ""
          });
    var result = await _sut.Create(book) as OkObjectResult;

    // Assert
    _mockBookService.Verify(service => service.CreateBookAsync(book), Times.Once);
  }
  [Fact]
  public async Task Create_ReturnsOK_When_BookCreated()
  {
    // Arrange
    Guid bookId = Guid.NewGuid();
    var book = BookFixtures.GetSingleBook();
    var expected = new Response<Guid>()
    {
      Success = true,
      Message = "Book created",
      Data = bookId
    };
    _mockBookService
      .Setup(service => service.CreateBookAsync(book))
      .ReturnsAsync(expected);

    // Act
    var result = await _sut.Create(book) as OkObjectResult;

    // Assert
    result.Should().NotBeNull()
      .And.BeOfType<OkObjectResult>()
      .Which.Value.Should().BeEquivalentTo(expected);
  }

  [Fact]
  public async Task Add_ReturnsBadRequest_When_ValidationFails()
  {
    // Arrange
    var book = BookFixtures.GetSingleBook();
    book.Title = "";
    string expectedMessage = "Title is required";

    // Act
    var result = await _sut.Create(book);

    // Assert
    result.Should().NotBeNull().And.BeOfType<BadRequestObjectResult>()
        .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

    // Extract and assert the error message
    var badRequestResult = result as BadRequestObjectResult;

    // Assert
    result.Should().NotBeNull()
      .And.BeOfType<BadRequestObjectResult>()
      .Which.Value.Should().Be(expectedMessage);
  }
  #endregion
}