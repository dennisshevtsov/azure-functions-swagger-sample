// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Dtos
{
  using System.Collections.Generic;

  /// <summary>Represents an object that wraps a collection of TODO lists.</summary>
  public sealed class GetTodoListsResponseDto
  {
    /// <summary>Gets/sets an object that represents a collection of TODO lists.</summary>
    public IEnumerable<GetTodoListsItemResponseDto> Items { get; set; }
  }
}
