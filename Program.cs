using Microsoft.EntityFrameworkCore;
using Note_App_API.Entities;
using System.Reflection;
using Note_App_API.Services;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<NoteDbContext>(
    option => option
        .UseSqlServer(builder.Configuration.GetConnectionString("NoteAppConnectionString"))
    );
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
