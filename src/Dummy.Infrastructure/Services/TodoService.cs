using Dummy.DTOs;
using Dummy.Interfaces;

namespace Dummy.Services;

public class TodoService : ITodoService {
  public async Task<Response<List<TodoDTO>>> GetAsync() {
    return new Response<List<TodoDTO>>() {
      Success = true,
      Message = "Items found",
      Data = [
        new TodoDTO() { Id=1, Title="Bring eggs from market", Description="lorem ipsum", IsCompleted = false },
        new TodoDTO() { Id=2, Title="Visit bank for ATM", Description="lorem ipsum", IsCompleted = false },
        new TodoDTO() { Id=3, Title="Visit dentist this week", Description="lorem ipsum", IsCompleted = true },
        new TodoDTO() { Id=4, Title="Read 10 pages from Atomic Habbits", Description="lorem ipsum", IsCompleted = false },
      ]

    };
  }
}