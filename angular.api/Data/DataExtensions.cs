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
    var connString = "Data Source=ProductStore.db";
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
