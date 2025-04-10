using MeowSpace.API.Helper;
using MeowSpace.API.Middleware;
using MeowSpace.Application;
using MeowSpace.Application.Models.Identity;
using MeowSpace.Identity;
using MeowSpace.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MeowSpace.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddApplicationServices();
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
            builder.Services.AddInfraServices(builder.Configuration);

            // Configure logging provider
            builder.Logging.ClearProviders(); // Optional: Clears default loggingproviders
            builder.Logging.AddConsole(); // Adds console logging provider
            builder.Logging.AddDebug(); // Adds Debug logging provider

            // Serilog implementation
            IConfiguration configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(
                    $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                    optional: true)
                .Build();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configurationBuilder)
                .CreateBootstrapLogger().Freeze();
            new LoggerConfiguration()
                .ReadFrom.Configuration(configurationBuilder)
                .CreateLogger();

            builder.Host.UseSerilog((ctx, lc) => lc
                    .WriteTo.Console()
                    .ReadFrom.Configuration(ctx.Configuration));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddAuthentication(); // For Auth
            //builder.Services.AddSwaggerGen();

            // API Versioning
            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            builder.Services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();                                         
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Please enter your token with this format: ''Bearer YOUR_TOKEN''",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "bearer"

                });
                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new string[] { }
                    }
                });
                options.OperationFilter<SwaggerDefaultValues>();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            IApiVersionDescriptionProvider provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
               {
                   foreach (var description in provider.ApiVersionDescriptions)
                   {
                       options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                   }
               });


            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseAuthentication(); // For Auth
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
