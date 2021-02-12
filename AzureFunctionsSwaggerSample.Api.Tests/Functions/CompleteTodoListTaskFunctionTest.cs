// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Tests.Functions
{
  using System;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Moq;

  using AzureFunctionsSwaggerSample.Api.Documents;
  using AzureFunctionsSwaggerSample.Api.Functions;
  using AzureFunctionsSwaggerSample.Api.Services;

  [TestClass]
  public sealed class CompleteTodoListTaskFunctionTest
  {
    private Mock<ITodoService> _todoServiceMock;
    private CompleteTodoListTaskFunction _function;

    [TestInitialize]
    public void Initialize()
    {
      _todoServiceMock = new Mock<ITodoService>();
      _function = new CompleteTodoListTaskFunction(_todoServiceMock.Object);
    }

    [TestMethod]
    public async Task Test()
    {
      var todoListId = Guid.NewGuid();
      var todoListTaskId = Guid.NewGuid();
      var httpRequestMock = new Mock<HttpRequest>();

      var collectorMock = new Mock<IAsyncCollector<TodoListDocument>>();

      collectorMock.Setup(collector => collector.AddAsync(It.IsAny<TodoListDocument>(), It.IsAny<CancellationToken>()))
                   .Returns((TodoListDocument todoListDocument, CancellationToken cancellationToken) =>
                   {
                     if (todoListDocument == null ||
                         todoListDocument.Tasks == null)
                     {
                       Assert.Fail();
                     }

                     var todoListTaskDocument = todoListDocument.Tasks.FirstOrDefault(
                       document => document.TaskId == todoListTaskId);

                     if (todoListTaskDocument == null ||
                         todoListTaskDocument.TaskId != todoListTaskId ||
                         !todoListTaskDocument.Completed)
                     {
                       Assert.Fail();
                     }

                     return Task.CompletedTask;
                   });

      var document = new TodoListDocument
      {
        TodoListId = todoListId,
        Title = CompleteTodoListTaskFunctionTest.RandomToken(),
        Description = CompleteTodoListTaskFunctionTest.RandomToken(),
        Tasks = new[]
        {
          new TodoListTaskDocument
          {
            TaskId = Guid.NewGuid(),
          },
          new TodoListTaskDocument
          {
            TaskId = todoListTaskId,
          },
          new TodoListTaskDocument
          {
            TaskId = Guid.NewGuid(),
          },
        },
      };

      await _function.ExecuteAsync(
        httpRequestMock.Object,
        collectorMock.Object,
        document,
        todoListId,
        todoListTaskId,
        CancellationToken.None);

      _todoServiceMock.Verify(service => service.CompleteTodoListTaskAsync(
        It.IsAny<Guid>(), It.IsAny<TodoListDocument>(), It.IsAny<IAsyncCollector<TodoListDocument>>(), It.IsAny<CancellationToken>()));
      collectorMock.Verify(collector => collector.AddAsync(It.IsAny<TodoListDocument>(), It.IsAny<CancellationToken>()));
    }

    private static string RandomToken() => Guid.NewGuid().ToString().Replace("-", "");
  }
}
