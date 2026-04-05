using AiPromptOptimizer.Application.Interfaces;
using AiPromptOptimizer.Application.Services;
using AiPromptOptimizer.Infrastructure.Interfaces;
using AiPromptOptimizer.Infrastructure.Persistence;
using AiPromptOptimizer.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IPromptService, PromptService>();
builder.Services.AddScoped<IPromptBuilderService, PromptBuilderService>();
builder.Services.AddScoped<IAiInfrastructureService, AiInfrastructureService>();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();