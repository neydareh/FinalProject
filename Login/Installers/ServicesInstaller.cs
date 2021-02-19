using Login.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Login.Installers
{
    public class ServicesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRepository, Repository.Repository>();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }
    }
}
