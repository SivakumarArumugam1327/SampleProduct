using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Myproducts.Models;
using System;

namespace Myproducts.Commen
{
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Returns the connection string named "DefaultConnection" or throws a clear exception if missing.
        /// Usage: builder.Configuration.GetDefaultConnectionString()
        /// </summary>
        public static string GetDefaultConnectionString(this IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var cs = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(cs))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found in configuration (appsettings.json).");
            }

            return cs;
        }
    }

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}