using Serilog;
using Serilog.Events;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PersonAPI.Application.Behaviors;
using PersonAPI.Application.Commands;
using PersonAPI.Application.Interfaces;
using PersonAPI.Application.Mapping;
using PersonAPI.Application.Validators;
using PersonAPI.Infrastructure.Data;
using PersonAPI.Infrastructure.Repositories;
using PersonAPI.WebApi.Middleware;
using System.Reflection;
using AutoMapper; // Add this using
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.AspNetCore.Builder; // Add this using for InMemory
using Swashbuckle.AspNetCore.SwaggerUI;
using Serilog.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/personapi-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Application", "PersonAPI")
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

// Configure Serilog
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext();
});

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add Swagger/OpenAPI support
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Person API",
        Version = "v1",
        Description = "API for managing people and their versions"
    });
    // Optionally, include XML comments if available
    // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // options.IncludeXmlComments(xmlPath);
});

// Database
builder.Services.AddDbContext<PersonDbContext>(options => options.UseInMemoryDatabase("PersonDb"));

// CQRS and MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(AddPersonCommand).Assembly);
});

// Validation
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<PersonProfile>();
});

// Repositories
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonVersionRepository, PersonVersionRepository>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<ClientIdValidationMiddleware>();

app.UseCors();
app.UseRouting();

// Enable Swagger middleware
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Person API v1");
    options.RoutePrefix = string.Empty; // Swagger UI at root
});

app.MapControllers();
app.MapHealthChecks("/health");

// Seed data in development
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<PersonDbContext>();
    await SeedData(context);
}

await app.RunAsync();

static async Task SeedData(PersonDbContext context)
{
    if (await context.People.AnyAsync()) return;

    var people = new[]
    {
        new PersonAPI.Domain.Entities.Person(
            new PersonAPI.Domain.ValueObjects.PersonName("John", "Doe"),
            PersonAPI.Domain.ValueObjects.Gender.Male),
        new PersonAPI.Domain.Entities.Person(
            new PersonAPI.Domain.ValueObjects.PersonName("Jane", "Smith"),
            PersonAPI.Domain.ValueObjects.Gender.Female),
        new PersonAPI.Domain.Entities.Person(
            new PersonAPI.Domain.ValueObjects.PersonName("Alex", "Johnson"),
            PersonAPI.Domain.ValueObjects.Gender.Other)
    };

    // Add birth information to some people
    people[0].RecordBirth(
        new DateOnly(1990, 5, 15),
        new PersonAPI.Domain.ValueObjects.Location("New York", "NY", "USA"));

    people[1].RecordBirth(
        new DateOnly(1985, 8, 22),
        new PersonAPI.Domain.ValueObjects.Location("London", null, "UK"));

    await context.People.AddRangeAsync(people);
    await context.SaveChangesAsync();
}