// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

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

      var document = new TodoListDocument
      {
        TodoListId = todoListId,
        Title = UpdateTodoListFunctionTest.RandomToken(),
        Description = UpdateTodoListFunctionTest.RandomToken(),
      };

      var collectorMock = new Mock<IAsyncCollector<TodoListDocument>>();

      await _function.ExecuteAsync(
        httpRequestMock.Object,
        collectorMock.Object,
        document,
        todoListId,
        CancellationToken.None);

      _serializationServiceMock.Verify(service => service.DeserializeAsync<UpdateTodoListRequestDto>(It.IsAny<Stream>(), It.IsAny<CancellationToken>()));
      _todoServiceMock.Verify(service => service.UpdateTodoListAsync(It.IsAny<UpdateTodoListRequestDto>(), It.IsAny<TodoListDocument>(), It.IsAny<IAsyncCollector<TodoListDocument>>(), It.IsAny<CancellationToken>()));
    }

    private static string RandomToken() => Guid.NewGuid().ToString().Replace("-", "");
  }
}
