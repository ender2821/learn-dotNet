// This is the data context class for the ProductStore application.
// It is used to querty and save instances of the Product entity to the database.

using angular.api.Models;
using Microsoft.EntityFrameworkCore;

namespace angular.api.Data;

public class ProductStoreContext(DbContextOptions<ProductStoreContext> options) : DbContext(options)
{
  // Any queries for the Product entity will be made through this DbSet property to the database.
  public DbSet<Product> Products => Set<Product>();
  public DbSet<Category> Categories => Set<Category>();
}
