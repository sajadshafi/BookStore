using Application.IServices;
using Application.Services;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application {
  public static class ApplicationDI {
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration) {
      
      services.AddServices(configuration);
      
      return services;
    }
    
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration) {

      services.AddScoped<IBookService, BookService>();
      
      return services;
    }
  }
  
}