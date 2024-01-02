using Dummy.DTOs;

namespace Dummy.Interfaces;

public interface ITodoService {
  Task<Response<int>> DeleteAsync(int id);

  #region Functionality
  Task<Response<List<TodoDTO>>> GetAsync();
  Task<Response<TodoDTO>> GetAsync(int id);
  #endregion

}