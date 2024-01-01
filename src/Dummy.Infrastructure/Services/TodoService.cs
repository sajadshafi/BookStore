using Dummy.DTOs;
using Dummy.Interfaces;

namespace Dummy.Services;

public class TodoService : ITodoService {
  public Response<TodoDTO> GetAsync() {
    throw new NotImplementedException();
  }
}