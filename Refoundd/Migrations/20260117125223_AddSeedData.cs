using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Refoundd.Migrations
{
    public partial class AddSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Item_Id", "Date", "Description", "Item_Name", "Location", "Status", "User_Id" },
                values: new object[,]
                {
                    { 3, new DateTime(2023, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stainless steel bottle with a silver cap and a 'Go Bears' sticker.", "Blue HydroFlask", "Central Library, Level 2", "Found", 1 },
                    { 4, new DateTime(2023, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Left in a black sleeve. Has a small scratch on the front logo.", "Silver MacBook Air", "Science Building, Room 402", "Lost", 1 },
                    { 5, new DateTime(2023, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Toyota key fob with several house keys and a gym membership card.", "Keys with Red Lanyard", "Student Union, Food Court", "Found", 1 },
                    { 6, new DateTime(2023, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bi-fold wallet containing student ID and transit card. Reward offered.", "Black Leather Wallet", "Gym, Locker Rooms", "Lost", 1 },
                    { 7, new DateTime(2023, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tortoise shell frames found near the window seating area.", "Reading Glasses", "University Cafe", "Found", 1 },
                    { 8, new DateTime(2023, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Texas Instruments TI-84 Plus, name 'Alex' on the back.", "Scientific Calculator", "Lecture Hall A", "Lost", 1 },
                    { 9, new DateTime(2023, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dark blue foldable umbrella found in the umbrella stand.", "Umbrella", "Main Entrance Lobby", "Found", 1 },
                    { 10, new DateTime(2023, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Card for student #202495. Lost near the shuttle terminal.", "Student ID Card", "West Campus Bus Stop", "Lost", 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_Id", "Email", "First_Name", "Flag", "Last_Name", "Password", "User_Name" },
                values: new object[] { 3, "test@university.edu", "Test", 0, "User", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", "test_user" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Item_Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Item_Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Item_Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Item_Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Item_Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Item_Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Item_Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Item_Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_Id",
                keyValue: 3);
        }
    }
}
