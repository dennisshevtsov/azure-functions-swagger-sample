// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Dtos
{
  using System;

  /// <summary>Represents detail of a TODO list task.</summary>
  public sealed class CreateTodoListTaskResponseDto
  {
    public CreateTodoListTaskResponseDto(Guid todoListTaskId) => TodoListTaskId = todoListTaskId;

    /// <summary>Gets/sets a value that represents and ID of a TODO list task.</summary>
    public Guid TodoListTaskId { get; set; }
  }
}
