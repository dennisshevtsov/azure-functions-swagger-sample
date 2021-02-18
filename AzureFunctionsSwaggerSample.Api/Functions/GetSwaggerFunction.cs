// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Functions
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Net.Mime;
  using System.Reflection;
  using System.Xml.Linq;

  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Azure.WebJobs;
  using Microsoft.OpenApi;
  using Microsoft.OpenApi.CSharpAnnotations.DocumentGeneration;
  using Microsoft.OpenApi.Extensions;

  /// <summary>Provides a method to handle an HTTP request.</summary>
  public sealed class GetSwaggerFunction
  {
    /// <summary>GetSwaggerFunction</summary>
    /// <group>Swagger</group>
    /// <remarks>Gets a Swagger document.</remarks>
    /// <param name="request">An object that represents the incoming side of an individual HTTP request.</param>
    /// <param name="executionContext"></param>
    /// <returns>An object that defines a contract that represents the result of an action method.</returns>
    /// <verb>get</verb>
    /// <url>http://localhost:7071/api/swagger/swagger.json</url>
    /// <response code="200"></response>
    [FunctionName(nameof(GetSwaggerFunction))]
    public IActionResult Execute(
      [HttpTrigger("get", Route = "swagger/swagger.json")] HttpRequest request,
      ExecutionContext executionContext)
    {
      var assembly = Assembly.GetExecutingAssembly();
      var xmlDocumentationPath =
        $"{executionContext.FunctionAppDirectory}\\{assembly.GetName().Name}.xml";

      var generator = new OpenApiGenerator();
      var xmlDocuments = new List<XDocument>
      {
        XDocument.Load(xmlDocumentationPath),
      };
      var assemblyPaths = new List<string> { assembly.Location, };

      var input = new OpenApiGeneratorConfig(
        xmlDocuments, assemblyPaths, "V1", FilterSetVersion.V1);
      var generationSettings = new OpenApiDocumentGenerationSettings(
        new SchemaGenerationSettings(new CamelCasePropertyNameResolver()));

      var openApiDocuments = generator.GenerateDocuments(input, out _, generationSettings);
      var json = openApiDocuments.First().Value.Serialize(
        OpenApiSpecVersion.OpenApi3_0, OpenApiFormat.Json);

      var result = new ContentResult
      {
        Content = json,
        ContentType = MediaTypeNames.Application.Json,
        StatusCode = StatusCodes.Status200OK,
      };

      return result;
    }
  }
}
