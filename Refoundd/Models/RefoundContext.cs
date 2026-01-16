// RefoundContext.cs
using Microsoft.EntityFrameworkCore;
using Refoundd.Models;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Refoundd.Models
{
    public class RefoundContext : DbContext
    {
        public RefoundContext(DbContextOptions<RefoundContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Item> Items { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed Data for Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    User_Id = 1,
                    User_Name = "Ali",
                    First_Name = "Ali",
                    Last_Name = "Ahmed",
                    Email = "ali@example.com",
                    Password = "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4",
                    // pass: 1234
                    Flag = 0
                },
                new User
                {
                    User_Id = 2,
                    User_Name = "Sara",
                    First_Name = "Sara",
                    Last_Name = "Khan",
                    Email = "sara@example.com",
                    Password = "d404559f602eab6fd602ac7680dacbfaadd13630335e951f097af3900e9de176",
                    // pass: 5678
                    Flag = 0
                }
            );

            // Seed Data for Items
            modelBuilder.Entity<Item>().HasData(
                new Item
                {
                    Item_Id = 1,
                    Item_Name = "Black Wallet",
                    Description = "Leather wallet with cards",
                    Status = "Lost",
                    Location = "University",
                    Date = new DateTime(2025, 1, 1),
                    User_Id = 1
                },
                new Item
                {
                    Item_Id = 2,
                    Item_Name = "Samsung Phone",
                    Description = "Black Samsung mobile",
                    Status = "Found",
                    Location = "Library",
                    Date = new DateTime(2025, 1, 3),
                    User_Id = 2
                }
            );
        }
    }
}

