using System.Xml.Serialization;
using University_Schedule_Gateway.EndPoints;
using University_Schedule_Gateway.Extensions;
using University_Schedule_Gateway.Infrastructure.Auth;
using University_Schedule_Gateway.Interfaces.Auth;
using University_Schedule_Gateway.Repositories;
using University_Schedule_Gateway.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
builder.Logging.AddConsole();
// Swagger/OpenAPI
services.AddOpenApi();
services.AddSwaggerGen();

services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
//добавляем базы
services.AddPostgres(builder.Configuration);

//Сервисы для JWT
services.AddScoped<UserRepository>();
services.AddScoped<IJwtProvider, JwtProvider>();
services.AddScoped<UserService>();
services.AddScoped<IPasswordHasher, PasswordHasher>();


services.AddApiAuthentication(
    builder.Configuration.GetSection("JwtOptions")); // схема аутентификации - с помощью jwt-токенов.
var app = builder.Build();
// Включаем middleware для Swagger
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "University Schedule API V1");
    options.RoutePrefix = ""; // Доступ по /
});
app.UseAuthentication();
app.UseAuthorization();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
//регистрируем эндпоинты login register
app.MapUserEndpoints();

app.UseHttpsRedirection();


app.Run();