// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Tests.Services
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.Azure.WebJobs;
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Moq;

  using AzureFunctionsSwaggerSample.Api.Documents;
  using AzureFunctionsSwaggerSample.Api.Dtos;
  using AzureFunctionsSwaggerSample.Api.Services;

  [TestClass]
  public sealed class TodoServiceTest
  {
    private ITodoService _todoService;

    [TestInitialize]
    public void Initialize()
    {
      _todoService = new TodoService();
    }

    [TestMethod]
    public async Task Test_CreateTodoListAsync()
    {
      var todoListId = Guid.NewGuid();
      var command = new CreateTodoListRequestDto();
      var collectorMock = TodoServiceTest.GetCollectorMock(todoListId, command);

      await _todoService.CreateTodoListAsync(
        todoListId, command, collectorMock.Object, CancellationToken.None);

      collectorMock.Verify(collector => collector.AddAsync(It.IsAny<TodoListDocument>(), It.IsAny<CancellationToken>()));
    }

    [TestMethod]
    public async Task Test_UpdateTodoListAsync()
    {
      var command = new UpdateTodoListRequestDto();
      var document = new TodoListDocument();
      var collectorMock = TodoServiceTest.GetCollectorMock(command);

      await _todoService.UpdateTodoListAsync(
        command, document, collectorMock.Object, CancellationToken.None);

      collectorMock.Verify(collector => collector.AddAsync(It.IsAny<TodoListDocument>(), It.IsAny<CancellationToken>()));
    }

    private static Mock<IAsyncCollector<TodoListDocument>> GetCollectorMock(Guid todoListId, CreateTodoListRequestDto command)
    {
      var collectorMock = new Mock<IAsyncCollector<TodoListDocument>>();

      collectorMock.Setup(collector => collector.AddAsync(It.IsAny<TodoListDocument>(), It.IsAny<CancellationToken>()))
                   .Returns((TodoListDocument document, CancellationToken cancellationToken) =>
                   {
                     if (document.TodoListId != todoListId)
                     {
                       Assert.Fail("document.TodoListId != todoListId");
                     }

                     if (document.Title != command.Title)
                     {
                       Assert.Fail("document.Title != command.Title");
                     }

                     if (document.Description != command.Description)
                     {
                       Assert.Fail("document.Description != command.Description");
                     }

                     return Task.CompletedTask;
                   });

      return collectorMock;
    }

    private static Mock<IAsyncCollector<TodoListDocument>> GetCollectorMock(UpdateTodoListRequestDto command)
    {
      var collectorMock = new Mock<IAsyncCollector<TodoListDocument>>();

      collectorMock.Setup(collector => collector.AddAsync(It.IsAny<TodoListDocument>(), It.IsAny<CancellationToken>()))
                   .Returns((TodoListDocument document, CancellationToken cancellationToken) =>
                   {
                     if (document.Title != command.Title)
                     {
                       Assert.Fail("document.Title != command.Title");
                     }

                     if (document.Description != command.Description)
                     {
                       Assert.Fail("document.Description != command.Description");
                     }

                     return Task.CompletedTask;
                   });

      return collectorMock;
    }
  }
}
