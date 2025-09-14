using HotelRoomBooking.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var AllowedOrigins = new[]
{
    "http://localhost:4200",
    "http://localhost:49893",
    "http://localhost:60197",
    "http://localhost:61269", 
    "http://localhost:51295"
};

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalDev", policy =>
    {
        policy.WithOrigins(AllowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelRoomBooking API V1");
        c.RoutePrefix = string.Empty; // serve swagger at https://localhost:7251/
    });
}

app.UseHttpsRedirection();


app.UseCors("AllowLocalDev");

app.UseAuthorization();

app.MapControllers();

app.Run();
