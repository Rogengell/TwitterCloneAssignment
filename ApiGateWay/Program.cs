using ApiGateWay;
using ApiGateWay.Service;
using EasyNetQ;
using EasyNetQ.Internals;
using Polly;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

var retryPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

// Register HttpClient with the retry policy
builder.Services.AddHttpClient("RetryClient")
    .AddPolicyHandler(retryPolicy);

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUserService, UserService>();

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