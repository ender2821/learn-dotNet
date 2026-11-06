using System;
using angular.api.Data;
using angular.api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace angular.api.Endpoints;

public static class CategoryEndpoints
{
  public static void MapCategoryEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("/categories");

    // GET /categories
    group.MapGet("/", async (ProductStoreContext dbContext) =>
      await dbContext.Categories
        .Select(category => new CategoryDto(category.Id, category.Name))
        .AsNoTracking()
        .ToListAsync()
    );
  }

}
