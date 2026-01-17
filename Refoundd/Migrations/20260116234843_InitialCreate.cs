using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Refoundd.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    First_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Last_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Flag = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Item_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    User_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Item_Id);
                    table.ForeignKey(
                        name: "FK_Items_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_Id", "Email", "First_Name", "Flag", "Last_Name", "Password", "User_Name" },
                values: new object[] { 1, "ali@example.com", "Ali", 0, "Ahmed", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", "Ali" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_Id", "Email", "First_Name", "Flag", "Last_Name", "Password", "User_Name" },
                values: new object[] { 2, "sara@example.com", "Sara", 0, "Khan", "d404559f602eab6fd602ac7680dacbfaadd13630335e951f097af3900e9de176", "Sara" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Item_Id", "Date", "Description", "Item_Name", "Location", "Status", "User_Id" },
                values: new object[] { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Leather wallet with cards", "Black Wallet", "University", "Lost", 1 });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Item_Id", "Date", "Description", "Item_Name", "Location", "Status", "User_Id" },
                values: new object[] { 2, new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Black Samsung mobile", "Samsung Phone", "Library", "Found", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Items_User_Id",
                table: "Items",
                column: "User_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
