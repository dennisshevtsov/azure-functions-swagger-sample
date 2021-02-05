// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Dtos
{
  using System;
  using System.Collections.Generic;

  /// <summary>Represents detail of a TODO list.</summary>
  public sealed class GetTodoListResponseDto
  {
    /// <summary>Gets/sets a value that represents an ID of a TODO list</summary>
    public Guid TodoListId { get; set; }

    /// <summary>Gets/sets a value that represents a title of a TODO list</summary>
    public string Title { get; set; }

    /// <summary>Gets/sets a value that represents a description of a TODO list</summary>
    public string Description { get; set; }

    /// <summary>Gets/sets an object that represents a collection of TODO tasks.</summary>
    public IEnumerable<GetTodoListTaskResponseDto> Tasks { get; set; }
  }
}
