using Microsoft.AspNetCore.Builder;
using Plan.Application;
using Plan.Infraestructure;
using Plan.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddWebApiServices();

var app = builder.Build();

app.UseAuthorization();
app.UseFastEndpoints();
app.UseHealthChecks("/health");
app.UseHttpsRedirection();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerGen(); //add this
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();

public partial class Program { }

