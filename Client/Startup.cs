using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MutualTls;

namespace Client
{
    public class Startup
    {
        private const string ServerUrl = "https://localhost:5101";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            services.AddHttpClient("Server", client => { client.BaseAddress = new Uri(ServerUrl); });

            services.AddHttpClient("ServerWithCertificate", client => { client.BaseAddress = new Uri(ServerUrl); })
                .AddClientCertificate(provider =>
                {
                    var certSubject1 = Configuration["ServerCertificateSubject"];
                    return CertificateFinder.FindBySubject(certSubject1);
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
