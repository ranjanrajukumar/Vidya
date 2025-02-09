using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Vidya.Application.Services;
using Vidya.Core.Security;
using Vidya.Infrastructure.Caching;
using Vidya.Infrastructure.Data;
using Vidya.Infrastructure.Repositories;
using Vidya.Application.Interfaces;
using Vidya.Infrastructure.Logging;
using Vidya.API.Middlewares;
using Vidya.API.Filters;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new CustomExceptionFilter()); // Register global exception filter
});

// ?? Enable CORS (Allow any origin, method, and header)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Configure Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Services and Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICollegeRepository, CollegeRepository>();
builder.Services.AddSingleton<JwtTokenService>();
builder.Services.AddSingleton<RedisCacheService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddSingleton<ILogger, CustomLogger>();

// Configure Logging
builder.Logging.ClearProviders(); // Clears default log providers
builder.Logging.AddProvider(new CustomLoggerProvider()); // Adds custom logger

// Configure Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "YourDefaultKey")
            ),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"]
        };
    });

// Enable Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = ""; // Serve Swagger UI at root
    });
}

// ?? Apply CORS Middleware
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Register Custom Middleware
app.UseMiddleware<RequestLoggingMiddleware>();

app.MapControllers();

app.Run();
