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

builder.Services.AddDbContext<ExperienceContext>(options =>
    options.UseNpgsql(builder.Configuration
        .GetSection("AIDSBOOGER")
        .GetSection("PORTFOLIO")
        .GetSection("EXPERIENCE")
        .GetSection("CONNSTR").Value));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}/";
        options.Audience = builder.Configuration["Auth0:Audience"];
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = false
        };
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => policy.RequireClaim("http://hicksm.dev/roles", "Admin User"));

var app = builder.Build();

//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
