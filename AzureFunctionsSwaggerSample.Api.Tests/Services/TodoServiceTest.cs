// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Tests
{
  using Microsoft.VisualStudio.TestTools.UnitTesting;

  using AzureFunctionsSwaggerSample.Api.Services;

  [TestClass]
  public sealed class TodoServiceTest
  {
    private TodoService _todoService;

    [TestInitialize]
    public void Initialize()
    {
      _todoService = new TodoService();
    }

    [TestMethod]
    public void Test()
    {
    }
  }
}
