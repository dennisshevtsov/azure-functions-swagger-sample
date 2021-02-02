using System;

namespace AzureFunctionsSwaggerSample.Api.Documents
{
  public sealed class TodoListTaskDocument
  {
    public Guid TaskId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime Deadline { get; set; }

    public bool Done { get; set; }
  }
}
