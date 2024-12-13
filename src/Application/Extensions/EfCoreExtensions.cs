using Microsoft.EntityFrameworkCore;

namespace Application.Extensions {
  public static class EfCoreExtensions {
    public static async Task<(int, IEnumerable<T>)> ToPaginatedResultAsync<T>(this IQueryable<T> queryable, int pageNumber, int pageSize) where T : class {
      int count = queryable.Count();
      int skip = (pageNumber - 1) * pageSize;

      List<T> items = await queryable.Skip(skip).Take(pageSize).ToListAsync();
      
      return (count, items.AsEnumerable());
    }
  }
}