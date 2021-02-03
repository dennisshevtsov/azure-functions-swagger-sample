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

  public sealed class CreateTodoListTaskFunction
  {
    private readonly ITodoService _todoService;
    private readonly ISerializationService _serializationService;

    public CreateTodoListTaskFunction(
      ITodoService todoService,
      ISerializationService serializationService)
    {
      _todoService = todoService ?? throw new ArgumentNullException(nameof(todoService));
      _serializationService = serializationService ?? throw new ArgumentNullException(nameof(serializationService));
    }

    [FunctionName(nameof(CreateTodoListTaskFunction))]
    public async Task<CreateTodoListTaskResponseDto> RunAsync(
      [HttpTrigger("post", Route = "todo/{todoListId}")] HttpRequest request,
      Guid todoListId,
      CancellationToken cancellationToken)
    {
      var command = await _serializationService.DeserializeAsync<CreateTodoListTaskRequestDto>(
        request.Body, cancellationToken);

      command.TodoListId = todoListId;

      return await _todoService.CreateTodoListTaskAsync(command, cancellationToken);
    }
  }
}
