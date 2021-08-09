using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Dentist.Models;

namespace Dentist.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>()
                .HasMany(p => p.Items)
                .WithMany(p => p.Bills);

            modelBuilder.Entity<Costumer>()
                .HasMany(p => p.Bills)
                .WithOne(p => p.Costumer);

            modelBuilder.Entity<BillableItems>()
                .HasMany(p => p.Bills)
                .WithMany(p => p.Items);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Dentist.Models.Bill> Bill { get; set; }
        public DbSet<Dentist.Models.BillableItems> BillableItems { get; set; }
        public DbSet<Dentist.Models.Costumer> Costumer { get; set; }
    }
}
