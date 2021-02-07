// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Dtos
{
  using System;

  using AzureFunctionsSwaggerSample.Api.Documents;

  /// <summary>Represents detail of a TODO list.</summary>
  public sealed class CreateTodoListResponseDto
  {
    /// <summary>Gets/sets a value that represents an ID of a TODO list.</summary>
    public Guid TodoListId { get; set; }

    /// <summary>Creates an instance of the <see cref="AzureFunctionsSwaggerSample.Api.Dtos.CreateTodoListResponseDto"/> class from an instance of the <see cref="AzureFunctionsSwaggerSample.Api.Documents.TodoListDocument"/> class.</summary>
    /// <param name="document">An instance of the <see cref="AzureFunctionsSwaggerSample.Api.Documents.TodoListDocument"/> class.</param>
    /// <returns>An instance of the <see cref="AzureFunctionsSwaggerSample.Api.Dtos.CreateTodoListResponseDto"/> class.</returns>
    public static CreateTodoListResponseDto FromDocument(TodoListDocument document)
      => new CreateTodoListResponseDto
      {
        TodoListId = document.TodoListId,
      };
  }
}
