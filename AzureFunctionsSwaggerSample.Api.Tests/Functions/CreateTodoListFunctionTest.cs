// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
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
    private Mock<ITodoService> _todoServiceMock;
    private CreateTodoListFunction _function;

    [TestInitialize]
    public void Initialize()
    {
      _serializationServiceMock = new Mock<ISerializationService>();
      _todoServiceMock = new Mock<ITodoService>();
      _function = new CreateTodoListFunction(_serializationServiceMock.Object, _todoServiceMock.Object);
    }

    [TestMethod]
    public async Task Test()
    {
      var httpRequestMock = new Mock<HttpRequest>();
      var collectorMock = new Mock<IAsyncCollector<TodoListDocument>>();

      var requestDto = new CreateTodoListRequestDto
      {
        Title = CreateTodoListFunctionTest.RandomToken(),
        Description = CreateTodoListFunctionTest.RandomToken(),
      };

      _serializationServiceMock.Setup(service => service.DeserializeAsync<CreateTodoListRequestDto>(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(requestDto);

      await _function.ExecuteAsync(httpRequestMock.Object, collectorMock.Object, CancellationToken.None);

      _serializationServiceMock.Verify(service => service.DeserializeAsync<CreateTodoListRequestDto>(It.IsAny<Stream>(), It.IsAny<CancellationToken>()));
      _todoServiceMock.Verify(service => service.CreateTodoListAsync(It.IsAny<Guid>(), It.IsAny<CreateTodoListRequestDto>(), It.IsAny<IAsyncCollector<TodoListDocument>>(), It.IsAny<CancellationToken>()));
    }

    private static string RandomToken() => Guid.NewGuid().ToString().Replace("-", "");
  }
}
