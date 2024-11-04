using Microsoft.EntityFrameworkCore;
using Note_App_API.Entities;
using System.Reflection;
using Note_App_API.Services;
using NLog.Web;
using Note_App_API.Middleware;
using Microsoft.AspNetCore.Identity;
using FluentValidation;
using Note_App_API.Models;
using Note_App_API.Models.Validators;
using FluentValidation.AspNetCore;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly).AddFluentValidationAutoValidation();
        builder.Services.AddDbContext<NoteDbContext>(
            option => option
                .UseSqlServer(builder.Configuration.GetConnectionString("NoteAppConnectionString"))
        );
        builder.Services.AddScoped<INoteService, NoteService>();
        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
        builder.Services.AddScoped<ErrorHandlingMiddleware>();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        builder.Services.AddScoped<IValidator<CreateAccountDto>, CreateAccountDtoValidator>();

        builder.Logging.ClearProviders();
        builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
        builder.Host.UseNLog();

        var app = builder.Build();

// Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseHttpsRedirection();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Note App API");
        });

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
