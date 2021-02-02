using System.Collections.Generic;

namespace AzureFunctionsSwaggerSample.Api.Dtos
{
  public sealed class GetTodoListsResponseDto
  {
    public IEnumerable<GetTodoListsItemResponseDto> Items { get; set; }
  }
}
