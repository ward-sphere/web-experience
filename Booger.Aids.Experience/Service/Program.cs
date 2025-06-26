using Microsoft.EntityFrameworkCore;
using Service.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddEnvironmentVariables("AIDSBOOGER__PORTFOLIO__EXPERIENCE");

builder.Services.AddDbContext<ExperienceContext>(options =>
    options.UseNpgsql(builder.Configuration
        .GetSection("AIDSBOOGER")
        .GetSection("PORTFOLIO")
        .GetSection("EXPERIENCE")
        .GetSection("CONNSTR").Value));

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
