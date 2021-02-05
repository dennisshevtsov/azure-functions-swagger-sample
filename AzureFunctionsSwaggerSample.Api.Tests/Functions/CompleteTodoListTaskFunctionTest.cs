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
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Moq;

  using AzureFunctionsSwaggerSample.Api.Dtos;
  using AzureFunctionsSwaggerSample.Api.Functions;
  using AzureFunctionsSwaggerSample.Api.Services;

  [TestClass]
  public sealed class CompleteTodoListTaskFunctionTest
  {
    private Mock<ITodoService> _todoServiceMock;
    private Mock<ISerializationService> _serializationServiceMock;
    private CompleteTodoListTaskFunction _function;

    [TestInitialize]
    public void Initialize()
    {
      _todoServiceMock = new Mock<ITodoService>();
      _serializationServiceMock = new Mock<ISerializationService>();
      _function = new CompleteTodoListTaskFunction(
        _todoServiceMock.Object, _serializationServiceMock.Object);
    }

    [TestMethod]
    public async Task Test()
    {
      var todoListId = Guid.NewGuid();
      var todoListTaskId = Guid.NewGuid();
      var httpRequestMock = new Mock<HttpRequest>();

      _serializationServiceMock.Setup(service => service.DeserializeAsync<CompleteTodoListTaskRequestDto>(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(new CompleteTodoListTaskRequestDto());

      _todoServiceMock.Setup(service => service.CompleteTodoListTaskAsync(It.IsAny<CompleteTodoListTaskRequestDto>(), It.IsAny<CancellationToken>()))
                      .Returns((CompleteTodoListTaskRequestDto command, CancellationToken cancellationToken) =>
                      {
                        if (command.TodoListId != todoListId || command.TaskId != todoListTaskId)
                        {
                          Assert.Fail();
                        }

                        return Task.CompletedTask;
                      });

      await _function.RunAsync(httpRequestMock.Object, todoListId, todoListTaskId, CancellationToken.None);

      _serializationServiceMock.Verify(service => service.DeserializeAsync<CompleteTodoListTaskRequestDto>(It.IsAny<Stream>(), It.IsAny<CancellationToken>()));
      _todoServiceMock.Verify(service => service.CompleteTodoListTaskAsync(It.IsAny<CompleteTodoListTaskRequestDto>(), It.IsAny<CancellationToken>()));
    }
  }
}
