using Dummy.Interfaces;
using Dummy.Services;

namespace Dummy.Extensions;

public static class CustomServiceContainer {
  public static IServiceCollection AddCustomServices(this IServiceCollection services) {

    services.AddTransient<ITodoService, TodoService>();

    return services;
  }
}