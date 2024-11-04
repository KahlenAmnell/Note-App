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
using Microsoft.IdentityModel.Tokens;
using Note_App_API;
using System.Text;
using static Note_App_API.Services.IUserContextService;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        var authenticationSettings = new AuthenticationSettings();
        builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

        builder.Services.AddAuthentication(option => 
        {
            option.DefaultAuthenticateScheme = "Bearer";
            option.DefaultScheme = "Bearer";
            option.DefaultChallengeScheme = "Bearer";
        }).AddJwtBearer(cfg => 
        {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true; 
            cfg.TokenValidationParameters = new TokenValidationParameters 
            {
                ValidIssuer = authenticationSettings.JwtIssuer,
                ValidAudience = authenticationSettings.JwtIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
            };
        });

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
        builder.Services.AddSingleton(authenticationSettings);
        builder.Services.AddScoped<IUserContextService, UserContextService>();
        builder.Services.AddHttpContextAccessor();


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

        app.UseAuthorization();

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
