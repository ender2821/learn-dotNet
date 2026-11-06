using angular.api.Data;
using angular.api.Endpoints;

// where we configure services and middleware
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

builder.AddProductStoreDB();

var app = builder.Build();
// where we configurte the http request pipeline

app.MapProductsEndpoints();
app.MapCategoryEndpoints();

app.MigrateDatabase();

app.Run();
