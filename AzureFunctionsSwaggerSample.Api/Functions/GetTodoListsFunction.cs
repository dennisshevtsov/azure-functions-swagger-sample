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
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    /// <verb>get</verb>
    /// <url>http://localhost:7071/api/todo</url>
    /// <response code="200"><see cref="AzureFunctionsSwaggerSample.Api.Dtos.GetTodoListsResponseDto"/>An object that represents detail of a TODO list task.</response>
    [FunctionName(nameof(GetTodoListsFunction))]
    public async Task<GetTodoListsResponseDto> ExecuteAsync(
      [HttpTrigger("get", Route = "todo")] HttpRequest request,
      CancellationToken cancellationToken)
      => await _todoService.GetTodoListsAsync(cancellationToken);
  }
}
