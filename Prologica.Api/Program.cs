using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using apiModels = Prologica.Api.Models;
using Prologica.DAL.Repositories;
using dalModels = Prologica.DAL.imports;
using Microsoft.EntityFrameworkCore;
using Prologica.DAL.imports;
using FluentValidation;
using Prologica.Api.Models;
using Prologica.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();


//Instance Db Context
builder.Services.AddDbContext<PrologicaContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection")));

//Instance interfaces
builder.Services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
builder.Services.AddTransient<IConsoleRepository, ConsoleRepository>();
builder.Services.AddTransient<IGameRepository, GameRepository>();

builder.Services.AddTransient<IConsoleServices, ConsoleServices>();
builder.Services.AddTransient<IGameServices, GameServices>();


//Instance validators
builder.Services.AddScoped<IValidator<apiModels.Console>, ConsoleValidator>();
builder.Services.AddScoped<IValidator<apiModels.Game>, GameValidator>();


//Mappers
IMapper mapper = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<apiModels.Console, dalModels.Console>();
    cfg.CreateMap<apiModels.Game, dalModels.Game>();
}
).CreateMapper();
builder.Services.AddSingleton(mapper);

//Start
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader()
);

app.UseHttpsRedirection();

//EndPoints
#region Console

app.MapPost("/console", async ([FromServices] IConsoleServices _consoleService, apiModels.Console request) =>
    await _consoleService.Add(request));

app.MapPut("/console", async ([FromServices] IConsoleServices _consoleService, apiModels.Console request) =>
    await _consoleService.Update(request));

app.MapGet("/console", async ([FromServices] IConsoleServices _consoleService) =>
    await _consoleService.GetAll());

app.MapGet("/console/{id}", async ([FromServices] IConsoleServices _consoleService, [FromRoute] int id) =>
    await _consoleService.GetById(id));

app.MapDelete("/console/{id}", async ([FromServices] IConsoleServices _consoleService, [FromRoute] int id) =>
    await _consoleService.Delete(id));

#endregion

#region Games

app.MapPost("/game", async ([FromServices] IGameServices _gameService, apiModels.Game request) =>
    await _gameService.Add(request));

app.MapPut("/game", async ([FromServices] IGameServices _gameService, apiModels.Game request) =>
    await _gameService.Update(request));

app.MapGet("/game", async ([FromServices] IGameServices _gameService) =>
    await _gameService.GetAll());

app.MapGet("/game/{id}", async ([FromServices] IGameServices _gameService, [FromRoute] int id) =>
    await _gameService.GetById(id));

app.MapDelete("/game/{id}", async ([FromServices] IGameServices _gameService, [FromRoute] int id) =>
    await _gameService.Delete(id));

#endregion

app.Run();
