// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Services
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using AzureFunctionsSwaggerSample.Api.Dtos;

  /// <summary>Provides a simple API to operate within the TODO list domain.</summary>
  public interface ITodoService
  {
    /// <summary>Gets a TODO list by its ID.</summary>
    /// <param name="todoListId"></param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<GetTodoListResponseDto> GetTodoListAsync(Guid todoListId, CancellationToken cancellationToken);

    /// <summary>Gets a collection of TODO lists.</summary>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<GetTodoListsResponseDto> GetTodoListsAsync(CancellationToken cancellationToken);

    /// <summary>Creates a TODO list.</summary>
    /// <param name="command">An object that represents data to create a TODO list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<CreateTodoListResponseDto> CreateTodoListAsync(CreateTodoListRequestDto command, CancellationToken cancellationToken);

    /// <summary>Updates a TODO list.</summary>
    /// <param name="command">An object that represents data to update a TODO list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task UpdateTodoListAsync(UpdateTodoListRequestDto command, CancellationToken cancellationToken);

    /// <summary>Creates a TODO list task.</summary>
    /// <param name="command">An object that represents data to create a TODO list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<CreateTodoListTaskResponseDto> CreateTodoListTaskAsync(CreateTodoListTaskRequestDto command, CancellationToken cancellationToken);

    /// <summary>Completes a TODO list task.</summary>
    /// <param name="command">An object that represents data to complete a TODO list task.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task CompleteTodoListTaskAsync(CompleteTodoListTaskRequestDto command, CancellationToken cancellationToken);
  }
}
