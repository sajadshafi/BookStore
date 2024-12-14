using Application.Contracts;
using Application.Contracts.Base;
using Application.Contracts.Books;
using Application.IServices;
using Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Controllers;

namespace Web.Tests.Controllers.Book;

public class GetPaginatedBooksTests
{

  #region Private Fields
  private readonly Mock<IBookService> _mockBookService;
  private readonly BooksController _sut;
  #endregion
  #region Constructors
  public GetPaginatedBooksTests()
  {
    _mockBookService = new Mock<IBookService>();
    _sut = new BooksController(_mockBookService.Object);
  }
  #endregion

  #region Test Cases for Get functionality of TodoController

  [Fact]
  public async Task Create_InvokesBookServices_ExactlyOnce()
  {
    // Arrange
    var books = BookFixtures.GetBookList();
    var payload = new PaginationRequest(1, 10);

    // Act
    _mockBookService.Setup(s =>
        s.GetBooksAsync(payload))
      .ReturnsAsync(new PaginatedResponse<GetBookResponse>()
      {
        Data = books,
        Success = true,
        Message = ""
      });
    var result = await _sut.Get(payload) as OkObjectResult;

    // Assert
    _mockBookService.Verify(service => service.GetBooksAsync(payload), Times.Once);
  }

  [Theory]
  [InlineData(-1)]
  [InlineData(0)]
  public async Task Get_ShouldReturnBadRequest_WhenPageNumberIsInvalid(int pageNumber)
  {
    // Arrange
    var payload = new PaginationRequest(pageNumber, 10);
    string expectedMessage = "Page number must be greater than 0";

    // Act
    var result = await _sut.Get(payload);

    // Assert
    result.Should().NotBeNull()
      .And.BeOfType<BadRequestObjectResult>()
      .Which.Value.Should().Be(expectedMessage);
  }
  [Theory]
  [InlineData(-1)]
  [InlineData(0)]
  public async Task Get_ShouldReturnBadRequest_WhenPageSizeIsInvalid(int pageSize)
  {
    // Arrange
    var payload = new PaginationRequest(1, pageSize);
    string expectedMessage = "Page size must be greater than 0";

    // Act
    var result = await _sut.Get(payload);

    // Assert
    result.Should().NotBeNull()
      .And.BeOfType<BadRequestObjectResult>()
      .Which.Value.Should().Be(expectedMessage);
  }

  [Theory]
  [InlineData(1, 10)]
  [InlineData(1, 6)]
  [InlineData(2, 4)]
  public async Task Get_ShouldReturnOk_WhenPayloadIsValid(int pageNumber, int pageSize)
  {
    // Arrange
    var expectedBooks = BookFixtures.GetBookList(pageNumber, pageSize);
    var payload = new PaginationRequest(pageNumber, pageSize);
    var expectedResponse = new PaginatedResponse<GetBookResponse>()
    {
      Success = true,
      Data = expectedBooks,
      Message = "Success",
      TotalRecords = expectedBooks.Count
    };
    _mockBookService.Setup(s => s.GetBooksAsync(payload))
      .ReturnsAsync(expectedResponse);

    // Act
    var result = await _sut.Get(payload);

    // Assert
    result.Should().NotBeNull()
      .And.BeOfType<OkObjectResult>()
      .Which.Value.Should().BeEquivalentTo(expectedResponse);
  }
  #endregion
}