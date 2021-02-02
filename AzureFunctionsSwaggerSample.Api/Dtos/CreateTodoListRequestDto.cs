// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Dtos
{
  public sealed class CreateTodoListRequestDto
  {
    public string Title { get; set; }

    public string Description { get; set; }
  }
}
