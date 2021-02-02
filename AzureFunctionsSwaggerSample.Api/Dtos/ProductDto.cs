using System;

namespace AzureFunctionsSwaggerSample.Api.Dtos
{
  public sealed class ProductDto
  {
    public Guid ProductId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public float PricePerUnit { get; set; }

    public ProductUnitDto Unit { get; set; }
  }
}
