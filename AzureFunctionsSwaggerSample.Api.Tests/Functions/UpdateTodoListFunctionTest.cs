// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Tests.Functions
{
  using System;
  using System.IO;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Moq;

  using AzureFunctionsSwaggerSample.Api.Documents;
  using AzureFunctionsSwaggerSample.Api.Dtos;
  using AzureFunctionsSwaggerSample.Api.Functions;
  using AzureFunctionsSwaggerSample.Api.Services;

  [TestClass]
  public sealed class UpdateTodoListFunctionTest
  {
    private Mock<ISerializationService> _serializationServiceMock;
    private Mock<ITodoService> _todoServiceMock;
    private UpdateTodoListFunction _function;

    [TestInitialize]
    public void Initialize()
    {
      _serializationServiceMock = new Mock<ISerializationService>();
      _todoServiceMock = new Mock<ITodoService>();
      _function = new UpdateTodoListFunction(_serializationServiceMock.Object, _todoServiceMock.Object);
    }

    [TestMethod]
    public async Task Test()
    {
      var todoListId = Guid.NewGuid();
      var httpRequestMock = new Mock<HttpRequest>();

      var requestDto = new UpdateTodoListRequestDto
      {
        Title = UpdateTodoListFunctionTest.RandomToken(),
        Description = UpdateTodoListFunctionTest.RandomToken(),
      };

      _serializationServiceMock.Setup(service => service.DeserializeAsync<UpdateTodoListRequestDto>(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(requestDto);

      var collectorMock = new Mock<IAsyncCollector<TodoListDocument>>();

      collectorMock.Setup(collector => collector.AddAsync(It.IsAny<TodoListDocument>(), It.IsAny<CancellationToken>()))
                   .Returns((TodoListDocument document, CancellationToken cancellationToken) =>
                   {
                     if (document.TodoListId != todoListId ||
                         document.Title != requestDto.Title ||
                         document.Description != requestDto.Description)
                     {
                       Assert.Fail();
                     }

                     return Task.CompletedTask;
                   });

      var document = new TodoListDocument
      {
        TodoListId = todoListId,
        Title = UpdateTodoListFunctionTest.RandomToken(),
        Description = UpdateTodoListFunctionTest.RandomToken(),
      };

      await _function.ExecuteAsync(
        httpRequestMock.Object,
        collectorMock.Object,
        document,
        todoListId,
        CancellationToken.None);

      _serializationServiceMock.Verify(service => service.DeserializeAsync<UpdateTodoListRequestDto>(It.IsAny<Stream>(), It.IsAny<CancellationToken>()));
      collectorMock.Verify(collector => collector.AddAsync(It.IsAny<TodoListDocument>(), It.IsAny<CancellationToken>()));
    }

    private static string RandomToken() => Guid.NewGuid().ToString().Replace("-", "");
  }
}
