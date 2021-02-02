namespace AzureFunctionsSwaggerSample.Api.Dtos
{
  using System;

  public sealed class CompleteTodoListTaskRequestDto
  {
    public Guid TodoListId { get; set; }

    public Guid TaskId { get; set; }
  }
}
