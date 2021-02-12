// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Services
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.Azure.WebJobs;

  using AzureFunctionsSwaggerSample.Api.Documents;
  using AzureFunctionsSwaggerSample.Api.Dtos;

  public interface ITodoService
  {
    public Task<TodoListDocument> CreateTodoListAsync(
      CreateTodoListRequestDto command,
      IAsyncCollector<TodoListDocument> collector,
      CancellationToken cancellationToken);

    public Task UpdateTodoListAsync(
      UpdateTodoListRequestDto command,
      TodoListDocument document,
      IAsyncCollector<TodoListDocument> collector,
      CancellationToken cancellationToken);

    public Task<TodoListTaskDocument> CreateTodoListTaskAsync(
      CreateTodoListTaskRequestDto command,
      TodoListDocument todoListDocument,
      IAsyncCollector<TodoListDocument> collector,
      CancellationToken cancellationToken);

    public Task CompleteTodoListTaskAsync(
      Guid todoListTaskId,
      TodoListDocument todoListDocument,
      IAsyncCollector<TodoListDocument> collector,
      CancellationToken cancellationToken);
  }
}
