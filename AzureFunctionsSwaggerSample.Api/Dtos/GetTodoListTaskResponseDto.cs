// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Dtos
{
  using System;
  using System.Text.Json.Serialization;

  /// <summary>Represents detail of a TODO list task.</summary>
  public sealed class GetTodoListTaskResponseDto
  {
    /// <summary>Gets/sets a value that represents an ID of a TODO list task.</summary>
    [JsonPropertyName("taskId")]
    public Guid TaskId { get; set; }

    /// <summary>Gets/sets a value that represents a title of a TODO list task.</summary>
    [JsonPropertyName("title")]
    public string Title { get; set; }

    /// <summary>Gets/sets a value that represents a description of a TODO list task.</summary>
    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>Gets/sets a value that represents a deadline of a TODO list task.</summary>
    [JsonPropertyName("deadline")]
    public DateTime Deadline { get; set; }

    /// <summary>Gets/sets a value that indicates if a TODO list is completed.</summary>
    [JsonPropertyName("completed")]
    public bool Completed { get; set; }
  }
}
