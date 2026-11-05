using angular.api.DTOs;

namespace angular.api.Endpoints;

public static class ProductsEndpoints
{

  const string GetProductEndpointName = "GetProduct";

  private static readonly List<ProductDto> products =
  [
    new ProductDto(
      1,
      "Product 1",
      "Category 1",
      "Description 1",
      10.99m,
      8.00m,
      new DateOnly(2023, 1, 1)
    ),
    new ProductDto(
      2,
      "Product 2",
      "Category 2",
      "Description 2",
      20.00m,
      null,
      new DateOnly(2023, 2, 1)
    ),
    new ProductDto(
      3,
      "Product 3",
      "Category 2",
      "Description 3",
      30.00m,
      25.99m,
      new DateOnly(2023, 3, 1)
    )
  ];

  public static void MapProductsEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("/products");
    // GET /products
    group.MapGet("/", () => products);

    // GET /products/1
    group.MapGet("/{id}", (int id) =>
    {
      var product = products.Find(product => product.Id == id);

      return product is null ? Results.NotFound() : Results.Ok(product);
    })
      .WithName(GetProductEndpointName);


    // POST /products
    group.MapPost("/", (CreateProductDto newProduct) =>
    {

      ProductDto product = new ProductDto(
        products.Count + 1,
        newProduct.Name,
        newProduct.Category,
        newProduct.Description,
        newProduct.Price,
        newProduct.DiscountPrice,
        newProduct.ReleaseDate
      );

      products.Add(product);

      return Results.CreatedAtRoute(GetProductEndpointName, new { id = product.Id }, product);
    });

    // Put /products/1
    group.MapPut("/{id}", (int id, UpdateProductDto updatedProduct) =>
    {
      var index = products.FindIndex(product => product.Id == id);

      // This list is just a mock database, so we don't have to worry about concurrency issues or anything like that.
      // In a real application, you would want to handle this more gracefully.
      if (index == -1)
      {
        return Results.NotFound();
      }

      products[index] = new ProductDto(
        id,
        updatedProduct.Name,
        updatedProduct.Category,
        updatedProduct.Description,
        updatedProduct.Price,
        updatedProduct.DiscountPrice,
        updatedProduct.ReleaseDate
      );

      return Results.NoContent();
    });

    // Delete /products/1
    group.MapDelete("/{id}", (int id) =>
    {

      products.RemoveAll(product => product.Id == id);

      return Results.NoContent();
    });
  }
}
