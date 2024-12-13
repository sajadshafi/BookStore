using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure {
  public static class InfrastructureDI {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
      
      services.AddDatabaseService(configuration);
      
      return services;
    }

    public static IServiceCollection AddDatabaseService(this IServiceCollection services, IConfiguration configuration) {
      
      string connectionString = configuration.GetConnectionString("BookStoreConnection");

      if(string.IsNullOrEmpty(connectionString)) {
        throw new ArgumentException("Connection string is missing");
      }

      services.AddDbContext<BookStoreContext>(options => 
        options.UseNpgsql(connectionString)
          .UseSnakeCaseNamingConvention()
      );
      
      return services;
    }
  }
}