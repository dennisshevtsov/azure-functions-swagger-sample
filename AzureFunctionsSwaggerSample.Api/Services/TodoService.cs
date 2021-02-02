// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Services
{
  using System;
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;

  using AzureFunctionsSwaggerSample.Api.Documents;
  using AzureFunctionsSwaggerSample.Api.Dtos;

  public sealed class TodoService : ITodoService
  {
    private readonly IDictionary<Guid, TodoListDocument> _todoListDictionary;
    private readonly IDictionary<Guid, IList<TodoListTaskDocument>> _todoListTaskDictionary;

    public TodoService()
    {
      _todoListDictionary = new Dictionary<Guid, TodoListDocument>();
      _todoListTaskDictionary = new Dictionary<Guid, IList<TodoListTaskDocument>>();
    }

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
      var todoListId = Guid.NewGuid();
      var totoListTasks = new List<TodoListTaskDocument>();
      var todoListDocument = new TodoListDocument
      {
        TodoListId = todoListId,
        Title = command.Title,
        Description = command.Description,
        Tasks = totoListTasks,
      };

      _todoListDictionary.Add(todoListId, todoListDocument);
      _todoListTaskDictionary.Add(todoListId, totoListTasks);

      var responseDto = new CreateTodoListResponseDto
      {
        TodoListId = todoListId,
      };

      return Task.FromResult(responseDto);
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
