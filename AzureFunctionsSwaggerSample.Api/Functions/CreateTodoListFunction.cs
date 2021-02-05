﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
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
  public sealed class CreateTodoListFunction
  {
    private readonly ITodoService _todoService;
    private readonly ISerializationService _serializationService;

    /// <summary>Initializes a new instance of the <see cref="AzureFunctionsSwaggerSample.Api.Functions.CreateTodoListFunction"/> class.</summary>
    /// <param name="todoService">An object that provides a simple API to operate within the TODO list domain.</param>
    /// <param name="serializationService">An object that provides a simple API to serialize/deserialize an object.</param>
    public CreateTodoListFunction(
      ITodoService todoService,
      ISerializationService serializationService)
    {
      _todoService = todoService ?? throw new ArgumentNullException(nameof(todoService));
      _serializationService = serializationService ?? throw new ArgumentNullException(nameof(serializationService));
    }

    [FunctionName(nameof(CreateTodoListFunction))]
    public async Task<CreateTodoListResponseDto> ExecuteAsync(
      [HttpTrigger("post", Route = "todo")] HttpRequest request,
      CancellationToken cancellationToken)
    {
      var command = await _serializationService.DeserializeAsync<CreateTodoListRequestDto>(
        request.Body, cancellationToken);

      return await _todoService.CreateTodoListAsync(command, cancellationToken);
    }
  }
}
