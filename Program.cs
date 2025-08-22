using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Vidya.Application.Services;
using Vidya.Core.Security;
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

// ? Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// ? Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ? Register Services & Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICollegeRepository, CollegeRepository>();
builder.Services.AddScoped<IMenuListRepository, MenuListRepository>();
builder.Services.AddScoped<IMenuRightsRepository, MenuRightsRepository>();
builder.Services.AddSingleton<JwtTokenService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddSingleton<ILogger, CustomLogger>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContextService, UserContextService>();

// ? Configure Logging
builder.Logging.ClearProviders();
builder.Logging.AddProvider(new CustomLoggerProvider());

// ? JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "SuperSecretKey123") // ? FIXED
            ),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"]
        };
    });

// ? Swagger + JWT Auth
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vidya API", Version = "v1" });

//    // JWT Bearer token support
//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Name = "Authorization",
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "Bearer",
//        BearerFormat = "JWT",
//        In = ParameterLocation.Header,
//        Description = "Enter 'Bearer {your token}'"
//    });

//    c.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            },
//            new string[] {}
//        }
//    });
//});

//var app = builder.Build();

//// ? Swagger UI
//if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
//{
//    // ?? Remove UsePathBase unless you want "/vidya/api/..." instead of "/api/..."
//    // app.UsePathBase("/vidya");

//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vidya API V1");
//        c.RoutePrefix = ""; // Swagger at root
//    });
//}



// ? Swagger + JWT Auth
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Vidya API",
        Version = "v1",
        Description = "Vidya project API documentation"
    });

    // ✅ OpenAPI 3 JWT Bearer token support
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,   // ✅ Use Http instead of ApiKey
        Scheme = "bearer",                // ✅ must be lowercase for OpenAPI 3
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer {your token}'"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();


// ? Swagger UI
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    // ?? Remove UsePathBase unless you want "/vidya/api/..." instead of "/api/..."
    // app.UsePathBase("/vidya");

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vidya API V1");
        c.RoutePrefix = ""; // Swagger at root
    });
}
// ? Middlewares
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Custom Middlewares
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<UserContextMiddleware>();

app.MapControllers();

// ? Auto Migrate DB
using (var scope = app.Services.CreateScope())
{
    try
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while applying database migrations.");
    }
}

app.Run();
