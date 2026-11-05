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
}
