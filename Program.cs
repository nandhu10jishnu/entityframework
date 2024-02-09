using EmployeeDetails.Interface;
using EmployeeDetails.Repository;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<Databasesettings>(
    builder.Configuration.GetSection(nameof(Databasesettings)));

builder.Services.AddSingleton<IDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<Databasesettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration.GetValue<string>("DatabaseSettings:ConnectionString")));

builder.Services.AddScoped<IEmploye, EmployeRepository>();
builder.Services.AddScoped<ICompany, CompanyRepository>();
// Add services to the container.
// Add services to the container.

builder.Services.AddControllers();
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
