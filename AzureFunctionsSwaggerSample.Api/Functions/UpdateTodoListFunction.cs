// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

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

  public sealed class UpdateTodoListFunction
  {
    private readonly ITodoService _todoService;

    public UpdateTodoListFunction(ITodoService todoService)
      => _todoService = todoService ?? throw new ArgumentNullException(nameof(todoService));

    [FunctionName(nameof(UpdateTodoListFunction))]
    public async Task RunAsync(
      [HttpTrigger("put", Route = "todo/{todoListId}")] HttpRequest request,
      Guid todoListId,
      CancellationToken cancellationToken)
    {
      var command = await JsonSerializer.DeserializeAsync<UpdateTodoListRequestDto>(
        request.Body,
        new JsonSerializerOptions
        {
          PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        },
        cancellationToken);

      command.TodoListId = todoListId;

      await _todoService.UpdateProductAsync(command, cancellationToken);
    }
  }
}
