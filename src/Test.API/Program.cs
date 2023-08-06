using Test.API.Middleware;
using Test.Application.Configurations;
using Test.Application.Configurations.Registration;
using Test.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.UsePersistence(builder.Configuration).AddDependencies(builder.Configuration)
    .UseRabbitMQ(builder.Configuration).AddRateLimiting(builder.Configuration);
builder.Services.AddHttpContextAccessor();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ValidationExceptionHandlerMiddleware>();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();