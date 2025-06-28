using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Service.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddEnvironmentVariables("AIDSBOOGER__PORTFOLIO__EXPERIENCE");

string? GetEnvironmentConfigurationValue(string key)
{
    return builder.Configuration
        .GetSection("AIDSBOOGER")
        .GetSection("PORTFOLIO")
        .GetSection("EXPERIENCE")
        .GetSection(key).Value;
}

builder.Services.AddDbContext<ExperienceContext>(options =>
    options.UseNpgsql(GetEnvironmentConfigurationValue("CONNSTR")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}/";
        options.Audience = builder.Configuration["Auth0:Audience"];
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => policy.RequireClaim("http://hicksm.dev/roles", "Admin User"));

var app = builder.Build();

//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
