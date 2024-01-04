using Dummy.Controllers;
using Dummy.Application.DTOs;
using Dummy.Infrastructure.Interfaces;
using Dummy.Tests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace Dummy.UnitTests.Controllers.Todo;

public class AddTests {

  #region Private fields
  private readonly TodoController _sut;
  private readonly Mock<ITodoService> _mockTodoService;
  #endregion

  #region Constructors  
  public AddTests() {
    _mockTodoService = new Mock<ITodoService>();
    _sut = new TodoController(_mockTodoService.Object);
  }
  #endregion

  #region Test Cases for Add functionality of TodoController

  [Fact]
  public void Get_InvokesTodoServicesExactlyOnce() {
    // Arrange
    var todo = TodoFixtures.GetSingleTodo();

    // Act
    _mockTodoService.Setup(s => s.GetAsync()).ReturnsAsync(new Response<List<TodoDTO>>());
    var result = _sut.Add(todo).Result as OkObjectResult;

    // Assert
    _mockTodoService.Verify(service => service.AddAsync(todo), Times.Once);
  }

  /// <summary>
  /// input model => { Id = 0, Title = "Something", Description = "Some description", IsCompleted = false }
  /// expected => { Id = 25, Title = "Something", Description = "Some Description", IsCompleted = false }
  /// </summary>
  [Fact]
  public void Add_ReturnsOK_When_TodoAdded() {
    // Arrange
    var todo = TodoFixtures.GetSingleTodo(0);
    var expected = new Response<TodoDTO>() {
      Success = true,
      Message = "Item added",
      Data = TodoFixtures.GetSingleTodo(23)
    };
    _mockTodoService.Setup(service => service.AddAsync(todo)).ReturnsAsync(expected);

    // Act
    var result = _sut.Add(todo).Result as OkObjectResult;

    // Assert
    result.Should().NotBeNull().And.BeOfType<OkObjectResult>()
      .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

    result.Value.Should().NotBeNull().And.BeOfType<Response<TodoDTO>>()
      .Which.Data.Should().NotBeNull().And.BeOfType<TodoDTO>()
      .Which.Id.Should().NotBe(default).And.NotBe(0);
  }
  #endregion

  [Fact]
  public void Add_ReturnsBadRequest_When_TitleIsEmpty() {
    // Arrange
    var todo = TodoFixtures.GetSingleTodo(0);
    todo.Title = "";

    // Act
    var result = _sut.Add(todo).Result;

    // Assert
    result.Should().NotBeNull().And.BeOfType<BadRequestResult>()
        .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
  }
}