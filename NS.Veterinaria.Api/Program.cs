using Hellang.Middleware.ProblemDetails;
using Hellang.Middleware.ProblemDetails.Mvc;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NS.Veterinary.Api.Configurations;
using NS.Veterinary.Api.Data.Context;
using NS.Veterinary.Api.Extensions;
using System.Globalization;

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
services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
services.AddProblemDetailsWithConfigurations();
services.AddControllers();
services.AddProblemDetailsConventions();
services.RegisterServices();

services.AddIndentityConfiguration(builder.Configuration);
services.AddAutoMapper(typeof(Program).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("pt-BR")
    };

    options.DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR");

    options.AddInitialRequestCultureProvider(new CustomRequestCultureProvider(async context =>
    {
        return await Task.FromResult(new ProviderCultureResult("pt"));
    }));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseProblemDetails();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();