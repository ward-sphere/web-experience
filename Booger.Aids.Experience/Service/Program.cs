using Microsoft.EntityFrameworkCore;
using Service.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ExperienceContext>(options =>
    options.UseNpgsql(System.Environment.GetEnvironmentVariable("PORTFOLIO_PGSQL_CONNECTION_STRING")));

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
