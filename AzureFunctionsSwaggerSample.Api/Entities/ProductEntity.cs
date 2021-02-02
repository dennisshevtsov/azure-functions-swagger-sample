using System;

namespace AzureFunctionsSwaggerSample.Api.Entities
{
  public sealed class ProductEntity
  {
    public Guid ProductId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public float PricePerUnit { get; set; }

    public ProductUnitEntity Unit { get; set; }
  }
}
