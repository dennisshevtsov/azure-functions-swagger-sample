// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Dtos
{
  using System;

  /// <summary>Represents data to create a TODO list task.</summary>
  public sealed class CreateTodoListTaskRequestDto
  {
    /// <summary>Gets/sets a value that represents a title of a TODO list task.</summary>
    public string Title { get; set; }

    /// <summary>Gets/sets a value that represents a descriptino of a TODO list task.</summary>
    public string Description { get; set; }

    /// <summary>Gets/sets a value that represents a deadline of a TODO list task.</summary>
    public DateTime Deadline { get; set; }
  }
}
