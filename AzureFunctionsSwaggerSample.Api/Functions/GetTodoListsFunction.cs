﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Functions
{
  using System;
  using System.Collections.Generic;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;

  using AzureFunctionsSwaggerSample.Api.Documents;
  using AzureFunctionsSwaggerSample.Api.Dtos;
  using AzureFunctionsSwaggerSample.Api.Services;

  /// <summary>Provides a method to handle an HTTP request.</summary>
  public sealed class GetTodoListsFunction
  {
    private readonly ITodoService _todoService;

    /// <summary>Initializes a new instance of the <see cref="AzureFunctionsSwaggerSample.Api.Functions.GetTodoListsFunction"/> class.</summary>
    /// <param name="todoService">An object that provides a simple API to operate within the TODO list domain.</param>
    public GetTodoListsFunction(ITodoService todoService)
      => _todoService = todoService ?? throw new ArgumentNullException(nameof(todoService));

    /// <summary>GetTodoListsFunction</summary>
    /// <group>TODO List</group>
    /// <remarks>Gets TODO lists.</remarks>
    /// <param name="request">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="response">An object that represents an object that wraps a collection of TODO lists.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    /// <verb>get</verb>
    /// <url>http://localhost:7071/api/todo</url>
    /// <response code="200"><see cref="System.Collections.Generic.IEnumerable{T}"/> where T is <see cref="AzureFunctionsSwaggerSample.Api.Dtos.GetTodoListsItemResponseDto"/> An object that represents detail of a TODO list task.</response>
    [FunctionName(nameof(GetTodoListsFunction))]
    public IEnumerable<GetTodoListsItemResponseDto> ExecuteAsync(
      [HttpTrigger("get", Route = "todo")] HttpRequest request,
      [CosmosDB("{databaseId}", "{collectionId}", ConnectionStringSetting = "{connectionString}",
        PartitionKey = nameof(TodoListDocument))] IEnumerable<GetTodoListsItemResponseDto> response)
      => response;
  }
}
