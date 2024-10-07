using EFramework.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbContextFactory = new AGWDbContextFactory();

// Register the DbContext using the factory
builder.Services.AddDbContext<AGWDbContext>(options =>
{
    var context = dbContextFactory.CreateDbContext();
    options.UseSqlServer(context.Database.GetDbConnection().ConnectionString); // Get the connection string from your context
});

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