using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anubis.Domain.ExceptionsDomain;
using Anubis.Domain.UsersDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;

namespace Anubis.Infrastructure
{
    public class ExceptionalContext : DbContext
    {
        public ExceptionalContext()
        {
            ChangeTracker.LazyLoadingEnabled = true;
        }
        public ExceptionalContext(DbContextOptions<ExceptionalContext> options) : base(options) { }

        public DbSet<MyApplicationException> MyApplicationExceptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();
                var connString = configuration.GetConnectionString("ExceptionalConnection");
                optionsBuilder.UseSqlServer(connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyApplicationException>().ToTable("Exceptions", t=>t.ExcludeFromMigrations());
            modelBuilder.Entity<MyApplicationException>().HasKey(u => u.Id);
        }
    }
}