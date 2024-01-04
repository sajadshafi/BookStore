namespace Dummy.Application.DTOs;

public class Response<T> {
  public string Message { get; set; }
  public T Data { get; set; }
  public bool Success { get; set; }
}

public class PagedResponse<T> {
  public string Message { get; set; }
  public bool Success { get; set; }
  public int PageNumber { get; set; }
  public int TotalPages { get; set; }
  public int PageSize { get; set; }
}