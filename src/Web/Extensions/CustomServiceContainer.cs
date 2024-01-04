using Dummy.Infrastructure.Interfaces;
using Dummy.Infrastructure.Services;

namespace Dummy.Web.Extensions;

public static class CustomServiceContainer {
  public static IServiceCollection AddCustomServices(this IServiceCollection services) {

    services.AddTransient<ITodoService, TodoService>();

    return services;
  }
}