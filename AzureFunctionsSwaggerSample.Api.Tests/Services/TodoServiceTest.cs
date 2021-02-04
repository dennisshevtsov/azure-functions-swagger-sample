// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Tests
{
  using System;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.VisualStudio.TestTools.UnitTesting;

  using AzureFunctionsSwaggerSample.Api.Dtos;
  using AzureFunctionsSwaggerSample.Api.Services;
 
  [TestClass]
  public sealed class TodoServiceTest
  {
    private TodoService _todoService;

    [TestInitialize]
    public void Initialize()
    {
      _todoService = new TodoService();
    }

    [TestMethod]
    public async Task Test()
    {
      var creatingTodoList = TodoServiceTest.NewTodoTask();
      var createdTodoList = await _todoService.CreateTodoListAsync(creatingTodoList, CancellationToken.None);

      Assert.IsNotNull(createdTodoList);
      Assert.IsTrue(createdTodoList.TodoListId != default);

      var receivedTodoListTask0 = await _todoService.GetTodoListAsync(createdTodoList.TodoListId, CancellationToken.None);

      Assert.IsNotNull(receivedTodoListTask0);
      Assert.AreEqual(creatingTodoList.Title, receivedTodoListTask0.Title);
      Assert.AreEqual(creatingTodoList.Description, receivedTodoListTask0.Description);

      var tasks = 5;

      var creatingTodoListTasks = new CreateTodoListTaskRequestDto[tasks];
      var createdTodoListTasks = new CreateTodoListTaskResponseDto[tasks];

      for (var i = 0; i < tasks; ++i)
      {
        creatingTodoListTasks[i] = TodoServiceTest.NewTodoListTask(createdTodoList.TodoListId);

        createdTodoListTasks[i] = await _todoService.CreateTodoListTaskAsync(
          creatingTodoListTasks[i], CancellationToken.None);

        Assert.IsNotNull(createdTodoListTasks[i]);
        Assert.IsTrue(createdTodoListTasks[i].TaskId != default);
      }

      var receivedTodoListTask1 = await _todoService.GetTodoListAsync(
        createdTodoList.TodoListId, CancellationToken.None);

      Assert.IsNotNull(receivedTodoListTask1.Tasks);

      var receivedTodoListTasks = receivedTodoListTask1.Tasks.ToArray();

      Assert.AreEqual(tasks, receivedTodoListTasks.Length);

      for (var i = 0; i < tasks; ++i)
      {
        Assert.IsTrue(receivedTodoListTasks[i].TaskId != default);
        Assert.AreEqual(creatingTodoListTasks[i].Title, receivedTodoListTasks[i].Title);
        Assert.AreEqual(creatingTodoListTasks[i].Description, receivedTodoListTasks[i].Description);
        Assert.AreEqual(creatingTodoListTasks[i].Deadline, receivedTodoListTasks[i].Deadline);
        Assert.AreEqual(false, receivedTodoListTasks[i].Done);
      }

      var completeTask = 0;
      var completing0 = new CompleteTodoListTaskRequestDto
      {
        TodoListId = createdTodoList.TodoListId,
        TaskId = receivedTodoListTasks[completeTask].TaskId,
      };

      await _todoService.CompleteTodoListTaskAsync(completing0, CancellationToken.None);

      var receivedTodoListTask2 = await _todoService.GetTodoListAsync(
        createdTodoList.TodoListId, CancellationToken.None);

      var receivedTodoListTasks1 = receivedTodoListTask2.Tasks.ToArray();

      for (var i = 0; i < tasks; ++i)
      {
        Assert.IsTrue(receivedTodoListTasks1[i].TaskId != default);
        Assert.AreEqual(receivedTodoListTasks1[i].Title, receivedTodoListTasks[i].Title);
        Assert.AreEqual(receivedTodoListTasks1[i].Description, receivedTodoListTasks[i].Description);
        Assert.AreEqual(receivedTodoListTasks1[i].Deadline, receivedTodoListTasks[i].Deadline);
        Assert.AreEqual(i == completeTask, receivedTodoListTasks1[i].Done);
      }
    }

    private static string RandomToken() => Guid.NewGuid().ToString().Replace("-", "");

    private static CreateTodoListRequestDto NewTodoTask() =>
      new CreateTodoListRequestDto
      {
        Title = TodoServiceTest.RandomToken(),
        Description = TodoServiceTest.RandomToken(),
      };

    private static CreateTodoListTaskRequestDto NewTodoListTask(Guid todoListId) =>
      new CreateTodoListTaskRequestDto
      {
        TodoListId = todoListId,
        Title = TodoServiceTest.RandomToken(),
        Description = TodoServiceTest.RandomToken(),
        Deadline = DateTime.UtcNow.AddDays(2),
      };
  }
}
