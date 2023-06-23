using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anubis.Domain.UsersDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;

namespace Anubis.Infrastracture
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
            ChangeTracker.LazyLoadingEnabled = true;
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRefreshToken> RefreshTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();
                var connString = configuration.GetConnectionString("AnubisConnection");
                optionsBuilder.UseSqlServer(connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().HasAlternateKey(u => u.UserID);

            modelBuilder.Entity<UserRefreshToken>().ToTable("UserRefreshTokens").HasKey(x => x.UserID);


            modelBuilder.Entity<User>().HasOne(x => x.RefreshToken)
                .WithOne(x => x.User)
                .HasPrincipalKey<User>(e=>e.UserID)
                .HasForeignKey<UserRefreshToken>(e => e.UserID)
                .IsRequired();
        }
    }
}
