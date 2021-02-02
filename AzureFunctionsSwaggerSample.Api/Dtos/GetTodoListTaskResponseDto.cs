namespace AzureFunctionsSwaggerSample.Api.Dtos
{
  using System;

  public sealed class GetTodoListTaskResponseDto
  {
    public Guid TaskId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime Deadline { get; set; }

    public bool Done { get; set; }
  }
}
