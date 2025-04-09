using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeowSpace.Application.Interfaces.Infrastructure;
using MeowSpace.Infrastructure.Context;
using MeowSpace.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeowSpace.Infrastructure
{
    public static class InfraServiceRegistration
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MeowSpaceDBContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("MeowSpaceDBConnString")));
            services.AddScoped<IPetRepository, PetRepository>();
            return services;
        }
    }
}
