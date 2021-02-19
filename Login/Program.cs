using Login.Data;
using Login.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Login
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var logger = loggerFactory.CreateLogger<Program>();

                    // Beging logging

                    logger?.LogInformation("Begin seeding roles into the database");


                    var context = services.GetRequiredService<ApplicationDbContext>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var config = services.GetRequiredService<IConfiguration>();

                    // Seed the Roles into the database
                    await ContextSeed.SeedRoleAsync(roleManager);

                    //used User Secret.json to get the password
                    var pwd = config["SuperAdmin"];

                    // Seed the SuperAdmin Role to the database
                    await ContextSeed.SeedSuperAdminAsync(userManager, pwd);

                    logger?.LogInformation("Seeding roles into the database was successful");
                }
                catch (Exception ex)
                {
                    // Log any issuse while seeding occurs
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger?.LogError(ex.Message, "An error occurred while seeding the DB. Seeding failed!!");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
