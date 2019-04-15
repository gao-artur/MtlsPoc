using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

namespace MutualTls
{
    public static class HttpClientHandlerExtensions
    {
        public static HttpClientHandler AddClientCertificate(this HttpClientHandler handler, X509Certificate2 certificate)
        {
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ClientCertificates.Add(certificate);

            return handler;
        }
    }
}