using EFramework;
using EFramework.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<LoginServiceApi.Service.ILoginService, LoginServiceApi.Service.LoginService>();

builder.Services.AddDbContext<AGWDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
//app.UseHttpsRedirection();

app.Run();

