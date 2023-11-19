using FirstModule.SecondTask;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationContext>(option=>option.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapControllers();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Run();