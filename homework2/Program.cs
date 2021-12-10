using homework2.OpenWeather;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

// Build application
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<OpenWeatherClient>();

// Middleware
var app = builder
	.Build();

app.UseRouting();
app.UseEndpoints(endpointRouteBuilder => endpointRouteBuilder.MapControllers());

app.Run();
