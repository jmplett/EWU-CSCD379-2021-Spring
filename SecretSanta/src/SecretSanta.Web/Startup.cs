using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SecretSanta.Web.Api;

namespace SecretSanta.Web
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        public System.Net.Http.HttpClient ApiClient { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ApiClient = new()
            {
                BaseAddress = new System.Uri(Configuration["ApiHost"])
            };
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUsersClient, UsersClient>(_ => new UsersClient(ApiClient));
            services.AddControllersWithViews();
        }
    }
}