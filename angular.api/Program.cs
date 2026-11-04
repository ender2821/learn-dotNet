using angular.api.DTOs;

const string GetProductEndpointName = "GetProduct";

// where we configure services and middleware
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
// where we configurte the http request pipeline


List<ProductDto> products =
[
    new ProductDto(
      1,
      "Product 1",
      "Description 1",
      10.99m,
      8.00m,
      new DateOnly(2023, 1, 1)
    ),
    new ProductDto(
      2,
      "Product 2",
      "Description 2",
      20.00m,
      null,
      new DateOnly(2023, 2, 1)
    ),
    new ProductDto(
      3,
      "Product 3",
      "Description 3",
      30.00m,
      25.99m,
      new DateOnly(2023, 3, 1)
    )
];

// GET /products
app.MapGet("/products", () => products);

// Get /products/1
app.MapGet("/products/{id}", (int id) =>
{
  var product = products.Find(product => product.Id == id);

  return product is null ? Results.NotFound() : Results.Ok(product);
})
  .WithName(GetProductEndpointName);


// POST /products
app.MapPost("/products", (CreateProductDto newProduct) =>
{
  ProductDto product = new ProductDto(
    products.Count + 1,
    newProduct.Name,
    newProduct.Description,
    newProduct.Price,
    newProduct.DiscountPrice,
    newProduct.ReleaseDate
  );

  products.Add(product);

  return Results.CreatedAtRoute(GetProductEndpointName, new { id = product.Id }, product);
});

// Put /products/1
app.MapPut("/products/{id}", (int id, UpdateProductDto updatedProduct) =>
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
    updatedProduct.Description,
    updatedProduct.Price,
    updatedProduct.DiscountPrice,
    updatedProduct.ReleaseDate
  );

  return Results.NoContent();
});

// Delete /products/1
app.MapDelete("/products/{id}", (int id) =>
{

  products.RemoveAll(product => product.Id == id);

  return Results.NoContent();
});

app.Run();
