// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Services
{
  using System;
  using System.IO;
  using System.Text.Json;
  using System.Threading;
  using System.Threading.Tasks;

  /// <summary>Provides a simple API to serialize/deserialize an object.</summary>
  public sealed class SerializationService : ISerializationService
  {
    private readonly JsonSerializerOptions _options;

    /// <summary>Initializes a new instance of the <see cref="AzureFunctionsSwaggerSample.Api.Services.SerializationService"/> class.</summary>
    /// <param name="options">An object that provides options to be used with <see cref="System.Text.Json.JsonSerializer"/>.</param>
    public SerializationService(JsonSerializerOptions options)
      => _options = options ?? throw new ArgumentNullException(nameof(options));

    /// <summary>Deserializes an object from a stream.</summary>
    /// <typeparam name="T">A type of an object.</typeparam>
    /// <param name="stream">An object that provides a generic view of a sequence of bytes./param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>Provides a value type that wraps a <see cref="System.Threading.Tasks.Task{TResult}"/> and a TResult, only one of which is used.</returns>
    public ValueTask<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken)
      => JsonSerializer.DeserializeAsync<T>(stream, _options, cancellationToken);

    /// <summary>Gets an instance of the <see cref="AzureFunctionsSwaggerSample.Api.Services.ISerializationService"/> type.</summary>
    /// <returns>An instance of the <see cref="AzureFunctionsSwaggerSample.Api.Services.ISerializationService"/> type.</returns>
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
