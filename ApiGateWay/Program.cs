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

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration.GetSection("Settings").Get<Settings>();

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

        var authResponse = client.Auth.UserpassLogin("user",new UserpassLoginRequest("1234"));
        client.SetToken(authResponse.ResponseAuth.ClientToken);
        VaultResponse<KvV2ReadResponse> response = client.Secrets.KvV2Read("secret", "MicroServiceToken");
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