
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

app.Run();
