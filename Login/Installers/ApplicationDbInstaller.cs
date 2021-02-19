using Login.Data;
using Login.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Login.Installers
{
    public class ApplicationDbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = new ConnectionStrings();
            configuration.GetSection(nameof(ConnectionStrings)).Bind(connectionString);

            services.AddDbContextPool<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString.UsersConnection);
            });
        }
    }
}
