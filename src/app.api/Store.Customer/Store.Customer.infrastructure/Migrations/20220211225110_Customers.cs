using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Customers.Infrastructure.Migrations
{
    public partial class Customers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CPF = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Street = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Number = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Complement = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Neighborhood = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ZipCode = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_CustomerId",
                table: "Address",
                column: "CustomerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
