using Domain.Features.Books;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context {
  public class BookStoreContext : DbContext {

    public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options) {}
    
    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder
        .Entity<Book>()
        .ToTable("books");
    }
  }
}