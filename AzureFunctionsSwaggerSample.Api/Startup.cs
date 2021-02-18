// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

[assembly: Microsoft.Azure.WebJobs.Hosting.WebJobsStartup(typeof(AzureFunctionsSwaggerSample.Api.Startup))]

namespace AzureFunctionsSwaggerSample.Api
{
  using Microsoft.Azure.WebJobs;
  using Microsoft.Azure.WebJobs.Hosting;
  using Microsoft.Extensions.DependencyInjection;

  using AzureFunctionsSwaggerSample.Api.Services;

  /// <summary>Provides an entry point to configure the function app.</summary>
  public sealed class Startup : IWebJobsStartup
  {
    /// <summary>Configures the function app.</summary>
    /// <param name="builder">An object that provides a simple API to configure the function app.</param>
    public void Configure(IWebJobsBuilder builder)
    {
      builder.Services.AddSingleton(provider => SerializationService.Get());
      builder.Services.AddSingleton<ITodoService, TodoService>();
    }
  }
}
