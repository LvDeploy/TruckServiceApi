using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;
using TruckSystem.DAL.Context;
using AutoMapper;
using TruckSystem.DAL.IRepository;
using TruckSystem.DAL.Repository;
using TruckSystem.Domain.Vehicles.IService;
using TruckSystem.Service.Services;
using TruckSystem.CrossCutting.AutoMapper;

namespace TruckSystem.CrossCutting.IoC
{
    [ExcludeFromCodeCoverage]
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterSystemServices(this IServiceCollection services, IConfiguration config, bool useInMemoryServicesForTest = false)
        {
            services.AddEntityFrameworkSqlServer();
            services.AddDbContext<SqlContext>(opt =>
            {
                if (useInMemoryServicesForTest)
                {
                    opt.UseInMemoryDatabase(nameof(SqlContext));
                    return;
                }

                opt.UseSqlServer(
                    config.GetConnectionString("DefaultConnection"), opts =>
                    {
                        opts.CommandTimeout((int)TimeSpan.FromMinutes(20).TotalSeconds);
                    });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "WebAPI", Version = "v1" });
            });
            services.AddTransient<IModelRepository, ModelRepository>();
            services.AddTransient<ITruckRepository, TruckRepository>();
            services.AddScoped<ITruckService, TruckService>();
            services.AddAutoMapper(
              typeof(AutoMapperConfig));
            services.AddScoped<IMapper, Mapper>();

            return services;
        }
    }
}
