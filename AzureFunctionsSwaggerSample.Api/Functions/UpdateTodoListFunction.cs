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

  public sealed class UpdateTodoListFunction
  {
    private readonly ITodoService _todoService;
    private readonly ISerializationService _serializationService;

    public UpdateTodoListFunction(
      ITodoService todoService,
      ISerializationService serializationService)
    {
      _todoService = todoService ?? throw new ArgumentNullException(nameof(todoService));
      _serializationService = serializationService ?? throw new ArgumentNullException(nameof(serializationService));
    }

    [FunctionName(nameof(UpdateTodoListFunction))]
    public async Task RunAsync(
      [HttpTrigger("put", Route = "todo/{todoListId}")] HttpRequest request,
      Guid todoListId,
      CancellationToken cancellationToken)
    {
      var command = await _serializationService.DeserializeAsync<UpdateTodoListRequestDto>(
        request.Body, cancellationToken);

      command.TodoListId = todoListId;

      await _todoService.UpdateProductAsync(command, cancellationToken);
    }
  }
}
