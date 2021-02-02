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

  public sealed class GetTodoListsFunction
  {
    private readonly ITodoService _todoService;

    public GetTodoListsFunction(ITodoService todoService)
      => _todoService = todoService ?? throw new ArgumentNullException(nameof(todoService));

    [FunctionName(nameof(GetTodoListsFunction))]
    public async Task<GetTodoListsResponseDto> RunAsync(
      [HttpTrigger("get", Route = "todo")] HttpRequest request,
      CancellationToken cancellationToken)
      => await _todoService.GetTodoListsAsync(cancellationToken);
  }
}
