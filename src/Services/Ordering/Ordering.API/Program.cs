using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.

//-------
// Infrastructure - EF Core
// Application - MediatR
// API - Carter, Swagger, HealthChecks


builder.Services.AddApplicationServices()
    .AddInfrastructure(builder.Configuration)
    .AddWebApiServices();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.Run();
