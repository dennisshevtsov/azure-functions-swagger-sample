
namespace AzureFunctionsSwaggerSample.Api.Dtos
{
  using System;

  public sealed class CreateTodoListTaskRequestDto
  {
    public Guid TodoListId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime Deadline { get; set; }
  }
}
