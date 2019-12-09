using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TruckSystem.CrossCutting.IoC;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using TruckSystem.DAL.Context.Seeder;
using System.Diagnostics.CodeAnalysis;

namespace TruckSystem.WebApi
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private readonly IHostingEnvironment _environment;

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            _environment = environment;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(opt => opt.AddPolicy("default", pol =>
            {
                pol.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
            }));

            services.AddOptions();
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(x => 
                {
                    new Newtonsoft.Json.JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
                    x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            services.AddTransient<DbSeeder>();
            services.RegisterSystemServices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("default");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api V1");
                c.OAuthClientId("levi-api");
                c.OAuthAppName("Levi");
            });
            
            app.UseMvc();
        }
    }
}
