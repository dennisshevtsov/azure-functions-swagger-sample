namespace AzureFunctionsSwaggerSample.Api.Dtos
{
  using System;

  public sealed class GetTodoListsItemResponseDto
  {
    public Guid TodoListId { get; set; }

    public string Title { get; set; }
  }
}
