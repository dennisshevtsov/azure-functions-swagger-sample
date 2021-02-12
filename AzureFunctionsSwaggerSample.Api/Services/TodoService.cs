// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Services
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.Azure.WebJobs;

  using AzureFunctionsSwaggerSample.Api.Documents;
  using AzureFunctionsSwaggerSample.Api.Dtos;
  
  public sealed class TodoService : ITodoService
  {
    public Task CreateTodoListAsync(
      Guid todoListId,
      CreateTodoListRequestDto command,
      IAsyncCollector<TodoListDocument> collector,
      CancellationToken cancellationToken)
    {
      var document = new TodoListDocument
      {
        TodoListId = todoListId,
        PartitionId = nameof(TodoListDocument),
        Title = command.Title,
        Description = command.Description,
      };

      return collector.AddAsync(document, cancellationToken);
    }

    public Task UpdateTodoListAsync(
      UpdateTodoListRequestDto command,
      TodoListDocument document,
      IAsyncCollector<TodoListDocument> collector,
      CancellationToken cancellationToken)
    {
      document.Title = command.Title;
      document.Description = command.Description;

      return collector.AddAsync(document, cancellationToken);
    }

    public Task CreateTodoListTaskAsync(
      Guid todoListTaskId,
      CreateTodoListTaskRequestDto command,
      TodoListDocument todoListDocument,
      IAsyncCollector<TodoListDocument> collector,
      CancellationToken cancellationToken)
    {
      var todoListTaskDocument = new TodoListTaskDocument
      {
        TaskId = todoListTaskId,
        Title = command.Title,
        Description = command.Description,
        Deadline = command.Deadline,
      };

      var tasks = new List<TodoListTaskDocument>(todoListDocument.Tasks ?? Enumerable.Empty<TodoListTaskDocument>());

      tasks.Add(todoListTaskDocument);
      todoListDocument.Tasks = tasks;

      return collector.AddAsync(todoListDocument, cancellationToken);
    }

    public Task CompleteTodoListTaskAsync(
      Guid todoListTaskId,
      TodoListDocument todoListDocument,
      IAsyncCollector<TodoListDocument> collector,
      CancellationToken cancellationToken)
    {
      var todoListTaskDocument = todoListDocument.Tasks.FirstOrDefault(
        document => document.TaskId == todoListTaskId);

      if (todoListTaskDocument != null)
      {
        todoListTaskDocument.Completed = true;
      }

      return collector.AddAsync(todoListDocument, cancellationToken);
    }
  }
}
