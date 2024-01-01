using Dummy.DTOs;

namespace Dummy.Interfaces;

public interface ITodoService {

  #region Functionality
  Task<Response<List<TodoDTO>>> GetAsync();
  #endregion

}