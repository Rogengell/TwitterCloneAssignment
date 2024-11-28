using ApiGateWay;
using ApiGateWay.Service;
using EasyNetQ;
using EasyNetQ.Internals;
using Polly;
using Polly.Extensions.Http;
using Vault;
using Vault.Model;
using Vault.Client;
using Newtonsoft.Json.Linq;
using Serilog;
using Serilog.Sinks.Fluentd;

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
        tag: "apigateway"  // Optional tag
    )
    .CreateLogger();

builder.Host.UseSerilog();

var retryPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

// Register HttpClient with the retry policy
builder.Services.AddHttpClient("RetryClient")
    .AddPolicyHandler(retryPolicy);

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddSingleton(
    (ServiceProvider) =>
    {
        VaultConfiguration vaultConfig = new VaultConfiguration(config.VoultHostName);
        VaultClient client = new VaultClient(vaultConfig);

        var authResponse = client.Auth.UserpassLogin("user", new UserpassLoginRequest("1234"));
        client.SetToken(authResponse.ResponseAuth.ClientToken);
        VaultResponse<KvV2ReadResponse> response = client.Secrets.KvV2Read("secret", "secret");
        JObject data = (JObject)response.Data.Data;
        SecretSettings secretSettings = data.ToObject<SecretSettings>();

        return secretSettings;
    }
);
builder.Services.AddSingleton<Settings>(config);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapControllers();

app.Run();

AppDomain.CurrentDomain.ProcessExit += (sender, args) => Log.CloseAndFlush();