// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Dtos
{
  using System;

  using Newtonsoft.Json;

  /// <summary>Represents detail of a TODO list.</summary>
  public sealed class GetTodoListsItemResponseDto
  {
    /// <summary>Gets/sets a value that represents an ID of a TODO list.</summary>
    [JsonProperty("id")]
    public Guid TodoListId { get; set; }

    /// <summary>Gets/sets a value that represents a title of a TODO list.</summary>
    [JsonProperty("title")]
    public string Title { get; set; }
  }
}
