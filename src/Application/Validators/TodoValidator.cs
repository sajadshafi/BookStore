using FluentValidation;
using Dummy.Application.DTOs;

namespace Application.Validators;

public class TodoValidator : AbstractValidator<TodoDTO> {
  public TodoValidator() {
    RuleFor(x => x.Title).NotNull().NotEmpty();
  }
}
