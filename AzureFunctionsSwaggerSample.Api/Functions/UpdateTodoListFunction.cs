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

  /// <summary>Provides a method to handle an HTTP request.</summary>
  public sealed class UpdateTodoListFunction
  {
    private readonly ITodoService _todoService;
    private readonly ISerializationService _serializationService;

    /// <summary>Initializes a new instance of the <see cref="AzureFunctionsSwaggerSample.Api.Functions.UpdateTodoListFunction"/> class.</summary>
    /// <param name="todoService">An object that provides a simple API to operate within the TODO list domain.</param>
    /// <param name="serializationService">An object that provides a simple API to serialize/deserialize an object.</param>
    public UpdateTodoListFunction(
      ITodoService todoService,
      ISerializationService serializationService)
    {
      _todoService = todoService ?? throw new ArgumentNullException(nameof(todoService));
      _serializationService = serializationService ?? throw new ArgumentNullException(nameof(serializationService));
    }

    /// <summary>UpdateTodoListFunction</summary>
    /// <group>TODO List</group>
    /// <remarks>Updates a TODO list.</remarks>
    /// <param name="request">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="todoListId" required="true" cref="System.Guid" in="path">A value that represents an ID of TODO list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <param name="command" required="true" in="body"><see cref="AzureFunctionsSwaggerSample.Api.Dtos.UpdateTodoListRequestDto"/>An object that represents data to update a TODO list.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    /// <verb>put</verb>
    /// <url>http://localhost:7071/api/todo/{todoListId}</url>
    /// <response code="204"></response>
    [FunctionName(nameof(UpdateTodoListFunction))]
    public async Task ExecuteAsync(
      [HttpTrigger("put", Route = "todo/{todoListId}")] HttpRequest request,
      Guid todoListId,
      CancellationToken cancellationToken)
    {
      var command = await _serializationService.DeserializeAsync<UpdateTodoListRequestDto>(
        request.Body, cancellationToken);

      command.TodoListId = todoListId;

      await _todoService.UpdateTodoListAsync(command, cancellationToken);
    }
  }
}
