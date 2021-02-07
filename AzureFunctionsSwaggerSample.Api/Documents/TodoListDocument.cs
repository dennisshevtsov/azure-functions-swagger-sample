﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Documents
{
  using System;
  using System.Collections.Generic;
  using System.Text.Json.Serialization;

  /// <summary>Represents detail of a TODO list.</summary>
  public sealed class TodoListDocument
  {
    /// <summary>Gets/sets a value that represents an ID of a TODO list.</summary>
    [JsonPropertyName("id")]
    public Guid TodoListId { get; set; }

    /// <summary>Gets/sets a value that represents an ID of a partition.</summary>
    [JsonPropertyName("_type")]
    public string PartitionId { get; set; }

    /// <summary>Gets/sets a value that represents a title of a TODO list.</summary>
    [JsonPropertyName("title")] 
    public string Title { get; set; }

    /// <summary>Gets/sets a value that represents a description of a TODO list.</summary>
    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>Gets/sets an object that represents a collection of tasks of a TODO list.</summary>
    [JsonPropertyName("tasks")]
    public IEnumerable<TodoListTaskDocument> Tasks { get; set; }
  }
}
