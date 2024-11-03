using EventTicketBookingApi.Contract;
using EventTicketBookingApi.Data;
using EventTicketBookingApi.Repository;
using EventTicketBookingApi.Service;
using EventTicketBookingApi.Service.IService;
using hotelListing.API.Configuration;
using hotelListing.API.Contract;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("EventsBookingDbConnectionString");

builder.Services.AddDbContext<EventTicketBookingDbContext>(options => {
    options.UseNpgsql(connectionString);
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b => b.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IBookingRepository), typeof(BookingRepository));
builder.Services.AddScoped(typeof(IEventsRepository),typeof(EventsRepository));

// Register services
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IEventService, EventsService>();

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
