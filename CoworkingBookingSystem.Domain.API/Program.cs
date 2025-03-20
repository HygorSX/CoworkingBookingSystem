using CoworkingBookingSystem.Domain.Handlers;
using CoworkingBookingSystem.Domain.Infra.Contexts;
using CoworkingBookingSystem.Domain.Infra.Repositories;
using CoworkingBookingSystem.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("DataBase"));

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ISpaceRepository, SpaceRepository>();
builder.Services.AddTransient<IRoomRepository, RoomRepository>();
builder.Services.AddTransient<IReservationRepository, ReservationRepository>();

builder.Services.AddTransient<UserHandler, UserHandler>();
builder.Services.AddTransient<SpaceHandler, SpaceHandler>();
builder.Services.AddTransient<ReservationHandler, ReservationHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();

