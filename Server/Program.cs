using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using MutualTls;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseKestrelWithClientCertificate2("ServerCertificateSubject")
                .UseStartup<Startup>();
        }
    }
}
