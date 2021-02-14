// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Tests.Functions
{
  using System;
  using System.IO;
  using System.Linq;
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
  public sealed class CreateTodoListTaskFunctionTest
  {
    private Mock<ISerializationService> _serializationServiceMock;
    private Mock<ITodoService> _todoServiceMock;
    private CreateTodoListTaskFunction _function;

    [TestInitialize]
    public void Initialize()
    {
      _serializationServiceMock = new Mock<ISerializationService>();
      _todoServiceMock = new Mock<ITodoService>();
      _function = new CreateTodoListTaskFunction(_serializationServiceMock.Object, _todoServiceMock.Object);
    }

    [TestMethod]
    public async Task Test()
    {
      var todoListId = Guid.NewGuid();

      var httpRequestMock = new Mock<HttpRequest>();

      var requestDto = new CreateTodoListTaskRequestDto
      {
        Title = CreateTodoListTaskFunctionTest.RandomToken(),
        Description = CreateTodoListTaskFunctionTest.RandomToken(),
        Deadline = DateTime.Today.AddDays(20),
      };

      _serializationServiceMock.Setup(service => service.DeserializeAsync<CreateTodoListTaskRequestDto>(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(requestDto);

      var todoListDocument = new TodoListDocument
      {
        TodoListId = todoListId,
        Title = CreateTodoListTaskFunctionTest.RandomToken(),
        Description = CreateTodoListTaskFunctionTest.RandomToken(),
      };

      var collectorMock = new Mock<IAsyncCollector<TodoListDocument>>();

      await _function.ExecuteAsync(httpRequestMock.Object, collectorMock.Object, todoListDocument, todoListId, CancellationToken.None);

      _serializationServiceMock.Verify(service => service.DeserializeAsync<CreateTodoListTaskRequestDto>(It.IsAny<Stream>(), It.IsAny<CancellationToken>()));
      _todoServiceMock.Verify(service => service.CreateTodoListTaskAsync(It.IsAny<Guid>(), It.IsAny<CreateTodoListTaskRequestDto>(), It.IsAny<TodoListDocument>(), It.IsAny<IAsyncCollector<TodoListDocument>>(), It.IsAny<CancellationToken>()));
    }

    private static string RandomToken() => Guid.NewGuid().ToString().Replace("-", "");
  }
}
