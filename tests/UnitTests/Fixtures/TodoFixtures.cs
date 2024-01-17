using Dummy.Application.DTOs;

namespace Dummy.Tests.Fixtures;

public class TodoFixtures {
  public static List<TodoDTO> GetTodoList() {
    return [
        new TodoDTO() { Id=1, Title="Bring eggs from market", Description="lorem ipsum", IsCompleted = false },
        new TodoDTO() { Id=2, Title="Visit bank for ATM", Description="lorem ipsum", IsCompleted = false },
        new TodoDTO() { Id=3, Title="Visit dentist this week", Description="lorem ipsum", IsCompleted = true },
        new TodoDTO() { Id=4, Title="Read 10 pages from Atomic Habbits", Description="lorem ipsum", IsCompleted = false },
      ];
  }

  public static TodoDTO GetSingleTodo(int id = 1) {
    return new TodoDTO() {
      Id = id,
      Title = $"{id} Bring eggs from market",
      Description = $"{id} lorem ipsum",
      IsCompleted = false
    };
  }
}
