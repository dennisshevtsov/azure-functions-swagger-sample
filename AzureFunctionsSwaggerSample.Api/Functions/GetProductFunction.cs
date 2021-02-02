namespace AzureFunctionsSwaggerSample.Api.Functions
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Azure.WebJobs;

  using AzureFunctionsSwaggerSample.Api.Dtos;
  using AzureFunctionsSwaggerSample.Api.Services;

  public sealed class GetProductFunction
  {
    private readonly IProductService _productService;

    public GetProductFunction(IProductService productService)
      => _productService = productService ?? throw new ArgumentNullException(nameof(productService));

    [FunctionName(nameof(GetProductFunction))]
    public async Task<ProductDto> RunAsync(
      [HttpTrigger("get", Route = "product/{productId}")] HttpRequest request,
      Guid productId,
      CancellationToken cancellationToken)
      => await _productService.GetProductAsync(productId, cancellationToken);
  }
}
