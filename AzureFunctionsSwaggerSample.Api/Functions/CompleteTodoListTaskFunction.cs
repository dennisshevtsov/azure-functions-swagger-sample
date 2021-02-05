// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Functions
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;

  using AzureFunctionsSwaggerSample.Api.Dtos;
  using AzureFunctionsSwaggerSample.Api.Services;

  public sealed class CompleteTodoListTaskFunction
  {
    private readonly ITodoService _todoService;
    private readonly ISerializationService _serializationService;

    public CompleteTodoListTaskFunction(
      ITodoService todoService,
      ISerializationService serializationService)
    {
      _todoService = todoService ?? throw new ArgumentNullException(nameof(todoService));
      _serializationService = serializationService ?? throw new ArgumentNullException(nameof(serializationService));
    }

    [FunctionName(nameof(CompleteTodoListTaskFunction))]
    public async Task ExecuteAsync(
      [HttpTrigger("post", Route = "todo/{todoListId}/task/{taskId}/complete")] HttpRequest request,
      Guid todoListId,
      Guid taskId,
      CancellationToken cancellationToken)
    {
      var command = await _serializationService.DeserializeAsync<CompleteTodoListTaskRequestDto>(
        request.Body, cancellationToken);

      command.TodoListId = todoListId;
      command.TaskId = taskId;

      await _todoService.CompleteTodoListTaskAsync(command, cancellationToken);
    }
  }
}
