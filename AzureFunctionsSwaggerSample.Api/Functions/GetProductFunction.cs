using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;

using AzureFunctionsSwaggerSample.Api.Entities;
using AzureFunctionsSwaggerSample.Api.Services;

namespace AzureFunctionsSwaggerSample.Api.Functions
{
  public sealed class GetProductFunction
  {
    private readonly IProductService _productService;

    public GetProductFunction(IProductService productService)
      => _productService = productService ?? throw new ArgumentNullException(nameof(productService));

    [FunctionName(nameof(GetProductFunction))]
    public async Task<ProductEntity> RunAsync(
      [HttpTrigger("get", Route = "product/{productId}")] HttpRequest request,
      Guid productId,
      CancellationToken cancellationToken)
      => await _productService.GetProductAsync(productId, cancellationToken);
  }
}
