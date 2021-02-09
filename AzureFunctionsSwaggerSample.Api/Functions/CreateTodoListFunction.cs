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
  using AzureFunctionsSwaggerSample.Api.Documents;

  /// <summary>Provides a method to handle an HTTP request.</summary>
  public sealed class CreateTodoListFunction
  {
    private readonly ISerializationService _serializationService;

    /// <summary>Initializes a new instance of the <see cref="AzureFunctionsSwaggerSample.Api.Functions.CreateTodoListFunction"/> class.</summary>
    /// <param name="serializationService">An object that provides a simple API to serialize/deserialize an object.</param>
    public CreateTodoListFunction(
      ISerializationService serializationService)
    {
      _serializationService = serializationService ?? throw new ArgumentNullException(nameof(serializationService));
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
    /// <response code="204"></response>
    [FunctionName(nameof(CreateTodoListFunction))]
    public async Task<CreateTodoListResponseDto> ExecuteAsync(
      [HttpTrigger("post", Route = "todo")] HttpRequest request,
      [CosmosDB("{databaseId}", "{collectionId}",
        ConnectionStringSetting = "{connectionString}")] IAsyncCollector<TodoListDocument> collector,
      CancellationToken cancellationToken)
    {
      var command = await _serializationService.DeserializeAsync<CreateTodoListRequestDto>(
        request.Body, cancellationToken);
      var document = command.ToDocument();

      await collector.AddAsync(document, cancellationToken);

      var response = CreateTodoListResponseDto.FromDocument(document);

      return response;
    }
  }
}
