using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.API;
using Ordering.Infrastructure.Data.Extentions;

var builder = WebApplication.CreateBuilder(args);

//Add Services to the container
builder.Services
    .AddApplicationServices()
    .AddInfrastuctureServices(builder.Configuration)
    .AddApiServices();





var app = builder.Build();

//configure the HTTp request pipeline.

app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.Run();
