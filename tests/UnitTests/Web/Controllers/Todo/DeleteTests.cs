using Dummy.Controllers;
using Dummy.Application.DTOs;
using Dummy.Infrastructure.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace Dummy.UnitTests.Controllers.Todo;

public class DeleteTests {

  #region Private Fields
  private readonly TodoController _sut;
  private readonly Mock<ITodoService> _mockTodoService;
  #endregion

  #region Constructors 
  public DeleteTests() {
    _mockTodoService = new Mock<ITodoService>();
    _sut = new TodoController(_mockTodoService.Object);
  }
  #endregion

  [Theory]
  [InlineData(0)]
  [InlineData(1)]
  [InlineData(2)]
  public void Delete_ReturnsNotFound_When_ItemNotAvailable(int id) {
    // Arrange
    var expected = new Response<int>() {
      Success = false,
      Message = "Item not found",
      Data = default,
    };
    _mockTodoService.Setup(service => service.DeleteAsync(id)).ReturnsAsync(expected);

    // Act
    var result = _sut.Delete(id).Result;

    // Assert
    result.Should().NotBeNull().And.BeOfType<NotFoundResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
  }

  [Theory]
  [InlineData(1)]
  [InlineData(2)]
  public void Delete_ReturnsOK_When_ItemDeleted(int id) {
    // Arrange
    var expected = new Response<int>() {
      Success = true,
      Message = "Item deleted",
      Data = id,
    };
    _mockTodoService.Setup(service => service.DeleteAsync(id)).ReturnsAsync(expected);

    // Act
    var result = _sut.Delete(id).Result;

    // Assert
    result.Should().NotBeNull().And.BeOfType<OkObjectResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
  }

  [Theory]
  [InlineData(0)]
  [InlineData(1)]
  public void Delete_InvokesTodoServiceExactlyOnce(int id) {
    // Arrange
    var expected = new Response<int>();
    _mockTodoService.Setup(service => service.DeleteAsync(id)).ReturnsAsync(expected);

    // Act
    var result = _sut.Delete(id).Result;

    // Then
    _mockTodoService.Verify(service => service.DeleteAsync(id), Times.Once);
  }
}