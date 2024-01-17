using Dummy.Application.DTOs;

namespace Dummy.Helpers;

public static class ValidationHelpers {
  public static bool IsValidTodo(this TodoDTO model) {
    if(string.IsNullOrWhiteSpace(model.Title)) {
      return false;
    }

    return true;
  }
}