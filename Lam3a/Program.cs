using Lam3a.Data;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

builder.Services.AddScoped<DataContextEf>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseHttpsRedirection();

app.Run();
