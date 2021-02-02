using System;
using System.Collections.Generic;

namespace AzureFunctionsSwaggerSample.Api.Dtos
{
  public sealed class GetTodoListResponseDto
  {
    public Guid TodoListId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public IEnumerable<GetTodoListTaskResponseDto> Tasks { get; set; }
  }
}
