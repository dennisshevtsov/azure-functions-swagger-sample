// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Dtos
{
  using System;

  using AzureFunctionsSwaggerSample.Api.Documents;

  /// <summary>Represents detail of a TODO list task.</summary>
  public sealed class CreateTodoListTaskResponseDto
  {
    /// <summary>Gets/sets a value that represents and ID of a TODO list task.</summary>
    public Guid TaskId { get; set; }

    /// <summary>Converts an instance of the <see cref="AzureFunctionsSwaggerSample.Api.Documents.TodoListTaskDocument"/> class to an instance of the <see cref="AzureFunctionsSwaggerSample.Api.Dtos.CreateTodoListTaskResponseDto"/> class.</summary>
    /// <param name="document">An instance of the <see cref="AzureFunctionsSwaggerSample.Api.Documents.TodoListTaskDocument"/> class</param>
    /// <returns>An instance of the <see cref="AzureFunctionsSwaggerSample.Api.Dtos.CreateTodoListTaskResponseDto"/> class.</returns>
    public static CreateTodoListTaskResponseDto FromDocument(TodoListTaskDocument document)
      => new CreateTodoListTaskResponseDto
      {
        TaskId = document.TaskId,
      };
  }
}
