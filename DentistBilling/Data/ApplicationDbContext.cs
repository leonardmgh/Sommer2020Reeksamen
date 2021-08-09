using DentistBilling.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DentistBilling.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>()
                .HasMany(p => p.Items)
                .WithOne(p => p.Bill);

            modelBuilder.Entity<Costumer>()
                .HasMany(p => p.Bills)
                .WithOne(p => p.Costumer);

            modelBuilder.Entity<BillableItems>()
                .HasMany(p => p.Bills)
                .WithOne(p => p.BillableItem);

            modelBuilder.Entity<BillToItem>()
            .HasOne(p => p.Bill)
            .WithMany(p => p.Items)
            .HasForeignKey(p => p.BillID);

            modelBuilder.Entity<BillToItem>()
                .HasOne(p => p.BillableItem)
                .WithMany(p => p.Bills)
                .HasForeignKey(p => p.ItemID);

            base.OnModelCreating(modelBuilder);
            CreateData(modelBuilder);
        }

        public void CreateData(ModelBuilder builder)
        {
            List<BillableItems> items = new List<BillableItems>
            {
                new BillableItems{
                    ID = 1,
                    Description = "Basic service",
                    CostumerPart = 10.5,
                    InsurancePart = 100,
                },
                new BillableItems{
                    ID = 2,
                    Description = "Premium service",
                    CostumerPart = 100,
                    InsurancePart = 100,
                },
                new BillableItems{
                    ID = 3,
                    Description = "Super service",
                    CostumerPart = 100,
                    InsurancePart = 100,
                },

            };
            List<Costumer> costumers = new List<Costumer>
            {
                new Costumer
                {
                    ID = 1,
                    FirstName = "Test",
                    LastName = "User",
                    City = "AArhus",
                    CPRNumber = "1234567890",
                    StreetName = "Vej",
                    StreetNumber = "20",
                    Insured = false,
                    ZipCode = "8000"
                },
                new Costumer
                {
                    ID = 2,
                    FirstName = "Tes2t",
                    LastName = "Use2r",
                    City = "AArhus",
                    CPRNumber = "1234567891",
                    StreetName = "Vej",
                    StreetNumber = "25",
                    Insured = true,
                    ZipCode = "8000"
                },
            };
            List<Bill> bills = new List<Bill>
            {
                new Bill
                {
                    BillDate = DateTimeOffset.Now,
                    ID = 1,
                    CostumerID = costumers[0].ID
                },
                new Bill
                {
                    BillDate = DateTimeOffset.Now,
                    ID = 2,
                    CostumerID = costumers[0].ID
                },
                new Bill
                {
                    BillDate = DateTimeOffset.Now,
                    ID = 3,
                    CostumerID = costumers[1].ID
                }
            };
            List<BillToItem> billToItems = new List<BillToItem>
            {
                new BillToItem
                {
                    ID = 1,
                    ItemID = 1,
                    BillID = 1,
                    Counter = 2
                },
                new BillToItem
                {
                    ID = 2,
                    ItemID = 2,
                    BillID = 1,
                    Counter = 1
                },
                new BillToItem
                {
                    ID = 3,
                    ItemID = 2,
                    BillID = 2,
                    Counter = 1
                },
                new BillToItem
                {
                    ID = 4,
                    ItemID = 1,
                    BillID = 2,
                    Counter = 3
                },
                new BillToItem
                {
                    ID = 5,
                    ItemID = 3,
                    BillID = 3,
                    Counter = 1
                },
                new BillToItem
                {
                    ID = 6,
                    ItemID = 1,
                    BillID = 3,
                    Counter = 1
                },

            };
            for (var i = 0; i < billToItems.Count; i++)
            {
                bills[billToItems[i].BillID - 1].TotalCostumer += items[billToItems[i].ItemID - 1].CostumerPart * billToItems[i].Counter;
                if(costumers[bills[billToItems[i].BillID - 1].CostumerID - 1].Insured)
                {
                    bills[billToItems[i].BillID - 1].TotalInsureance += items[billToItems[i].ItemID - 1].InsurancePart * billToItems[i].Counter;
                }
                else
                {
                    bills[billToItems[i].BillID - 1].TotalCostumer += items[billToItems[i].ItemID - 1].InsurancePart * billToItems[i].Counter;
                }
                
            }
            builder.Entity<BillableItems>().HasData(items.ToArray());
            builder.Entity<Costumer>().HasData(costumers.ToArray());
            builder.Entity<Bill>().HasData(bills.ToArray());
            builder.Entity<BillToItem>().HasData(billToItems.ToArray());
        }

        public DbSet<DentistBilling.Models.Bill> Bill { get; set; }

        public DbSet<DentistBilling.Models.BillableItems> BillableItems { get; set; }

        public DbSet<DentistBilling.Models.Costumer> Costumer { get; set; }
    }
}
