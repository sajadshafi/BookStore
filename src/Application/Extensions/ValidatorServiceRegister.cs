using Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Dummy.Application.Extensions;

public static class ValidatorServiceRegister {
  public static IServiceCollection AddValidators(this IServiceCollection services) {
    services.AddValidatorsFromAssemblyContaining<TodoValidator>();
    return services;
  }
}
