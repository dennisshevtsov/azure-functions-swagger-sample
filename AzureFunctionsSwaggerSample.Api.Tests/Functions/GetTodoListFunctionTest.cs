// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Tests.Functions
{
  using System;

  using Microsoft.AspNetCore.Http;
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Moq;

  using AzureFunctionsSwaggerSample.Api.Functions;

  [TestClass]
  public sealed class GetTodoListFunctionTest
  {
    private GetTodoListFunction _function;

    [TestInitialize]
    public void Initialize()
    {
      _function = new GetTodoListFunction();
    }

    [TestMethod]
    public void Test()
    {
      var todoListId = Guid.NewGuid();
      var httpRequestMock = new Mock<HttpRequest>();

      _function.Execute(httpRequestMock.Object, null, todoListId);
    }
  }
}
