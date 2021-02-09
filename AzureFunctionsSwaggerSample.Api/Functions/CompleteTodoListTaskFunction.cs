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
  using AzureFunctionsSwaggerSample.Api.Documents;

  /// <summary>Provides a method to handle an HTTP request.</summary>
  public sealed class CompleteTodoListTaskFunction
  {
    private readonly ISerializationService _serializationService;

    /// <summary>Initializes a new instance of the <see cref="AzureFunctionsSwaggerSample.Api.Functions.CompleteTodoListTaskFunction"/> class.</summary>
    /// <param name="todoService">An object that provides a simple API to operate within the TODO list domain.</param>
    /// <param name="serializationService">An object that provides a simple API to serialize/deserialize an object.</param>
    public CompleteTodoListTaskFunction(
      ISerializationService serializationService)
    {
      _serializationService = serializationService ?? throw new ArgumentNullException(nameof(serializationService));
    }

    /// <summary>CompleteTodoListTaskFunction</summary>
    /// <group>TODO List Task</group>
    /// <remarks>Marks a TODO list task as completed.</remarks>
    /// <param name="request">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="todoListId" required="true" cref="System.Guid" in="path">An object that represents an ID of a TODO list.</param>
    /// <param name="taskId" required="true" cref="System.Guid" in="path">An object that represents an ID of a TODO list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    /// <verb>post</verb>
    /// <url>http://localhost:7071/api/todo/{todoListId}/task/{taskId}/complete</url>
    /// <response code="204"></response>
    [FunctionName(nameof(CompleteTodoListTaskFunction))]
    public async Task ExecuteAsync(
      [HttpTrigger("post", Route = "todo/{todoListId}/task/{taskId}/complete")] HttpRequest request,
      [CosmosDB] IAsyncCollector<TodoListDocument> collector,
      [CosmosDB] TodoListDocument document,
      Guid todoListId,
      Guid taskId,
      CancellationToken cancellationToken)
    {
      var command = await _serializationService.DeserializeAsync<CompleteTodoListTaskRequestDto>(
        request.Body, cancellationToken);

      command.TodoListId = todoListId;
      command.TaskId = taskId;

      await collector.AddAsync(document, cancellationToken);
    }
  }
}
