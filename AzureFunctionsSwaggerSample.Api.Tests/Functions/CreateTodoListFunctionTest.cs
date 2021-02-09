// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Tests.Functions
{
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

      await _function.ExecuteAsync(httpRequestMock.Object, null, CancellationToken.None);

      _serializationServiceMock.Verify(service => service.DeserializeAsync<CreateTodoListRequestDto>(It.IsAny<Stream>(), It.IsAny<CancellationToken>()));
    }
  }
}
