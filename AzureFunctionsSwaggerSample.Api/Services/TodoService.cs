// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Services
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using AzureFunctionsSwaggerSample.Api.Dtos;

  public sealed class TodoService : ITodoService
  {
    public Task<GetTodoListResponseDto> GetTodoListAsync(Guid todoListId, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<GetTodoListsResponseDto> GetTodoListsAsync(CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<CreateTodoListResponseDto> CreateTodoListAsync(CreateTodoListRequestDto command, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task UpdateProductAsync(UpdateTodoListRequestDto command, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<CreateTodoListTaskResponseDto> CreateTodoListTaskAsync(CreateTodoListTaskRequestDto command, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task CompleteTodoListTaskAsync(CompleteTodoListTaskRequestDto command, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }
  }
}
