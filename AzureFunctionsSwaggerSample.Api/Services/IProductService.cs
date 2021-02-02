

namespace AzureFunctionsSwaggerSample.Api.Services
{
  using System;
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;

  using AzureFunctionsSwaggerSample.Api.Entities;

  public interface IProductService
  {
    public Task<ProductEntity> GetProductAsync(Guid productId, CancellationToken cancellationToken);

    public Task<IEnumerable<ProductEntity>> GetProductsAsync(CancellationToken cancellationToken);

    public Task<ProductEntity> CreateProductAsync(ProductEntity productEntity, CancellationToken cancellationToken);

    public Task<ProductEntity> UpdateProductAsync(ProductEntity productEntity, CancellationToken cancellationToken);
  }
}
