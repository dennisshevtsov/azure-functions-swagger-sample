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
      GetTodoListResponseDto reponseDto = null;

      if (_todoListDictionary.TryGetValue(todoListId, out var todoListDocument))
      {
        reponseDto = new GetTodoListResponseDto
        {
          TodoListId = todoListDocument.TodoListId,
          Title = todoListDocument.Title,
          Description = todoListDocument.Description,
          Tasks = todoListDocument.Tasks.Select(document => new GetTodoListTaskResponseDto
          {
            TaskId = document.TaskId,
            Title = document.Title,
            Description = document.Description,
            Deadline = document.Deadline,
            Done = document.Done,
          }),
        };
      }

      return Task.FromResult(reponseDto);
    }

    public Task<GetTodoListsResponseDto> GetTodoListsAsync(CancellationToken cancellationToken)
    {
      var responseDto = new GetTodoListsResponseDto
      {
        Items = _todoListDictionary.Select(document => new GetTodoListsItemResponseDto
        {
          TodoListId = document.Key,
          Title = document.Value.Title,
        }),
      };

      return Task.FromResult(responseDto);
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
      if (_todoListDictionary.TryGetValue(command.TodoListId, out var todoListDocument))
      {
        todoListDocument.Title = command.Title;
        todoListDocument.Description = command.Description;
      }

      throw new Exception();
    }

    public Task<CreateTodoListTaskResponseDto> CreateTodoListTaskAsync(CreateTodoListTaskRequestDto command, CancellationToken cancellationToken)
    {
      if (!_todoListTaskDictionary.TryGetValue(command.TodoListId, out var todoListTaskDocuments))
      {
        throw new Exception();
      }

      var todoListTaskDocument = new TodoListTaskDocument
      {
        TaskId = Guid.NewGuid(),
        Title = command.Title,
        Description = command.Description,
        Deadline = command.Deadline,
      };

      todoListTaskDocuments.Add(todoListTaskDocument);

      var responseDto = new CreateTodoListTaskResponseDto
      {
        TaskId = todoListTaskDocument.TaskId,
      };

      return Task.FromResult(responseDto);
    }

    public Task CompleteTodoListTaskAsync(CompleteTodoListTaskRequestDto command, CancellationToken cancellationToken)
    {
      if (_todoListTaskDictionary.TryGetValue(command.TodoListId, out var todoListTaskDocuments))
      {
        var todoListTaskDocument = todoListTaskDocuments.FirstOrDefault(document => document.TaskId == command.TaskId);

        if (todoListTaskDocument != null)
        {
          todoListTaskDocument.Done = true;

          return Task.CompletedTask;
        }
      }

      throw new Exception();
    }
  }
}
