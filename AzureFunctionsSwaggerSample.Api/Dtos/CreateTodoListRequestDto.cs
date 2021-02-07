﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Dtos
{
  using System;

  using AzureFunctionsSwaggerSample.Api.Documents;

  /// <summary>Represents data to create a TODO list.</summary>
  public sealed class CreateTodoListRequestDto
  {
    /// <summary>Gets/sets a value that represents a title of a TODO list.</summary>
    public string Title { get; set; }

    /// <summary>Gets/sets a value that represents a description of a TODO list.</summary>
    public string Description { get; set; }

    /// <summary>Converts to an instance of the <see cref="AzureFunctionsSwaggerSample.Api.Documents.TodoListDocument"/> class.</summary>
    /// <returns>An instance of the <see cref="AzureFunctionsSwaggerSample.Api.Documents.TodoListDocument"/> class.</returns>
    public TodoListDocument ToDocument() => new TodoListDocument
    {
      TodoListId = Guid.NewGuid(),
      PartitionId = nameof(TodoListDocument),
      Title = Title,
      Description = Description,
    };
  }
}
