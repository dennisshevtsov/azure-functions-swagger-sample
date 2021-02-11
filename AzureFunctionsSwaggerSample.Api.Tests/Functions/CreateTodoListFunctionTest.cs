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
  public sealed class CreateTodoListFunctionTest
  {
    private Mock<ISerializationService> _serializationServiceMock;
    private CreateTodoListFunction _function;

    [TestInitialize]
    public void Initialize()
    {
      _serializationServiceMock = new Mock<ISerializationService>();
      _function = new CreateTodoListFunction(_serializationServiceMock.Object);
    }

    [TestMethod]
    public async Task Test()
    {
      var httpRequestMock = new Mock<HttpRequest>();

      var requestDto = new CreateTodoListRequestDto
      {
        Title = CreateTodoListFunctionTest.RandomToken(),
        Description = CreateTodoListFunctionTest.RandomToken(),
      };

      _serializationServiceMock.Setup(service => service.DeserializeAsync<CreateTodoListRequestDto>(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(requestDto);

      var collectorMock = new Mock<IAsyncCollector<TodoListDocument>>();

      collectorMock.Setup(collector => collector.AddAsync(It.IsAny<TodoListDocument>(), It.IsAny<CancellationToken>()))
                   .Returns((TodoListDocument document, CancellationToken cancellationToken) =>
                   {
                     if (document.TodoListId == default ||
                         document.Title != requestDto.Title ||
                         document.Description != requestDto.Description)
                     {
                       Assert.Fail();
                     }

                     return Task.CompletedTask;
                   });

      await _function.ExecuteAsync(httpRequestMock.Object, collectorMock.Object, CancellationToken.None);

      _serializationServiceMock.Verify(service => service.DeserializeAsync<CreateTodoListRequestDto>(It.IsAny<Stream>(), It.IsAny<CancellationToken>()));
      collectorMock.Verify(collector => collector.AddAsync(It.IsAny<TodoListDocument>(), It.IsAny<CancellationToken>()));
    }

    private static string RandomToken() => Guid.NewGuid().ToString().Replace("-", "");
  }
}
