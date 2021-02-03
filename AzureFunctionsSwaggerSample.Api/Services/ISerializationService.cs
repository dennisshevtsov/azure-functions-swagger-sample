// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License, Version 2.0.
// See LICENSE.txt in the project root for license information.

namespace AzureFunctionsSwaggerSample.Api.Services
{
  using System.IO;
  using System.Threading;
  using System.Threading.Tasks;

  public interface ISerializationService
  {
    public ValueTask<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken);
  }
}
