// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Documents
{
  using System;

  /// <summary>Represents detail of a TODO list task.</summary>
  public sealed class TodoListTaskDocument
  {
    /// <summary>Gets/sets a value that represents an ID of a TODO list task.</summary>
    public Guid TaskId { get; set; }

    /// <summary>Gets/sets a value that represents a title of a TODO list task.</summary>
    public string Title { get; set; }

    /// <summary>Gets/sets a value that represents a description of a TODO list task.</summary>
    public string Description { get; set; }

    /// <summary>Gets/sets a value that represents a deadline of a TODO list task.</summary>
    public DateTime Deadline { get; set; }

    /// <summary>Gets/sets a value that indicates if a TODO list task is completed.</summary>
    public bool Completed { get; set; }
  }
}
