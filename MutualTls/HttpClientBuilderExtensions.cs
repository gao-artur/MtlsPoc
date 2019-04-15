#if NETSTANDARD2_0

using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.DependencyInjection;

namespace MutualTls
{
    public static class HttpClientBuilderExtensions
    {
        public static IHttpClientBuilder AddClientCertificate(this IHttpClientBuilder httpClientBuilder, Func<IServiceProvider, X509Certificate2> certificateFactory)
        {
            return httpClientBuilder
                .AddClientCertificate(certificateFactory.Invoke(httpClientBuilder.Services.BuildServiceProvider()));
        }

        public static IHttpClientBuilder AddClientCertificate(this IHttpClientBuilder httpClientBuilder, X509Certificate2 certificate)
        {
            httpClientBuilder.ConfigureHttpMessageHandlerBuilder(builder =>
            {
                if (builder.PrimaryHandler is HttpClientHandler handler)
                {
                    handler.AddClientCertificate(certificate);
                }
                else
                {
                    throw new InvalidOperationException($"Only {typeof(HttpClientHandler).FullName} handler type is supported. Actual type: {builder.PrimaryHandler.GetType().FullName}");
                }
            });

            return httpClientBuilder;
        }
    }
}

#endif