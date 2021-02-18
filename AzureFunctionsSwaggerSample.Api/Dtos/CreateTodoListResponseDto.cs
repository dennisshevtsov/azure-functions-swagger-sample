// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Dtos
{
  using System;

  /// <summary>Represents detail of a TODO list.</summary>
  public sealed class CreateTodoListResponseDto
  {
    /// <summary>Initializes a new instance of the <see cref="AzureFunctionsSwaggerSample.Api.Dtos.CreateTodoListResponseDto"/> class.</summary>
    /// <param name="todoListId">A value that represents an ID of a TODO list.</param>
    public CreateTodoListResponseDto(Guid todoListId) => TodoListId = todoListId;

    /// <summary>Gets/sets a value that represents an ID of a TODO list.</summary>
    public Guid TodoListId { get; }
  }
}
