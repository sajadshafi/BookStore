using Dummy.Controllers;
using Dummy.DTOs;
using Dummy.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using Fixtures;

namespace Dummy.UnitTests.Controllers.Todo;

public class GetSingle_Tests {

  #region Private Fields
  private readonly TodoController _sut;
  private readonly Mock<ITodoService> _mockTodoService;

  #endregion
  #region Constructors
  public GetSingle_Tests() {

    _mockTodoService = new Mock<ITodoService>();
    _sut = new TodoController(_mockTodoService.Object);

  }
  #endregion

  [Fact]
  public void GetSingle_OnSuccess_Returns200() {
    // Given
    _mockTodoService.Setup(service => service.GetAsync(1)).ReturnsAsync(new Response<TodoDTO>() {
      Success = true,
      Message = "Item found",
      Data = TodoFixtures.GetSingleTodo()
    });

    // When
    var result = _sut.Get(1).Result;

    // Then
    result.Should().NotBeNull();
    result.Should().BeOfType<OkObjectResult>();
    var resultObject = (OkObjectResult)result;
    resultObject.StatusCode.Should().Be((int)HttpStatusCode.OK);
  }

  [Fact]
  public void GetSingle_OnNoItem_ReturnsNotFound() {
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

  [Fact]
  public void GetSingle_OnSuccess_ReturnsSingleItem() {
    // Arrange
    _mockTodoService.Setup(service => service.GetAsync(1)).ReturnsAsync(new Response<TodoDTO>() {
      Success = true,
      Message = "Item found",
      Data = TodoFixtures.GetSingleTodo(),
    });

    // Act
    var result = _sut.Get(1).Result;

    // Assert
    result.Should().NotBeNull();
    result.Should().BeOfType<OkObjectResult>();
    var resultObject = (OkObjectResult)result;

    resultObject.StatusCode.Should().Be((int)HttpStatusCode.OK);
    resultObject.Value.Should().BeOfType(typeof(Response<TodoDTO>));
    var responseValue = (Response<TodoDTO>)resultObject.Value;

    responseValue.Data.Should().NotBeNull();
    responseValue.Data.Should().NotBe(default(TodoDTO));
  }
}