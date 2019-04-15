#if NETSTANDARD2_0

using System.Security.Cryptography.X509Certificates;
using idunno.Authentication.Certificate;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Configuration;

namespace MutualTls
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseKestrelWithClientCertificate(this IWebHostBuilder hostBuilder, X509Certificate2 certificate)
        {
            hostBuilder.UseKestrel(options =>
            {
                var config = (IConfiguration)options.ApplicationServices.GetService(typeof(IConfiguration));

                var port = config.GetValue<int>("HTTPS_PORT");
                options.ConfigureCertificate(port, certificate);
            });

            return hostBuilder;
        }

        public static IWebHostBuilder UseKestrelWithClientCertificate(this IWebHostBuilder hostBuilder, string subjectName)
        {
            hostBuilder.UseKestrel(options =>
            {
                var config = (IConfiguration)options.ApplicationServices.GetService(typeof(IConfiguration));

                var port = config.GetValue<int>("HTTPS_PORT");
                var certificate = CertificateFinder.FindBySubject(subjectName);
                
                options.ConfigureCertificate(port, certificate);
            });

            return hostBuilder;
        }

        public static IWebHostBuilder UseKestrelWithClientCertificate2(this IWebHostBuilder hostBuilder, string subjectNameConfigurationName)
        {
            hostBuilder.UseKestrel(options =>
            {
                var config = (IConfiguration)options.ApplicationServices.GetService(typeof(IConfiguration));

                var port = config.GetValue<int>("HTTPS_PORT");
                var subjectName = config.GetValue<string>(subjectNameConfigurationName);
                var certificate = CertificateFinder.FindBySubject(subjectName);
                
                options.ConfigureCertificate(port, certificate);
            });

            return hostBuilder;
        }

        private static void ConfigureCertificate(this KestrelServerOptions options, int port, X509Certificate2 certificate)
        {
            options.ListenLocalhost(port, listenOptions =>
            {
                listenOptions.UseHttps(new HttpsConnectionAdapterOptions
                {
                    ServerCertificate = certificate,
                    ClientCertificateMode = ClientCertificateMode.RequireCertificate,
                    ClientCertificateValidation = CertificateValidator.DisableChannelValidation,
                });
            });
        }
    }
}

#endif
