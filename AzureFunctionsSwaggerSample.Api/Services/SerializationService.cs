// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Services
{
  using System;
  using System.IO;
  using System.Text.Json;
  using System.Threading;
  using System.Threading.Tasks;

  public sealed class SerializationService : ISerializationService
  {
    private readonly JsonSerializerOptions _options;

    public SerializationService(JsonSerializerOptions options)
      => _options = options ?? throw new ArgumentNullException(nameof(options));

    public ValueTask<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken)
      => JsonSerializer.DeserializeAsync<T>(stream, _options, cancellationToken);

    public static ISerializationService Get()
    {
      var options = new JsonSerializerOptions
      {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
      };

      return new SerializationService(options);
    }
  }
}
