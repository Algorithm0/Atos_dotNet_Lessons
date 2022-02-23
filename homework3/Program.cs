using homework3.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

// Build application
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddDbContext<BookingContext>(options =>
	options.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=mysecretpassword"));

// Commands to add a migration and update the database
// dotnet ef migrations add InitialCreate
// dotnet ef database update

// Middleware
var app = builder
	.Build();

app.UseRouting();
app.UseEndpoints(endpointRouteBuilder => endpointRouteBuilder.MapControllers());

app.Run();
