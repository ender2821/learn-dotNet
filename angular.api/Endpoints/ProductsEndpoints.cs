using angular.api.Data;
using angular.api.DTOs;
using angular.api.Models;
using Microsoft.EntityFrameworkCore;

namespace angular.api.Endpoints;

public static class ProductsEndpoints
{

  const string GetProductEndpointName = "GetProduct";

  // This is a mock database we used to build out the endpoints before the database was setup

  // private static readonly List<ProductSummaryDto> products =
  // [
  //   new ProductSummaryDto(
  //     1,
  //     "Product 1",
  //     "Category 1",
  //     "Description 1",
  //     10.99m,
  //     8.00m,
  //     new DateOnly(2023, 1, 1)
  //   ),
  //   new ProductSummaryDto(
  //     2,
  //     "Product 2",
  //     "Category 2",
  //     "Description 2",
  //     20.00m,
  //     null,
  //     new DateOnly(2023, 2, 1)
  //   ),
  //   new ProductSummaryDto(
  //     3,
  //     "Product 3",
  //     "Category 2",
  //     "Description 3",
  //     30.00m,
  //     25.99m,
  //     new DateOnly(2023, 3, 1)
  //   )
  // ];

  public static void MapProductsEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("/products");
    // GET /products
    group.MapGet("/", async (ProductStoreContext dbContext)
    => await dbContext.Products
    // Include the Category navigation property to load the related Category data for each Product.
    .Include(product => product.Category)
    .Select(product => new ProductSummaryDto(
        product.Id,
        product.Name,
        product.Category!.Name,
        product.Description,
        product.Price,
        product.DiscountPrice,
        product.ReleaseDate
    ))
    .AsNoTracking()
    .ToListAsync());

    // GET /products/1
    group.MapGet("/{id}", async (int id, ProductStoreContext dbContext) =>
    {
      var product = await dbContext.Products.FindAsync(id);

      return product is null ? Results.NotFound() : Results.Ok(
        new ProductDetailsDto(
          product.Id,
          product.Name,
          product.CategoryId,
          product.Description,
          product.Price,
          product.DiscountPrice,
          product.ReleaseDate
        )
      );
    })
    .WithName(GetProductEndpointName);


    // POST /products
    group.MapPost("/", async (CreateProductDto newProduct, ProductStoreContext dbContext) =>
    {
      Product product = new()
      {
        Name = newProduct.Name,
        CategoryId = newProduct.CategoryId,
        Description = newProduct.Description,
        Price = newProduct.Price,
        DiscountPrice = newProduct.DiscountPrice,
        ReleaseDate = newProduct.ReleaseDate
      };

      dbContext.Products.Add(product);
      await dbContext.SaveChangesAsync();

      ProductDetailsDto productDto = new(
        product.Id,
        product.Name,
        product.CategoryId,
        product.Description,
        product.Price,
        product.DiscountPrice,
        product.ReleaseDate
      );

      return Results.CreatedAtRoute(GetProductEndpointName, new { id = productDto.Id }, productDto);
    });

    // Put /products/1
    group.MapPut("/{id}", async (int id, UpdateProductDto updatedProduct, ProductStoreContext dbContext) =>
    {
      var existingProduct = await dbContext.Products.FindAsync(id);

      // This list is just a mock database, so we don't have to worry about concurrency issues or anything like that.
      // In a real application, you would want to handle this more gracefully.
      if (existingProduct is null)
      {
        return Results.NotFound();
      }

      existingProduct.Name = updatedProduct.Name;
      existingProduct.CategoryId = updatedProduct.CategoryId;
      existingProduct.Description = updatedProduct.Description;
      existingProduct.Price = updatedProduct.Price;
      existingProduct.DiscountPrice = updatedProduct.DiscountPrice;
      existingProduct.ReleaseDate = updatedProduct.ReleaseDate;

      await dbContext.SaveChangesAsync();

      return Results.NoContent();
    });

    // Delete /products/1
    group.MapDelete("/{id}", async (int id, ProductStoreContext dbContext) =>
    {

      await dbContext.Products.Where(product => product.Id == id).ExecuteDeleteAsync();

      return Results.NoContent();
    });
  }
}
