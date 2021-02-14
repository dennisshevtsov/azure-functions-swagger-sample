// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Tests.Functions
{
  using System.Reflection;
  using System.Text.Json;

  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Azure.WebJobs;
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using Moq;

  using AzureFunctionsSwaggerSample.Api.Functions;

  [TestClass]
  public sealed class GetSwaggerFunctionTest
  {
    private GetSwaggerFunction _function;

    [TestInitialize]
    public void Initialize() => _function = new GetSwaggerFunction();

    [TestMethod]
    public void Test()
    {
      var httpRequestMock = new Mock<HttpRequest>();
      var executionContext = new ExecutionContext
      {
        FunctionAppDirectory = $"..\\..\\..\\..\\{Assembly.GetExecutingAssembly().GetName().Name}\\bin\\Debug\\netcoreapp3.1",
      };

      var actionResult = _function.Execute(httpRequestMock.Object, executionContext);

      Assert.IsNotNull(actionResult);

      var contentResult = actionResult as ContentResult;

      Assert.IsNotNull(contentResult);
      Assert.AreEqual("application/json", contentResult.ContentType);
      Assert.AreEqual(200, contentResult.StatusCode);
      Assert.IsFalse(string.IsNullOrWhiteSpace(contentResult.Content));

      var json = JsonDocument.Parse(contentResult.Content);

      if (!json.RootElement.TryGetProperty("paths", out var paths))
      {
        Assert.Fail();
      }

      if (!paths.TryGetProperty("/api/todo/{todoListId}/task/{taskId}/complete", out var completePath) ||
          !completePath.TryGetProperty("post", out _))
      {
        Assert.Fail();
      }

      if (!paths.TryGetProperty("/api/todo/{todoListId}", out var todoByIdPath) ||
          !todoByIdPath.TryGetProperty("get", out _) ||
          !todoByIdPath.TryGetProperty("put", out _))
      {
        Assert.Fail();
      }

      if (!paths.TryGetProperty("/api/todo/{todoListId}/task", out var todoByIdTaskPath) ||
          !todoByIdTaskPath.TryGetProperty("post", out _))
      {
        Assert.Fail();
      }

      if (!paths.TryGetProperty("/api/todo", out var todoPath) ||
          !todoPath.TryGetProperty("get", out _) ||
          !todoPath.TryGetProperty("post", out _))
      {
        Assert.Fail();
      }

      if (!paths.TryGetProperty("/api/swagger/swagger.json", out var swaggerPath) ||
          !swaggerPath.TryGetProperty("get", out _))
      {
        Assert.Fail();
      }
    }
  }
}
