using AutoMapper;
using BandAPI.DbContexts;
using BandAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BandAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(setupAction =>
            {
                //consente solo json
                setupAction.ReturnHttpNotAcceptable = true;
            })
            //lo metto prima, così abbiamo formato JSON come default
            .AddNewtonsoftJson(setupAction =>
            {
                //necessario per utilizzare PATCH
                setupAction.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
            })
            //così aggiungo anche il supporto per l'xml
            .AddXmlDataContractSerializerFormatters();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IBandAlbumRepository, BandAlbumRepository>();
            services.AddScoped<IPropertyMappingService, PropertyMappingService>();
            services.AddScoped<IPropertyValidationService, PropertyValidationService>();

            services.AddDbContext<BandAlbumContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });

            //var connectionString = Configuration["connectionStrings:Default"];
            //services.AddDbContext<BandAlbumContext>(c => c.UseSqlServer(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    //per gestire le eccezioni lato production
                    appBuilder.Run(async c =>
                    {
                        c.Response.StatusCode = 500;
                        await c.Response.WriteAsync("Something went horribly wrong!");
                    });
                });
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
