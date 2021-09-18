using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ImplementChallenge.Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ImplementChallenge.Api.Data
{   
    public class ApplicationContext : DbContext
    {
        private static readonly ILoggerFactory _logger = LoggerFactory.Create(p => p.AddConsole());
        private readonly string connectionString;

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Curtidas> Curtidas { get; set; }
        public ApplicationContext(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("ConnectionSql");
        }       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(_logger)
                .UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }

    }    
    
}
