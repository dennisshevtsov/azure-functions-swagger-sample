
namespace AzureFunctionsSwaggerSample.Api.Functions
{
  using System;

  using AzureFunctionsSwaggerSample.Api.Services;

  public sealed class GetProductsFunction
  {
    private readonly IProductService _productService;

    public GetProductsFunction(IProductService productService)
      => _productService = productService ?? throw new ArgumentNullException(nameof(productService));
  }
}
