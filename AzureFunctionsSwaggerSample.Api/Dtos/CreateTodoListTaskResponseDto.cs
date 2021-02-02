// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Dtos
{
  using System;

  public sealed class CreateTodoListTaskResponseDto
  {
    public Guid TaskId { get; set; }
  }
}
