using Domain.Abstractions;

namespace Domain.Features.Books {
  public class Book : BaseEntity {
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Author { get; private set; }
    public string Publisher { get; private set; }
    public DateTime PublishedOn { get; set; }
    public decimal Price { get; private set; }

    public static Book Create(
      string title, 
      string description,
      string author, 
      string publisher, 
      decimal price, 
      DateTime publishedOn
    ) {

      if(publishedOn >= DateTime.Now) {
        throw new ArgumentException("Published On date cannot be higher than todays");
      }
      
      ArgumentException.ThrowIfNullOrEmpty(title);
      ArgumentException.ThrowIfNullOrEmpty(author);
      
      return new Book {
        Title = title,
        Description = description,
        Author = author,
        Publisher = publisher,
        PublishedOn = publishedOn,
        Price = price
      };
    }
    
    public void Update(
      string title, 
      string description,
      string author, 
      string publisher, 
      decimal price, 
      DateTime publishedOn
    ) {

      if(publishedOn >= DateTime.Now) {
        throw new ArgumentException("Published On date cannot be higher than todays");
      }
      
      ArgumentException.ThrowIfNullOrEmpty(title);
      ArgumentException.ThrowIfNullOrEmpty(author);
      
      this.Title = title;
      this.Description = description;
      this.Author = author;
      this.Publisher = publisher;
      this.PublishedOn = publishedOn;
      this.Price = price;
    }
  }
}