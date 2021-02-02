using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunctionsSwaggerSample.Api.Documents
{
  public sealed class TodoListDocument
  {
    public Guid TodoListId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public IEnumerable<TodoListTaskDocument> Tasks { get; set; }
  }
}
