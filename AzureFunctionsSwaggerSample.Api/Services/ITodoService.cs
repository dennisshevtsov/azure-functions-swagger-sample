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

  /// <summary>Provides a simple API to execute operation within objects of the <see cref="AzureFunctionsSwaggerSample.Api.Documents.TodoListDocument"/> class.</summary>
  public interface ITodoService
  {
    /// <summary>Creates a TODO list.</summary>
    /// <param name="todoListId">A value that represents an ID of a TODO list.</param>
    /// <param name="command">An object that represents data to create a TODO list.</param>
    /// <param name="collector">An object that provides a simple API to store objects of the <see cref="AzureFunctionsSwaggerSample.Api.Documents.TodoListDocument"/> class.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task CreateTodoListAsync(
      Guid todoListId,
      CreateTodoListRequestDto command,
      IAsyncCollector<TodoListDocument> collector,
      CancellationToken cancellationToken);

    /// <summary>Updates a TODO list.</summary>
    /// <param name="command"></param>
    /// <param name="document">An object that r</param>
    /// <param name="collector">An object that provides a simple API to store objects of the <see cref="AzureFunctionsSwaggerSample.Api.Documents.TodoListDocument"/> class.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task UpdateTodoListAsync(
      UpdateTodoListRequestDto command,
      TodoListDocument document,
      IAsyncCollector<TodoListDocument> collector,
      CancellationToken cancellationToken);

    /// <summary>Creates a TODO list task.</summary>
    /// <param name="todoListTaskId">A value that represents an ID of a TODO list task.</param>
    /// <param name="command">An object that r</param>
    /// <param name="todoListDocument"></param>
    /// <param name="collector">An object that provides a simple API to store objects of the <see cref="AzureFunctionsSwaggerSample.Api.Documents.TodoListDocument"/> class.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task CreateTodoListTaskAsync(
      Guid todoListTaskId,
      CreateTodoListTaskRequestDto command,
      TodoListDocument todoListDocument,
      IAsyncCollector<TodoListDocument> collector,
      CancellationToken cancellationToken);

    /// <summary>Marks a TODO list task as completed.</summary>
    /// <param name="todoListTaskId">A value that represents an ID of a TODO list task.</param>
    /// <param name="todoListDocument">An object that r</param>
    /// <param name="collector">An object that provides a simple API to store objects of the <see cref="AzureFunctionsSwaggerSample.Api.Documents.TodoListDocument"/> class.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task CompleteTodoListTaskAsync(
      Guid todoListTaskId,
      TodoListDocument todoListDocument,
      IAsyncCollector<TodoListDocument> collector,
      CancellationToken cancellationToken);
  }
}
