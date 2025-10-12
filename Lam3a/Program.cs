using Lam3a.Data;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<DataContextEf>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddHybridCache(options =>
{
    //configure hybrid L1 + L2 cache.
    //L1: memory cache (RAM)
    //L2: redis (fast cache service - like a DB)
});

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

builder.Services.AddCors(options =>
{
    //define CORS policy.
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
    // use development CORS policy
}
else if (app.Environment.IsProduction())
{
    //use production CORS policy.
}

app.UseHttpsRedirection();
app.UseRequestTimeouts();
app.MapControllers().RequireRateLimiting("per-user").WithRequestTimeout(TimeSpan.FromSeconds(10));
app.Run();
