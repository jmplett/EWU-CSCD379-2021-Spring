using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using SecretSanta.Web.Api;
using Microsoft.Extensions.DependencyInjection;

namespace SecretSanta.Web.Test
{
    public class WebApplicationFactory : WebApplicationFactory<Startup>
    {
         public TestableEventsClient Client{get;} = new();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services => {
                services.AddScoped<IUsersClient, TestableUsersClient>(_ => Client);
            });
        }
    }
}