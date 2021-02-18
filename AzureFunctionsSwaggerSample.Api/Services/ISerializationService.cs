// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Services
{
  using System.IO;
  using System.Threading;
  using System.Threading.Tasks;

  /// <summary>Provides a simple API to serialize/deserialize an object.</summary>
  public interface ISerializationService
  {
    /// <summary>Deserializes an object from a stream.</summary>
    /// <typeparam name="T">A type of an object.</typeparam>
    /// <param name="stream">An object that provides a generic view of a sequence of bytes./param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>Provides a value type that wraps a <see cref="System.Threading.Tasks.Task{TResult}"/> and a TResult, only one of which is used.</returns>
    public ValueTask<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken);
  }
}
