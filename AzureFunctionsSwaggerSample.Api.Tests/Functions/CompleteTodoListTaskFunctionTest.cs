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
    private Mock<ISerializationService> _serializationServiceMock;
    private CompleteTodoListTaskFunction _function;

    [TestInitialize]
    public void Initialize()
    {
      _serializationServiceMock = new Mock<ISerializationService>();
      _function = new CompleteTodoListTaskFunction(_serializationServiceMock.Object);
    }

    [TestMethod]
    public async Task Test()
    {
      var todoListId = Guid.NewGuid();
      var todoListTaskId = Guid.NewGuid();
      var httpRequestMock = new Mock<HttpRequest>();

      _serializationServiceMock.Setup(service => service.DeserializeAsync<CompleteTodoListTaskRequestDto>(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(new CompleteTodoListTaskRequestDto());

      await _function.ExecuteAsync(httpRequestMock.Object, null, null, todoListId, todoListTaskId, CancellationToken.None);

      _serializationServiceMock.Verify(service => service.DeserializeAsync<CompleteTodoListTaskRequestDto>(It.IsAny<Stream>(), It.IsAny<CancellationToken>()));
    }
  }
}
