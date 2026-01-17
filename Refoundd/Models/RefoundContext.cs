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
                },

                new User
                {
                    User_Id = 3,
                    User_Name = "test_user",
                    First_Name = "Test",
                    Last_Name = "User",
                    Email = "test@university.edu",
                    Password = "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", // 1234
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
                },

                new Item
                {
                    Item_Id = 3,
                    Item_Name = "Blue HydroFlask",
                    Description = "Stainless steel bottle with a silver cap and a 'Go Bears' sticker.",
                    Status = "Found",
                    Location = "Central Library, Level 2",
                    Date = new DateTime(2023, 10, 25),
                    User_Id = 1
                },
                new Item
                {
                    Item_Id = 4,
                    Item_Name = "Silver MacBook Air",
                    Description = "Left in a black sleeve. Has a small scratch on the front logo.",
                    Status = "Lost",
                    Location = "Science Building, Room 402",
                    Date = new DateTime(2023, 10, 24),
                    User_Id = 1
                },
                new Item
                {
                    Item_Id = 5,
                    Item_Name = "Keys with Red Lanyard",
                    Description = "Toyota key fob with several house keys and a gym membership card.",
                    Status = "Found",
                    Location = "Student Union, Food Court",
                    Date = new DateTime(2023, 10, 24),
                    User_Id = 1
                },
                new Item
                {
                    Item_Id = 6,
                    Item_Name = "Black Leather Wallet",
                    Description = "Bi-fold wallet containing student ID and transit card. Reward offered.",
                    Status = "Lost",
                    Location = "Gym, Locker Rooms",
                    Date = new DateTime(2023, 10, 23),
                    User_Id = 1
                },
                new Item
                {
                    Item_Id = 7,
                    Item_Name = "Reading Glasses",
                    Description = "Tortoise shell frames found near the window seating area.",
                    Status = "Found",
                    Location = "University Cafe",
                    Date = new DateTime(2023, 10, 22),
                    User_Id = 1
                },
                new Item
                {
                    Item_Id = 8,
                    Item_Name = "Scientific Calculator",
                    Description = "Texas Instruments TI-84 Plus, name 'Alex' on the back.",
                    Status = "Lost",
                    Location = "Lecture Hall A",
                    Date = new DateTime(2023, 10, 22),
                    User_Id = 1
                },
                new Item
                {
                    Item_Id = 9,
                    Item_Name = "Umbrella",
                    Description = "Dark blue foldable umbrella found in the umbrella stand.",
                    Status = "Found",
                    Location = "Main Entrance Lobby",
                    Date = new DateTime(2023, 10, 21),
                    User_Id = 1
                },
                new Item
                {
                    Item_Id = 10,
                    Item_Name = "Student ID Card",
                    Description = "Card for student #202495. Lost near the shuttle terminal.",
                    Status = "Lost",
                    Location = "West Campus Bus Stop",
                    Date = new DateTime(2023, 10, 21),
                    User_Id = 1
                }
            );
        }

    }
}

