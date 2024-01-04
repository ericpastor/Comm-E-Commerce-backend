using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Comm.Business.src.Interfaces;
using Comm.Business.src.Service;
using Comm.Business.src.Services;
using Comm.Business.src.Shared;
using Comm.Core.src.Entities;
using Comm.Core.src.Interfaces;
using Comm.WebAPI.src.Database;
using Comm.WebAPI.src.Middleware;
using Comm.WebAPI.src.Repositories;
using Comm.WebAPI.src.Repository;
using Comm.WebAPI.src.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using Swashbuckle.AspNetCore.Filters;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddControllersWithViews(options => { options.SuppressAsyncSuffixInActionNames = false; });

// Add database contect service
// builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer {token}'",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});


// declare services
builder.Services
    .AddScoped<IUserService, UserService>()
    .AddScoped<ITokenService, TokenService>()
    .AddScoped<IAuthService, AuthService>()
    .AddScoped<IUserRepo, UserRepo>()
    .AddScoped<ICategoryService, CategoryService>()
    .AddScoped<ICategoryRepo, CategoryRepo>()
    .AddScoped<IProductService, ProductService>()
    .AddScoped<IProductRepo, ProductRepo>()
    .AddScoped<IOrderRepo, OrderRepo>()
    .AddScoped<IOrderService, OrderService>();
// Use the actual implementation class UserRepo


// add automapper dependecy injection
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
var connectionString = builder.Configuration.GetConnectionString("Comm");
var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
dataSourceBuilder.MapEnum<Role>();
dataSourceBuilder.MapEnum<Status>();
var dataSource = dataSourceBuilder.Build();

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options
        .UseNpgsql(dataSource)
        .UseSnakeCaseNamingConvention()
        .AddInterceptors(new TimeStampAsyncInterceptor());
    // .EnableSensitiveDataLogging();
});
// Error handler middleware
builder.Services.AddTransient<ExceptionHandlerMiddleware>();

// Config authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});


// builder.Services.AddAuthorization(policy =>
// {
//     // policy.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
//     // policy.AddPolicy("SuperAdmin", policy => policy.RequireClaim(ClaimTypes.Email, "eric@mail.com"));
// });

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();
// app.UseCors();
// Configure the HTTP request pipeline.

app.UseCors(options =>
{
    options
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader();
});


app.UseSwagger();
app.UseSwaggerUI(
    options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
}
);


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();


