namespace Application.Contracts.Books {
  public class CreateBookRequest {
    public string Title { get; set; }
    public string Description { get; set; }
    public string Author { get; set; }
    public string Publisher { get; set; }
    public DateTime PublishedOn { get; set; }
    public decimal Price { get; set; }
  }
}