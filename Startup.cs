using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using reverse_geocoding_api.Configurations;
using reverse_geocoding_api.Services;
using reverse_geocoding_api.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reverse_geocoding_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            InitialiseConfiguration(services);
            InitialiseServices(services);
        }

        private void InitialiseConfiguration(IServiceCollection services)
        {
            services.Configure<AwsAuthenticationOptions>(Configuration.GetSection("AwsAuthentication"));
            services.Configure<General>(Configuration.GetSection("General"));
        }
        private void InitialiseServices(IServiceCollection services)
        {
            services.AddScoped<IAwsAuthenticationService, AwsAuthenticationService>();
            services.AddScoped<IReverseGeocodeService, ReverseGeocodeService>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
