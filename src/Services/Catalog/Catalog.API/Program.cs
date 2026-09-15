
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddCarter();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddMarten(opts => { 

    opts.Connection(builder.Configuration.GetConnectionString("Database")!);

}).UseLightweightSessions();
if(builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
};

builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

var app = builder.Build();

// Configure the HTTP request pipeline. 

app.MapCarter();

app.UseExceptionHandler(exceptionHandlerApp => { 
 exceptionHandlerApp.Run(async context =>
 {
     context.Response.StatusCode = StatusCodes.Status500InternalServerError;
     context.Response.ContentType = "application/problem+json";
     var exception = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>()?.Error;
     if(exception == null)
     {
         return;
     }
     var problemDetails = new ProblemDetails
     {
         Title = exception.Message,
         Status = StatusCodes.Status500InternalServerError,
         Detail = exception.StackTrace
     };

     var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
     logger.LogError(exception, exception.Message);

     await context.Response.WriteAsJsonAsync(problemDetails);
     
 });

});

//app.UseHealthChecks("/health",
//    new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
//    {
//        ResponseWriter = async (context, report) =>
//        {
//            context.Response.ContentType = "application/json";
//            var result = System.Text.Json.JsonSerializer.Serialize(new
//            {
//                status = report.Status.ToString(),
//                checks = report.Entries.Select(entry => new
//                {
//                    name = entry.Key,
//                    status = entry.Value.Status.ToString(),
//                    exception = entry.Value.Exception?.Message,
//                    duration = entry.Value.Duration.ToString()
//                })
//            });
//            await context.Response.WriteAsync(result);
//        }
//    }
//    );
app.UseHealthChecks("/health",
    new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        
    }
    );

app.Run();
