// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Functions
{
  using System;
  using System.Text.Json;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;

  using AzureFunctionsSwaggerSample.Api.Dtos;
  using AzureFunctionsSwaggerSample.Api.Services;

  public sealed class CreateTodoListFunction
  {
    private readonly ITodoService _todoService;

    public CreateTodoListFunction(ITodoService todoService)
      => _todoService = todoService ?? throw new ArgumentNullException(nameof(todoService));

    [FunctionName(nameof(CreateTodoListFunction))]
    public async Task<CreateTodoListResponseDto> RunAsync(
      [HttpTrigger("post", Route = "todo")] HttpRequest request,
      CancellationToken cancellationToken)
    {
      var command = await JsonSerializer.DeserializeAsync<CreateTodoListRequestDto>(
        request.Body,
        new JsonSerializerOptions
        {
          PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        },
        cancellationToken);

      return await _todoService.CreateTodoListAsync(command, cancellationToken);
    }
  }
}
