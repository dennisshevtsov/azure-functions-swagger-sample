// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Dtos
{
  using System;

  /// <summary>Represents data to complete a TODO list task.</summary>
  public sealed class CompleteTodoListTaskRequestDto
  {
    /// <summary>Gets/sets a value that represents an ID of a TODO list.</summary>
    public Guid TodoListId { get; set; }

    /// <summary>Gets/sets a value that represents an ID of a TODO list task.</summary>
    public Guid TaskId { get; set; }
  }
}
