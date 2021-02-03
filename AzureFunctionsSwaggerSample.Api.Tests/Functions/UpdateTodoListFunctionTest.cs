// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Tests.Functions
{
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Moq;

  using AzureFunctionsSwaggerSample.Api.Functions;
  using AzureFunctionsSwaggerSample.Api.Services;

  [TestClass]
  public sealed class UpdateTodoListFunctionTest
  {
    private Mock<ITodoService> _todoServiceMock;
    private UpdateTodoListFunction _function;

    [TestInitialize]
    public void Initialize()
    {
      _function = new UpdateTodoListFunction(_todoServiceMock.Object);
    }

    [TestMethod]
    public void Test()
    {
    }
  }
}
