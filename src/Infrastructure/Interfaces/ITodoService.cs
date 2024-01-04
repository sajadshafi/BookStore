using Dummy.Application.DTOs;

namespace Dummy.Infrastructure.Interfaces;

public interface ITodoService {
  Task<Response<TodoDTO>> AddAsync(TodoDTO todo);
  Task<Response<int>> DeleteAsync(int id);

  #region Functionality
  Task<Response<List<TodoDTO>>> GetAsync();
  Task<Response<TodoDTO>> GetAsync(int id);
  #endregion

}