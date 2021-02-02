

namespace AzureFunctionsSwaggerSample.Api.Services
{
  using System;
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;

  using AzureFunctionsSwaggerSample.Api.Dtos;

  public interface IProductService
  {
    public Task<ProductDto> GetProductAsync(Guid productId, CancellationToken cancellationToken);

    public Task<IEnumerable<ProductDto>> GetProductsAsync(CancellationToken cancellationToken);

    public Task<ProductDto> CreateProductAsync(ProductDto productEntity, CancellationToken cancellationToken);

    public Task<ProductDto> UpdateProductAsync(ProductDto productEntity, CancellationToken cancellationToken);
  }
}
