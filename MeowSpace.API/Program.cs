using MeowSpace.API.Helper;
using MeowSpace.API.Middleware;
using MeowSpace.Application;
using MeowSpace.Identity;
using MeowSpace.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
            builder.Services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());

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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
