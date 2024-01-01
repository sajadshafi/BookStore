using Dummy.DTOs;

namespace Dummy.Interfaces;

public interface ITodoService {

  #region Functionality
  Response<TodoDTO> GetAsync();
  #endregion

}