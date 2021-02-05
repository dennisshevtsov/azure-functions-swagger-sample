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
    public Task<GetTodoListResponseDto> GetTodoListAsync(Guid todoListId, CancellationToken cancellationToken);

    public Task<GetTodoListsResponseDto> GetTodoListsAsync(CancellationToken cancellationToken);

    public Task<CreateTodoListResponseDto> CreateTodoListAsync(CreateTodoListRequestDto command, CancellationToken cancellationToken);

    public Task UpdateProductAsync(UpdateTodoListRequestDto command, CancellationToken cancellationToken);

    public Task<CreateTodoListTaskResponseDto> CreateTodoListTaskAsync(CreateTodoListTaskRequestDto command, CancellationToken cancellationToken);

    public Task CompleteTodoListTaskAsync(CompleteTodoListTaskRequestDto command, CancellationToken cancellationToken);
  }
}
