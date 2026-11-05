using angular.api.Data;
using angular.api.Endpoints;


// where we configure services and middleware
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

var connString = "Data Source=ProductStore.db";
builder.Services.AddSqlite<ProductStoreContext>(connString);

var app = builder.Build();
// where we configurte the http request pipeline

app.MapProductsEndpoints();

app.MigrateDatabase();

app.Run();
