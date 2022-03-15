using Microsoft.EntityFrameworkCore;
using NS.Veterinary.Api.Configurations;
using NS.Veterinary.Api.Data.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
            builder =>
            {
                builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true) // allow any origin
                    //.WithOrigins("https://localhost:44351")); // Allow only this origin can also have multiple origins separated with comma
                    .AllowCredentials();
            });
});

var services = builder.Services;
// Add services to the container.
services.AddDbContext<VeterinaryContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
services.AddControllers();
services.RegisterServices();

services.AddIndentityConfiguration(builder.Configuration);
services.AddAutoMapper(typeof(Program).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
