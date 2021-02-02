



namespace AzureFunctionsSwaggerSample.Api.Functions
{
  using System;
  using System.Text.Json;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;

  using AzureFunctionsSwaggerSample.Api.Dtos;
  using AzureFunctionsSwaggerSample.Api.Services;

  public sealed class CompleteTodoListTaskFunction
  {
    private readonly ITodoService _todoService;

    public CompleteTodoListTaskFunction(ITodoService todoService)
      => _todoService = todoService ?? throw new ArgumentNullException(nameof(todoService));

    [FunctionName(nameof(CompleteTodoListTaskFunction))]
    public async Task RunAsync(
      [HttpTrigger("post", Route = "todo/{todoListId}/task/{taskId}/complete")] HttpRequest request,
      Guid todoListId,
      Guid taskId,
      CancellationToken cancellationToken)
    {
      var command = await JsonSerializer.DeserializeAsync<CompleteTodoListTaskRequestDto>(
        request.Body,
        new JsonSerializerOptions
        {
          PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        },
        cancellationToken);

      command.TodoListId = todoListId;
      command.TaskId = taskId;

      await _todoService.CompleteTodoListTaskAsync(command, cancellationToken);
    }
  }
}
