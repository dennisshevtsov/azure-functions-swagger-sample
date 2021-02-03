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
  public sealed class CreateTodoListTaskFunctionTest
  {
    private Mock<ITodoService> _todoServiceMock;
    private Mock<ISerializationService> _serializationServiceMock;
    private CreateTodoListTaskFunction _function;

    [TestInitialize]
    public void Initialize()
    {
      _todoServiceMock = new Mock<ITodoService>();
      _serializationServiceMock = new Mock<ISerializationService>();
      _function = new CreateTodoListTaskFunction(
        _todoServiceMock.Object, _serializationServiceMock.Object);
    }

    [TestMethod]
    public void Test()
    {
    }
  }
}
