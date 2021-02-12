// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Dtos
{
  using System;

  /// <summary>Represents detail of a TODO list.</summary>
  public sealed class CreateTodoListResponseDto
  {
    public CreateTodoListResponseDto(Guid todoListId) => TodoListId = todoListId;

    /// <summary>Gets/sets a value that represents an ID of a TODO list.</summary>
    public Guid TodoListId { get; }
  }
}
