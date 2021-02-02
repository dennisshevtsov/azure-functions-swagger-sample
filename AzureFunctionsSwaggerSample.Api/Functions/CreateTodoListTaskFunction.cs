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

  public sealed class CreateTodoListTaskFunction
  {
    private readonly ITodoService _todoService;

    public CreateTodoListTaskFunction(ITodoService todoService)
      => _todoService = todoService ?? throw new ArgumentNullException(nameof(todoService));

    [FunctionName(nameof(CreateTodoListTaskFunction))]
    public async Task<CreateTodoListTaskResponseDto> RunAsync(
      [HttpTrigger("post", Route = "todo/{todoListId}")] HttpRequest request,
      Guid todoListId,
      CancellationToken cancellationToken)
    {
      var command = await JsonSerializer.DeserializeAsync<CreateTodoListTaskRequestDto>(
        request.Body,
        new JsonSerializerOptions
        {
          PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        },
        cancellationToken);

      command.TodoListId = todoListId;

      return await _todoService.CreateTodoListTaskAsync(command, cancellationToken);
    }
  }
}
