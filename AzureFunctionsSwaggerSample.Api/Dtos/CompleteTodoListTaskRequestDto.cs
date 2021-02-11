// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Dtos
{
  using System;
  using System.Linq;

  using AzureFunctionsSwaggerSample.Api.Documents;

  /// <summary>Represents data to complete a TODO list task.</summary>
  public sealed class CompleteTodoListTaskRequestDto
  {
    /// <summary>Gets/sets a value that represents an ID of a TODO list task.</summary>
    public Guid TaskId { get; set; }

    /// <summary>Updates an instance of the <see cref="AzureFunctionsSwaggerSample.Api.Documents.TodoListDocument"/>.</summary>
    /// <param name="todoListDocument">An instance of the <see cref="AzureFunctionsSwaggerSample.Api.Documents.TodoListDocument"/>.</param>
    public void UpdateDocument(TodoListDocument todoListDocument)
    {
      var todoListTaskDocument = todoListDocument.Tasks.FirstOrDefault(
        document => document.TaskId == TaskId);

      if (todoListTaskDocument != null)
      {
        todoListTaskDocument.Completed = true;
      }
    }
  }
}
