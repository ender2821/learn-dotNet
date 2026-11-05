using angular.api.Models;
using Microsoft.EntityFrameworkCore;

namespace angular.api.Data;

// this file migrates the database to the latest version when the application starts up
public static class DataExtensions
{
  public static void MigrateDatabase(this WebApplication app)
  {
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ProductStoreContext>();
    dbContext.Database.Migrate();
  }

  public static void AddProductStoreDB(this WebApplicationBuilder builder)
  {
    var connString = builder.Configuration.GetConnectionString("ProductStore");

    // AddSQlite is a scoped dbContext and has a scope lifetime, so that it ensures that the database connection is properly managed and disposed of when it is no longer needed.
    // it creates a new instance of the ProductStoreContext for each request and disposes of it when the request is complete.
    // db connections are limited and an expensive to create and maintain, so it is important to manage them properly to avoid resource leaks and performance issues.
    // dbContext is not thread safe and should not be shared across multiple threads or requests, so it is important to create a new instance for each request to ensure that the dbContext is used safely and correctly.
    // makes it easier to manage transactions and ensure that the database is in a consistent state, as each request has its own dbContext instance and can manage its own transactions independently of other requests.
    // reusing a dbcontenxt can lead to increased memory usage and performance issues, as the dbContext may hold onto resources and data that are no longer needed, leading to memory leaks and slower performance over time.

    builder.Services.AddSqlite<ProductStoreContext>(
      connString,
      // this is a custom extension method that seeds the database with initial data on startup. 
      // It checks if the Category table is empty and if so, it adds some initial categories to the database.
      optionsAction: options => options.UseSeeding((context, _) =>
      {
        if (!context.Set<Category>().Any())
        {
          context.Set<Category>().AddRange(
            new Category { Name = "camera" },
            new Category { Name = "computer" },
            new Category { Name = "phone" }
          );

          context.SaveChanges();
        }
      })
    );
  }
}
