// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

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

  public sealed class GetSwaggerFunction
  {
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
