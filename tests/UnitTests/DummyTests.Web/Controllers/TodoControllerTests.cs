using System.Net;
using Dummy.Controllers;
using Dummy.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Dummy.Tests.Controllers;

public class TodoControllerTests {
  [Fact]
  public void Get_OnSuccess_ReturnsOK() {
    // Arrange
    var mockTodoService = new Mock<ITodoService>();
    var sut = new TodoController();

    // Act
    var result = sut.Get();

    // Assert
    result.Should().BeOfType<OkObjectResult>();

    var resultObject = (OkObjectResult)result;
    resultObject.StatusCode.Should().Be((int)HttpStatusCode.OK);
  }
}
