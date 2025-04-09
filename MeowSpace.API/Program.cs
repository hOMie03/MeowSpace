using MeowSpace.API.Middleware;
using MeowSpace.Application;
using MeowSpace.Identity;
using MeowSpace.Infrastructure;

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

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
