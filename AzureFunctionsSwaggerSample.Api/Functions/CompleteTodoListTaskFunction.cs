// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Functions
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Azure.WebJobs;

  using AzureFunctionsSwaggerSample.Api.Documents;
  using AzureFunctionsSwaggerSample.Api.Services;

  /// <summary>Provides a method to handle an HTTP request.</summary>
  public sealed class CompleteTodoListTaskFunction
  {
    private readonly ITodoService _todoService;

    /// <summary>Initializes a new instance of the <see cref="AzureFunctionsSwaggerSample.Api.Functions.CompleteTodoListTaskFunction"/> class.</summary>
    /// <param name="todoService">An object that provides a simple API to execute operation within objects of the <see cref="AzureFunctionsSwaggerSample.Api.Documents.TodoListDocument"/> class.</param>
    public CompleteTodoListTaskFunction(ITodoService todoService)
      => _todoService = todoService ?? throw new ArgumentNullException(nameof(todoService));

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
    /// <response code="400"></response>
    [FunctionName(nameof(CompleteTodoListTaskFunction))]
    public async Task<IActionResult> ExecuteAsync(
      [HttpTrigger("post", Route = "todo/{todoListId}/task/{taskId}/complete")] HttpRequest request,
      [CosmosDB("%DatabaseId%", "%CollectionId%",
        ConnectionStringSetting = "ConnectionString")] IAsyncCollector<TodoListDocument> collector,
      [CosmosDB("%DatabaseId%", "%CollectionId%", ConnectionStringSetting = "ConnectionString",
        Id = "{todoListId}", PartitionKey = nameof(TodoListDocument))] TodoListDocument todoListDocument,
      Guid todoListId,
      Guid taskId,
      CancellationToken cancellationToken)
    {
      if (todoListDocument == null || todoListDocument.Tasks == null)
      {
        return new BadRequestResult();
      }

      await _todoService.CompleteTodoListTaskAsync(taskId, todoListDocument, collector, cancellationToken);

      return new NoContentResult();
    }
  }
}
