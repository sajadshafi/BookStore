namespace Application.Contracts {
  public class Response<T> {
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
  }

  public class Response {
    public bool Success { get; set; }
    public string Message { get; set; }
  }

  public class PaginatedResponse<T> : Response<T> {
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public new IEnumerable<T> Data { get; set; } = [];
    public int TotalRecords { get; set; }
  }
}