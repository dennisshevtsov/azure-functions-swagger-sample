
namespace AzureFunctionsSwaggerSample.Api.Functions
{
  using System;

  using AzureFunctionsSwaggerSample.Api.Services;

  public sealed class UpdateProductFunction
  {
    private readonly IProductService _productService;

    public UpdateProductFunction(IProductService productService)
      => _productService = productService ?? throw new ArgumentNullException(nameof(productService));
  }
}
