using BAFLB;
using BAFLB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Я же говорил зарегистрировать бд как сервис и получать в конструкторе контроллера вот так
// строку подключения можно вынести в файл конфигурации
builder.Services.AddDbContext<ApplicationContext>(option
    => option.UseNpgsql("Host=localhost;Port=5432;Database=baflb;Username=postgres;Password=Vladik@2003"));
builder.Services.AddSingleton<Game>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseDefaultFiles();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
