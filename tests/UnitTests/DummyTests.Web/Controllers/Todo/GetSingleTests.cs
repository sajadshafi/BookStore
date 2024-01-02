using Dummy.Controllers;
using Dummy.DTOs;
using Dummy.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using Fixtures;

namespace Dummy.UnitTests.Controllers.Todo;

public class GetSingleTests {

  #region Private Fields
  private readonly TodoController _sut;
  private readonly Mock<ITodoService> _mockTodoService;

  #endregion
  #region Constructors
  public GetSingleTests() {

    _mockTodoService = new Mock<ITodoService>();
    _sut = new TodoController(_mockTodoService.Object);

  }
  #endregion

  [Fact]
  public void GetSingle_ReturnsOK_When_ItemFound() {
    // Given
    _mockTodoService.Setup(service => service.GetAsync(1)).ReturnsAsync(new Response<TodoDTO>() {
      Success = true,
      Message = "Item found",
      Data = TodoFixtures.GetSingleTodo()
    });

    // When
    var result = _sut.Get(1).Result as OkObjectResult;

    // Then
    result.Should().NotBeNull().And.BeOfType<OkObjectResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
    result.Value.Should().NotBeNull().And.BeOfType<Response<TodoDTO>>().Which.Data.Should().NotBe(default).And.NotBeNull();
  }

  [Fact]
  public void GetSingle_ReturnsNotFound_When_ItemNotAvailable() {
    // Given
    _mockTodoService.Setup(service => service.GetAsync(1)).ReturnsAsync(new Response<TodoDTO>() {
      Success = true,
      Message = "Item found",
      Data = default
    });

    // When
    var result = _sut.Get(1).Result;

    // Then
    result.Should().NotBeNull();
    result.Should().BeOfType<NotFoundResult>();
    var resultObject = (NotFoundResult)result;
    resultObject.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
  }

  [Theory]
  [InlineData(1)]
  [InlineData(2)]
  [InlineData(3)]
  public void GetSingle_InvokesTodoServicesExactlyOnce(int id) {
    // Arrange
    var expected = new Response<TodoDTO>() {
      Success = true,
      Message = "Item found",
      Data = TodoFixtures.GetSingleTodo(id),
    };
    // Act
    _mockTodoService.Setup(service => service.GetAsync(id)).ReturnsAsync(expected);
    var result = _sut.Get(id).Result as OkObjectResult;

    // Assert
    _mockTodoService.Verify(service => service.GetAsync(id), Times.Once);
  }

}