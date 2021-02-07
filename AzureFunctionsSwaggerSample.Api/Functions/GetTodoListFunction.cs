// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Functions
{
  using System;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;

  using AzureFunctionsSwaggerSample.Api.Documents;
  using AzureFunctionsSwaggerSample.Api.Dtos;
  using AzureFunctionsSwaggerSample.Api.Services;

  /// <summary>Provides a method to handle an HTTP request.</summary>
  public sealed class GetTodoListFunction
  {
    private readonly ITodoService _todoService;

    /// <summary>Initializes a new instance of the <see cref="AzureFunctionsSwaggerSample.Api.Functions.GetTodoListFunction"/> class.</summary>
    /// <param name="todoService">An object that provides a simple API to operate within the TODO list domain.</param>
    public GetTodoListFunction(ITodoService todoService)
      => _todoService = todoService ?? throw new ArgumentNullException(nameof(todoService));

    /// <summary>GetTodoListFunction</summary>
    /// <group>TODO List</group>
    /// <remarks>Gets a TODO list.</remarks>
    /// <param name="request">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="response">An object that represents detail of a TODO list.</param>
    /// <param name="todoListId" required="true" cref="System.Guid" in="path">A value that represents an ID of TODO list.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    /// <verb>get</verb>
    /// <url>http://localhost:7071/api/todo/{todoListId}</url>
    /// <response code="200"><see cref="AzureFunctionsSwaggerSample.Api.Dtos.GetTodoListResponseDto"/>An object that represents detail of a TODO list task.</response>
    [FunctionName(nameof(GetTodoListFunction))]
    public GetTodoListResponseDto Execute(
      [HttpTrigger("get", Route = "todo/{todoListId}")] HttpRequest request,
      [CosmosDB("{databaseId}", "{collectionId}", ConnectionStringSetting = "{connectionString}",
        Id = "todoListId", PartitionKey = nameof(TodoListDocument))] GetTodoListResponseDto response,
      Guid todoListId) => response;
  }
}
