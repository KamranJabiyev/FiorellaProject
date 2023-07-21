using Fiorella.Aplication.Abstraction.Repository;
using Fiorella.Aplication.Abstraction.Services;
using Fiorella.Aplication.Validators.CategoryValidators;
using Fiorella.Persistence;
using Fiorella.Persistence.Contexts;
using Fiorella.Persistence.Implementations.Repositories;
using Fiorella.Persistence.Implementations.Services;
using Fiorella.Persistence.MapperProfiles;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddPersistenceServices();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
