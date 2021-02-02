
namespace AzureFunctionsSwaggerSample.Api.Functions
{
  using System;

  using AzureFunctionsSwaggerSample.Api.Services;

  public sealed class CreateProductFunction
  {
    private readonly IProductService _productService;

    public CreateProductFunction(IProductService productService)
      => _productService = productService ?? throw new ArgumentNullException(nameof(productService));
  }
}
