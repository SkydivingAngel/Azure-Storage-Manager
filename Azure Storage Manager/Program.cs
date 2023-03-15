using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<FormOptions>(x =>
{
    x.ValueLengthLimit = int.MaxValue;
    x.MultipartBodyLengthLimit = int.MaxValue; // In case of multipart
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
