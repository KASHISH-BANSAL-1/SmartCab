using SmartCab.Core.Interfaces;
using SmartCab.Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IDispatchService, DispatchService>();
builder.Services.AddScoped<ICabRepository, InMemoryCabRepository>();


var app = builder.Build();

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
