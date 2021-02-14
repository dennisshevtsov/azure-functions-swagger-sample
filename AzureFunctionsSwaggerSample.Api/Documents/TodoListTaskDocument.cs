// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Documents
{
  using System;

  using Newtonsoft.Json;

  /// <summary>Represents detail of a TODO list task.</summary>
  public sealed class TodoListTaskDocument
  {
    /// <summary>Gets/sets a value that represents an ID of a TODO list task.</summary>
    [JsonProperty("id")]
    public Guid TaskId { get; set; }

    /// <summary>Gets/sets a value that represents a title of a TODO list task.</summary>
    [JsonProperty("title")]
    public string Title { get; set; }

    /// <summary>Gets/sets a value that represents a description of a TODO list task.</summary>
    [JsonProperty("description")]
    public string Description { get; set; }

    /// <summary>Gets/sets a value that represents a deadline of a TODO list task.</summary>
    [JsonProperty("deadline")]
    public DateTime Deadline { get; set; }

    /// <summary>Gets/sets a value that indicates if a TODO list task is completed.</summary>
    [JsonProperty("completed")]
    public bool Completed { get; set; }
  }
}
