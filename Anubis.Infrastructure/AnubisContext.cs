using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anubis.Domain.UsersDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;

namespace Anubis.Infrastructure
{
    public class AnubisContext : DbContext
    {
        public AnubisContext()
        {
            ChangeTracker.LazyLoadingEnabled = true;
        }
        public AnubisContext(DbContextOptions<AnubisContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<MedicPatient> MedicPatient { get; set; }
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
            modelBuilder.Entity<MedicPatient>().ToTable("MedicPatient").HasKey(x=>x.Id);

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().HasAlternateKey(u => u.UserID);
            modelBuilder.Entity<User>().HasAlternateKey(u => u.Email);

            //modelBuilder.Entity<MedicPatient>()
            //    .HasOne(mp => mp.Medic)
            //    .WithMany(u => u.MedicPatients)
            //    .HasForeignKey(mp => mp.MedicID)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<MedicPatient>()
            //    .HasOne(mp => mp.Patient)
            //    .WithMany(u => u.MedicPatients)
            //    .HasForeignKey(mp => mp.PatientID)
            //    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Patients)
                .WithMany(u => u.Medics)
                .UsingEntity<MedicPatient>(
                    l => l.HasOne<User>(x => x.Patient).WithMany().HasForeignKey(e => e.PatientID),
                    r => r.HasOne<User>(x => x.Medic).WithMany().HasForeignKey(e => e.MedicID));

            modelBuilder.Entity<UserRefreshToken>().ToTable("UserRefreshTokens").HasKey(x => x.UserID);


            modelBuilder.Entity<User>().HasOne(x => x.RefreshToken)
                .WithOne(x => x.User)
                .HasPrincipalKey<User>(e=>e.UserID)
                .HasForeignKey<UserRefreshToken>(e => e.UserID)
                .IsRequired();
        }
    }
}
