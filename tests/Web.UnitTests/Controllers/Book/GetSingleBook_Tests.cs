using Application.Contracts;
using Application.Contracts.Books;
using Application.IServices;
using Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Controllers;

namespace Web.Tests.Controllers.Book;

public class GetSingleBookTests
{

  private readonly Mock<IBookService> _mockBookService;
  private readonly BooksController _sut;

  public GetSingleBookTests()
  {
    _mockBookService = new Mock<IBookService>();
    _sut = new BooksController(_mockBookService.Object);
  }

  [Fact]
  public async Task Create_InvokesBookServices_ExactlyOnce()
  {
    // Arrange
    var book = BookFixtures.GetSingleBookResponse();

    // Act
    _mockBookService.Setup(s =>
        s.GetBookAsync(book.Id))
      .ReturnsAsync(new Response<GetBookResponse>()
      {
        Data = book,
        Success = true,
        Message = ""
      });
    var result = await _sut.Get(book.Id) as OkObjectResult;

    // Assert
    _mockBookService.Verify(service => service.GetBookAsync(book.Id), Times.Once);
  }

  [Fact]
  public async Task Get_ShouldReturnBadRequest_WhenIdIsNull()
  {
    // Arrange

    // Act
    var result = await _sut.Get((Guid?)null);

    // Assert
    result.Should().NotBeNull()
      .And.BeOfType<BadRequestResult>();
  }

  [Fact]
  public async Task Get_ShouldReturnOk_WhenIdIsValid()
  {
    // Arrange
    var expectedBook = BookFixtures.GetSingleBookResponse();
    var expectedResponse = new Response<GetBookResponse>()
    {
      Success = true,
      Data = expectedBook,
      Message = "Success",
    };
    _mockBookService.Setup(s => s.GetBookAsync(expectedBook.Id))
      .ReturnsAsync(expectedResponse);

    // Act
    var result = await _sut.Get(expectedBook.Id);

    // Assert
    result.Should().NotBeNull()
      .And.BeOfType<OkObjectResult>()
      .Which.Value.Should().BeEquivalentTo(expectedResponse);
  }
}