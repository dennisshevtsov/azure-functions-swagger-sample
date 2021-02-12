﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Functions
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;

  using AzureFunctionsSwaggerSample.Api.Documents;
  using AzureFunctionsSwaggerSample.Api.Dtos;
  using AzureFunctionsSwaggerSample.Api.Services;

  /// <summary>Provides a method to handle an HTTP request.</summary>
  public sealed class CreateTodoListTaskFunction
  {
    private readonly ISerializationService _serializationService;

    /// <summary>Initializes a new instance of the <see cref="AzureFunctionsSwaggerSample.Api.Functions.CreateTodoListTaskFunction"/> class.</summary>
    /// <param name="serializationService">An object that provides a simple API to serialize/deserialize an object.</param>
    public CreateTodoListTaskFunction(
      ISerializationService serializationService)
    {
      _serializationService = serializationService ?? throw new ArgumentNullException(nameof(serializationService));
    }

    /// <summary>CreateTodoListTaskFunction</summary>
    /// <group>TODO List Task</group>
    /// <remarks>Creates a TODO list task.</remarks>
    /// <param name="request">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="todoListId" required="true" cref="System.Guid" in="path">A value that represents an ID of TODO list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <param name="command" required="true" in="body"><see cref="AzureFunctionsSwaggerSample.Api.Dtos.CreateTodoListTaskRequestDto"/>An object that represents data to update a TODO list.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    /// <verb>post</verb>
    /// <url>http://localhost:7071/api/todo/{todoListId}/task</url>
    /// <response code="200"><see cref="AzureFunctionsSwaggerSample.Api.Dtos.CreateTodoListTaskResponseDto"/>An object that represents detail of a TODO list task.</response>
    [FunctionName(nameof(CreateTodoListTaskFunction))]
    public async Task<CreateTodoListTaskResponseDto> ExecuteAsync(
      [HttpTrigger("post", Route = "todo/{todoListId}/task")] HttpRequest request,
      [CosmosDB("%DatabaseId%", "%CollectionId%",
        ConnectionStringSetting = "ConnectionString")] IAsyncCollector<TodoListDocument> collector,
      [CosmosDB("%DatabaseId%", "%CollectionId%", ConnectionStringSetting = "ConnectionString",
        Id = "{todoListId}", PartitionKey = nameof(TodoListDocument))] TodoListDocument todoListDocument,
      Guid todoListId,
      CancellationToken cancellationToken)
    {
      var command = await _serializationService.DeserializeAsync<CreateTodoListTaskRequestDto>(
        request.Body, cancellationToken);
      var todoListTaskDocument = command.ToDocument();

      var tasks = new List<TodoListTaskDocument>(todoListDocument.Tasks ?? Enumerable.Empty<TodoListTaskDocument>());

      tasks.Add(todoListTaskDocument);
      todoListDocument.Tasks = tasks;

      await collector.AddAsync(todoListDocument, cancellationToken);

      var response = CreateTodoListTaskResponseDto.FromDocument(todoListTaskDocument);

      return response;
    }
  }
}
