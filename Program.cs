using Microsoft.EntityFrameworkCore;
using Note_App_API.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<NoteDbContext>(
    option => option
        .UseSqlServer(builder.Configuration.GetConnectionString("NoteAppConnectionString"))
    );

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
