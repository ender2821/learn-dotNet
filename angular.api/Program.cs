using angular.api.DTOs;
using angular.api.Endpoints;


// where we configure services and middleware
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
// where we configurte the http request pipeline

app.MapProductsEndpoints();

app.Run();
