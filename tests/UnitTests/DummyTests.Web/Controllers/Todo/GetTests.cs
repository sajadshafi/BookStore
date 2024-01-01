using System.Net;
using Dummy.Controllers;
using Dummy.DTOs;
using Dummy.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Dummy.UnitTests.Controllers.Todo;

public class Get_Tests {
  [Fact]
  public void Get_OnSuccess_ReturnsOK() {
    // Arrange
    var mockTodoService = new Mock<ITodoService>();
    var sut = new TodoController(mockTodoService.Object);

    // Act
    mockTodoService.Setup(s => s.GetAsync()).ReturnsAsync(new Response<List<TodoDTO>>() {
      Success = true,
      Message = "Items found",
      //Data = TodoFixtures
    });
    var result = sut.Get().Result as OkObjectResult;

    // Assert
    result.Should().NotBeNull();
    result.Should().BeOfType<OkObjectResult>();

    result?.StatusCode.Should().Be((int)HttpStatusCode.OK);
  }

  [Fact]
  public void Get_OnSuccess_ResponseOfListOfTodos() {
    // Arrange
    var mockTodoService = new Mock<ITodoService>();
    var sut = new TodoController(mockTodoService.Object);

    // Act
    mockTodoService.Setup(s => s.GetAsync()).ReturnsAsync(new Response<List<TodoDTO>>() {
      Success = true,
      Message = "Items found",
      //Data = TodoFixtures.GetTodoList(),
    });

    var result = sut.Get().Result as OkObjectResult;

    // Assert
    result.Should().NotBeNull();
    result.Should().BeOfType<OkObjectResult>();
    result?.Value.Should().BeOfType<Response<List<TodoDTO>>>();
  }

  [Fact]
  public void Get_OnEmptyResult_ReturnsNotFound() {
    // Arrange
    var mockTodoService = new Mock<ITodoService>();
    var sut = new TodoController(mockTodoService.Object);

    // Act
    mockTodoService.Setup(s => s.GetAsync()).ReturnsAsync(new Response<List<TodoDTO>>() {
      Success = true,
      Data = [],
      Message = "No task items available",
    });
    var result = sut.Get().Result;

    // Assert
    result.Should().NotBeNull();
    result.Should().BeOfType<NotFoundResult>();
  }

  [Fact]
  public void Get_OnSuccess_InvokesTodoServicesExactlyOnce() {
    // Arrange
    var mockTodoService = new Mock<ITodoService>();
    var sut = new TodoController(mockTodoService.Object);

    // Act
    mockTodoService.Setup(s => s.GetAsync()).ReturnsAsync(new Response<List<TodoDTO>>());
    var result = sut.Get().Result as OkObjectResult;

    // Assert
    mockTodoService.Verify(service => service.GetAsync(), Times.Once);
  }
}