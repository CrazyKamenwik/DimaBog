using SkyDrive.BLL.IoC;
using SkyDrive.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddContext(builder.Configuration)
    .AddServices();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandler>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
