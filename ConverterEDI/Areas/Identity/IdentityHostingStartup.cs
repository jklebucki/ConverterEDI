using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(ConverterEDI.Areas.Identity.IdentityHostingStartup))]
namespace ConverterEDI.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}