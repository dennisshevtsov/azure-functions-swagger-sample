namespace AzureFunctionsSwaggerSample.Api.Functions
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;

  using AzureFunctionsSwaggerSample.Api.Dtos;
  using AzureFunctionsSwaggerSample.Api.Services;

  public sealed class GetTodoListFunction
  {
    private readonly ITodoService _todoService;

    public GetTodoListFunction(ITodoService todoService)
      => _todoService = todoService ?? throw new ArgumentNullException(nameof(todoService));

    [FunctionName(nameof(GetTodoListFunction))]
    public async Task<GetTodoListResponseDto> RunAsync(
      [HttpTrigger("get", Route = "todo/{todoListId}")] HttpRequest request,
      Guid todoListId,
      CancellationToken cancellationToken)
      => await _todoService.GetTodoListAsync(todoListId, cancellationToken);
  }
}
