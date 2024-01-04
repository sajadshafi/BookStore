using FluentAssertions;

namespace Tests.Core;

public class Test {

  [Fact]
  public void DummyTest() {
    // Given

    // When
    int result = 4;

    // Then
    result.Should().Be(4);
  }
}