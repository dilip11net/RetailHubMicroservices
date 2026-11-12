var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 5004 - 5054 6004 - 6064 8080 - 8081

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));  


var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapReverseProxy();

app.Run();
