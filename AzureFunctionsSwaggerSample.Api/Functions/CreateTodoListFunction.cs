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
  using AzureFunctionsSwaggerSample.Api.Dtos;
  using AzureFunctionsSwaggerSample.Api.Services;

  /// <summary>Provides a method to handle an HTTP request.</summary>
  public sealed class CreateTodoListFunction
  {
    private readonly ISerializationService _serializationService;
    private readonly ITodoService _todoService;

    /// <summary>Initializes a new instance of the <see cref="AzureFunctionsSwaggerSample.Api.Functions.CreateTodoListFunction"/> class.</summary>
    /// <param name="serializationService">An object that provides a simple API to serialize/deserialize an object.</param>
    /// <param name="todoService">An object that provides a simple API to execute operation within objects of the <see cref="AzureFunctionsSwaggerSample.Api.Documents.TodoListDocument"/> class.</param>
    public CreateTodoListFunction(
      ISerializationService serializationService,
      ITodoService todoService)
    {
      _serializationService = serializationService ?? throw new ArgumentNullException(nameof(serializationService));
      _todoService = todoService ?? throw new ArgumentNullException(nameof(todoService));
    }

    /// <summary>CreateTodoListFunction</summary>
    /// <group>TODO List</group>
    /// <remarks>Creates a TODO list.</remarks>
    /// <param name="request">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <param name="command" required="true" in="body"><see cref="AzureFunctionsSwaggerSample.Api.Dtos.CreateTodoListRequestDto"/>An object that represents data to update a TODO list.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    /// <verb>post</verb>
    /// <url>http://localhost:7071/api/todo</url>
    /// <response code="201"><see cref="AzureFunctionsSwaggerSample.Api.Dtos.CreateTodoListResponseDto"/>An object that represents detail of a TODO list.</response>
    [FunctionName(nameof(CreateTodoListFunction))]
    public async Task<IActionResult> ExecuteAsync(
      [HttpTrigger("post", Route = "todo")] HttpRequest request,
      [CosmosDB("%DatabaseId%", "%CollectionId%", ConnectionStringSetting = "ConnectionString",
        Id = "todoListId", PartitionKey = nameof(TodoListDocument))] IAsyncCollector<TodoListDocument> collector,
      CancellationToken cancellationToken)
    {
      var todoListId = Guid.NewGuid();
      var command = await _serializationService.DeserializeAsync<CreateTodoListRequestDto>(
        request.Body, cancellationToken);

      await _todoService.CreateTodoListAsync(todoListId, command, collector, cancellationToken);

      return new ObjectResult(new CreateTodoListResponseDto(todoListId))
      {
        StatusCode = StatusCodes.Status201Created,
      };
    }
  }
}
