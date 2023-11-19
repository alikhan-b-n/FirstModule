using FirstModule.ThirdTask;
using FirstModule.ThirdTask.Services;
using FirstModule.ThirdTask.Services.Abstract;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("postgres")));
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IApiService, ApiService>();
builder.Services.AddScoped<IAnalysisService, AnalysisService>();


var app = builder.Build();

app.MapControllers();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();