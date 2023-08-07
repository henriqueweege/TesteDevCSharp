using Domain.Commands;
using Domain.Commands.CheckingAccountCommands;
using Domain.Converters;
using Domain.Converters.Contracts;
using Domain.Entities;
using Domain.Models;
using Domain.Repositories;
using Domain.Repositories.Contracts;
using Infrastructure.Data;
using Infrastructure.Data.Contracts;
using MediatR;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{

    c.SwaggerDoc("v1", new OpenApiInfo
    {

        Version = "1.0.0",
        Title = "Questão 5",
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

});


builder.Services.AddTransient<IRepository<CheckingAccountModel>, CheckingAccountRepository>();
builder.Services.AddTransient<IConverter<CheckingAccountEntity, CheckingAccountModel>, CheckingAccountConverter>();
builder.Services.AddTransient<IRepository<TransactionModel>, TransactionRepository>();
builder.Services.AddTransient<IConverter<TransactionEntity, TransactionModel>, TransactionConverter>();
builder.Services.AddTransient<IConverter<IdempotencyEntity, IdempotencyModel>, IdempotencyConverter>();
builder.Services.AddTransient<IRepository<IdempotencyModel>, IdempotencyRepository>();
builder.Services.AddTransient<IDbContext, SQLiteDbContext>();


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateCheckingAccountCommand).Assembly));

DataBaseSetter.Set();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
