using System.Text;
using EFramework;
using EFramework.Data;
using LoginServiceApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Polly;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration.GetSection("Settings").Get<Settings>();

var fluentdHost = Environment.GetEnvironmentVariable("FLUENTD_HOST") ?? "fluentd";
var fluentdPort = int.Parse(Environment.GetEnvironmentVariable("FLUENTD_PORT") ?? "24224");

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Fluentd(
        host: fluentdHost,
        port: fluentdPort,
        tag: "loginServiceApi"  // Optional tag
    )
    .CreateLogger();

builder.Host.UseSerilog();

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

builder.Services.AddSingleton(config);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<LoginServiceApi.Service.ILoginService, LoginServiceApi.Service.LoginService>();

var configuration = builder.Configuration;
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
            Console.WriteLine($"Retry {retryCount} after {timeSpan.Seconds} seconds due to: {exception.Message}");
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
app.UseAuthorization();
app.MapControllers();
//app.UseHttpsRedirection();

app.Run();

AppDomain.CurrentDomain.ProcessExit += (sender, args) => Log.CloseAndFlush();