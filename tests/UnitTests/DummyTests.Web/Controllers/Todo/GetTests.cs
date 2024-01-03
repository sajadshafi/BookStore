using System.Net;
using Dummy.Controllers;
using Dummy.DTOs;
using Dummy.Interfaces;
using Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Dummy.UnitTests.Controllers.Todo;

public class GetTests {

  #region Private Fields
  private readonly TodoController _sut;
  private readonly Mock<ITodoService> _mockTodoService;

  #endregion
  #region Constructors
  public GetTests() {

    _mockTodoService = new Mock<ITodoService>();
    _sut = new TodoController(_mockTodoService.Object);

  }
  #endregion

  #region Test Cases for Get functionality of TodoController

  [Fact]
  public void Get_ReturnsOK_When_ItemsFound() {
    // Arrange
    var dummyTodos = TodoFixtures.GetTodoList();
    var expected = new Response<List<TodoDTO>>() {
      Success = true,
      Message = "Items found",
      Data = dummyTodos
    };
    _mockTodoService.Setup(s => s.GetAsync()).ReturnsAsync(expected);

    // Act
    var result = _sut.Get().Result as OkObjectResult;

    // Assert
    result.Should().NotBeNull().And.BeOfType<OkObjectResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
    result.Value.Should().BeOfType<Response<List<TodoDTO>>>().Which.Data.Should().NotBeEmpty();
  }

  [Fact]
  public void Get_ReturnsNotFound_When_NoItemsAvailable() {
    // Arrange
    var expected = new Response<List<TodoDTO>>() {
      Success = true,
      Data = [],
      Message = "No task items available",
    };
    _mockTodoService.Setup(s => s.GetAsync()).ReturnsAsync(expected);

    // Act
    var result = _sut.Get().Result;

    // Assert
    result.Should().NotBeNull().And.BeOfType<NotFoundResult>();
  }

  [Fact]
  public void Get_InvokesTodoServicesExactlyOnce() {
    // Arrange

    // Act
    _mockTodoService.Setup(s => s.GetAsync()).ReturnsAsync(new Response<List<TodoDTO>>());
    var result = _sut.Get().Result as OkObjectResult;

    // Assert
    _mockTodoService.Verify(service => service.GetAsync(), Times.Once);
  }
  #endregion
}