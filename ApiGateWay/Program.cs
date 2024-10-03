using ApiGateWay;
using ApiGateWay.Service;
using EasyNetQ;
using EasyNetQ.Internals;
using Helpers:


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IBus>(new ConnectionHelper().GetRMQConnection());

builder.Services.AddScoped<ILoginService, LoginService>();

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