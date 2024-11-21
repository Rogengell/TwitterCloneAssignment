using EFramework;
using EFramework.Data;
using Microsoft.EntityFrameworkCore;
using UserServiceApi.Service;
using Polly;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserServiceApi;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration.GetSection("Settings").Get<Settings>();

string jwtIssuer = config.JwtIssuer;
string jwtKey = config.JwtKey;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    }

);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddDbContext<AGWDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
);

var retryPolicy = Policy
    .Handle<SqlException>()
    .Or<TimeoutException>()
    .WaitAndRetryAsync(
        retryCount: 3,
        sleepDurationProvider: attempt =>
        {
            return attempt switch
            {
                1 => TimeSpan.FromSeconds(5),  // First retry after 5 seconds
                2 => TimeSpan.FromSeconds(15), // Second retry after 15 seconds
                3 => TimeSpan.FromSeconds(30), // Third retry after 30 seconds
                _ => TimeSpan.Zero // No retry after 3 attempts
            };
        },
        onRetry: (exception, timeSpan, retryCount, context) =>
        {
            var sqlErrorNumber = exception is SqlException sqlException ? sqlException.Number.ToString() : "N/A";
            Console.WriteLine($"Retry {retryCount} after {timeSpan.Seconds} seconds due to: {exception.Message} SQL Error Number: {sqlErrorNumber}");
        });

builder.Services.AddSingleton<IAsyncPolicy>(retryPolicy);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.MapControllers();
//app.UseHttpsRedirection();

app.Run();