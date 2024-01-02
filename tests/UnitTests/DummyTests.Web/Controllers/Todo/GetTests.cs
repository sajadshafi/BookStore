using System.Net;
using Dummy.Controllers;
using Dummy.DTOs;
using Dummy.Interfaces;
using Fixtures;
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
    var dummyTodos = TodoFixtures.GetTodoList();
    mockTodoService.Setup(s => s.GetAsync()).ReturnsAsync(new Response<List<TodoDTO>>() {
      Success = true,
      Message = "Items found",
      Data = dummyTodos
    });

    // Act


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
    var dummyTodos = TodoFixtures.GetTodoList();
    mockTodoService.Setup(s => s.GetAsync()).ReturnsAsync(new Response<List<TodoDTO>>() {
      Success = true,
      Message = "Items found",
      Data = dummyTodos,
    });

    // Act

    var result = sut.Get().Result as OkObjectResult;

    // Assert
    result.Should().NotBeNull();
    result.Should().BeOfType<OkObjectResult>();
    result?.Value.Should().BeOfType<Response<List<TodoDTO>>>();

    var responseValue = (Response<List<TodoDTO>>)result.Value;

    responseValue.Data.Count.Should().BeGreaterThan(0);
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