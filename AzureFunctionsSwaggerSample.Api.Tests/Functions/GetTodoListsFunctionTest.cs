// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Tests.Functions
{
  using Microsoft.AspNetCore.Http;
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Moq;

  using AzureFunctionsSwaggerSample.Api.Functions;

  [TestClass]
  public sealed class GetTodoListsFunctionTest
  {
    private GetTodoListsFunction _function;

    [TestInitialize]
    public void Initialize()
    {
      _function = new GetTodoListsFunction();
    }

    [TestMethod]
    public void Test()
    {
      var httpRequestMock = new Mock<HttpRequest>();

      _function.Execute(httpRequestMock.Object, null);
    }
  }
}
