using DotNetEnv;
using FluentValidation;
using FluentValidation.AspNetCore;
using Lam3a.Data;
using Lam3a.Dto;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.EntityFrameworkCore;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// AUTOMATIC FLUENT VALIDATORS:
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterCredentialsDtoValidator>();

// DATABASE CONFIG AND DB CONTEXT.
var dbPassword =
    Environment.GetEnvironmentVariable("DB_PASSWORD")
    ?? throw new InvalidOperationException("DB_PASSWORD is missing");

var defaultConnection =
    builder.Configuration.GetConnectionString("DefaultConnection") + $"Password={dbPassword};";

builder.Services.AddDbContext<DataContextEf>(options => options.UseNpgsql(defaultConnection));

// CACHING
builder.Services.AddHybridCache(options =>
{
    //configure hybrid L1 + L2 cache.
    //L1: memory cache (RAM)
    //L2: redis (fast cache service - like a DB)
});

// TIMEOUTS
builder.Services.AddRequestTimeouts(options =>
{
    options.DefaultPolicy = new RequestTimeoutPolicy
    {
        Timeout = TimeSpan.FromSeconds(10),
        TimeoutStatusCode = StatusCodes.Status408RequestTimeout,
        WriteTimeoutResponse = async context =>
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(
                new { error = "Request Timed Out!", status = 408 }
            );
        },
    };
});

// CORS
builder.Services.AddCors(options =>
{
    //define CORS policy.
});
var app = builder.Build();

// CONFIGURE THE HTTP REQUEST PIPELINE.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // use development CORS policy
}
else if (app.Environment.IsProduction())
{
    //use production CORS policy.
}

// MIDDLEWARE
app.UseHttpsRedirection();
app.UseRequestTimeouts();
app.MapControllers().RequireRateLimiting("per-user").WithRequestTimeout(TimeSpan.FromSeconds(10));
app.Run();
