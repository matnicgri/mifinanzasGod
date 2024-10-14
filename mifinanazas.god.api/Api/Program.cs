using Microsoft.EntityFrameworkCore;
using Mifinanazas.God.Applicattion.Features.Game.Commands;
using Mifinanazas.God.Applicattion.Features.Game.Queries;
using Mifinanazas.God.Applicattion.Interfaces.Repositories;
using Mifinanazas.God.Persistence.Context;
using Mifinanazas.God.Persistence.Repositories;
using FluentValidation;
using Mifinanazas.God.Applicattion.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GodDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });

});

//validators
builder.Services.AddValidatorsFromAssemblyContaining<GameCommandValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<MoveOptionsCommandValidator>();

// Registrar MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GameCommandHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ScoreQuerieHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<MoveCommandHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<MoveOptionsCommandHandler>());


// Registrar repositorios
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IMovementsRepository, MovementsRepository>();

var app = builder.Build();

app.UseCors("AllowAll");

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
