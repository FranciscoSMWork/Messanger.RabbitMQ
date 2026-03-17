using Microsoft.AspNetCore.Authentication.JwtBearer;
using Messanger.Application.Abstractions.Repositories;
using Messanger.Application.Abstractions.Services;
using Messanger.Application.Services;
using Messanger.Domain.Entity;
using Messanger.Infrastructure.Context;
using Messanger.Infrastructure.Repository;
using Messanger.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Messanger.Worker.Consumers;
using Messanger.Infrastructure.Services.RabbitMq;
using Microsoft.Data.SqlClient;
using Messanger.Application.Services.Message;
using Messanger.Application.Abstractions.Services.MessageOrder;
using Messanger.Application.Abstractions.Services.Orders;
using Microsoft.OpenApi.Models;



var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("MessangerConnection") ?? "Server=(localdb)\\MSSQLLocalDB;Database=MessangerDb;Trusted_Connection=True;";

builder.Services.AddDbContext<MessangerDbContext>(opts =>
{
    opts.UseSqlServer(connString);
});

// Add services to the container.
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<MessangerDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMessageOrderService, MessageOrderService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddSingleton<IMessageBus, RabbitMqService>();
builder.Services.AddHostedService<OrderConsumer>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Digite: Bearer {seu token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
var key = builder.Configuration["Jwt:Key"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = 
    JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }