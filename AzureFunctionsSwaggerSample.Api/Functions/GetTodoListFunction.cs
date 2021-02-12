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
      [CosmosDB("%DatabaseId%", "%CollectionId%", ConnectionStringSetting = "ConnectionString",
        Id = "{todoListId}", PartitionKey = nameof(TodoListDocument))] GetTodoListResponseDto response,
      Guid todoListId) => response;
  }
}
