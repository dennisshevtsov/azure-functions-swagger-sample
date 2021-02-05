// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Tests.Functions
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Moq;

  using AzureFunctionsSwaggerSample.Api.Functions;
  using AzureFunctionsSwaggerSample.Api.Services;

  [TestClass]
  public sealed class GetTodoListFunctionTest
  {
    private Mock<ITodoService> _todoServiceMock;
    private GetTodoListFunction _function;

    [TestInitialize]
    public void Initialize()
    {
      _todoServiceMock = new Mock<ITodoService>();
      _function = new GetTodoListFunction(_todoServiceMock.Object);
    }

    [TestMethod]
    public async Task Test()
    {
      var todoListId = Guid.NewGuid();
      var httpRequestMock = new Mock<HttpRequest>();

      await _function.ExecuteAsync(httpRequestMock.Object, todoListId, CancellationToken.None);

      _todoServiceMock.Verify(service => service.GetTodoListAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()));
    }
  }
}
