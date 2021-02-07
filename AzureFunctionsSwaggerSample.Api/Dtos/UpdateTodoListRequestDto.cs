// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Dtos
{
  using System;
  
  using AzureFunctionsSwaggerSample.Api.Documents;
  
  /// <summary>Represents data to update a TODO list.</summary>
  public sealed class UpdateTodoListRequestDto
  {
    /// <summary>Gets/sets a value that represents an ID of a TODO list.</summary>
    public Guid TodoListId { get; set; }

    /// <summary>Gets/sets a value that represents a title of a TODO list.</summary>
    public string Title { get; set; }

    /// <summary>Gets/sets a value that represents a description of a TODO list.</summary>
    public string Description { get; set; }

    /// <summary>Updates an instance of the <see cref="AzureFunctionsSwaggerSample.Api.Documents.TodoListDocument"/> class.</summary>
    /// <param name="document">An instance of the <see cref="AzureFunctionsSwaggerSample.Api.Documents.TodoListDocument"/> class.</param>
    public void UpdateDocument(TodoListDocument document)
    {
      document.Title = Title;
      document.Description = Description;
    }
  }
}
