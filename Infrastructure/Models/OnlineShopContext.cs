using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class OnlineShopContext : DbContext
    {
        private string GetConnectionString()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            return config.GetConnectionString("DefaultConnection") ?? throw new ArgumentException("Cannot extract connection string form JSON file");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(GetConnectionString());
            optionsBuilder.UseInMemoryDatabase(databaseName: "OnlineShop");
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    Database.EnsureCreated();

        //    modelBuilder.Entity<Product>().HasData(
        //            new Product { Id = Guid.Parse("e644b674-203b-4136-a7dc-ce4e7454faf8"), Description = "Test description", Name = "TestName", Price = 1000f, ProductStatus = ProductStatus.AVAILABLE, Quantity = 100 },
        //            new Product { Id = Guid.Parse("e8f4440c-0464-4093-b878-886ba040d714"), Description = "Test description2", Name = "TestName2", Price = 1000f, ProductStatus = ProductStatus.AVAILABLE, Quantity = 100 },
        //            new Product { Id = Guid.Parse("7ae624e4-9c3a-4e00-8e15-d08be7ecccfe"), Description = "Test description3", Name = "TestName3", Price = 1000f, ProductStatus = ProductStatus.AVAILABLE, Quantity = 100 },
        //            new Product { Id = Guid.Parse("39c8670b-0345-48c1-9e74-9617b8726c7d"), Description = "Test description4", Name = "TestName4", Price = 1000f, ProductStatus = ProductStatus.UNAVAILABLE, Quantity = 100 },
        //            new Product { Id = Guid.Parse("accafcda-373f-4d72-816d-2b6e8d1a2920"), Description = "Test description5", Name = "TestName5", Price = 1000f, ProductStatus = ProductStatus.OUT_OF_STOCK, Quantity = 100 }
        //        );
        //    SaveChanges();
        //}

        public virtual DbSet<Product>? Products { get; }
    }
}
