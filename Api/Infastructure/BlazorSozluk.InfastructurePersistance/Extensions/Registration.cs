using BlazorSozluk.Application.Interfaces.Repositories;
using BlazorSozluk.InfastructurePersistance.Context;
using BlazorSozluk.InfastructurePersistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.InfastructurePersistance.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistiration(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<BlazorSozlukContext>(conf =>
            {
                var constr =configuration["BlazorSozlukConnectionStrings"].ToString();
                conf.UseSqlServer(constr, opt =>
                {
                    opt.EnableRetryOnFailure();
                });
            });
            //var seedData = new SeedData();
            //seedData.SeedAsync(configuration).GetAwaiter().GetResult();
            services.AddScoped<IUserRepository,UserRepository>();
            return services;
        }
    }
}
