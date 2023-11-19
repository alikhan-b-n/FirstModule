using FirstModel.FirstTask.Abstract;
using FirstModule;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(option =>
    option.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));
var dsa = builder.Configuration.GetSection("Logging").Value;
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IApiService, ApiService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.MapControllers();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();