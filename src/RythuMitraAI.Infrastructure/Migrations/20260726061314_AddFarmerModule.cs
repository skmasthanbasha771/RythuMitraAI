using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RythuMitraAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFarmerModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Farmers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FarmerCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Village = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Mandal = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    District = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    State = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Pincode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LandArea = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    LandUnit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProfileImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farmers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Farmers_District",
                table: "Farmers",
                column: "District");

            migrationBuilder.CreateIndex(
                name: "IX_Farmers_FarmerCode",
                table: "Farmers",
                column: "FarmerCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Farmers_PhoneNumber",
                table: "Farmers",
                column: "PhoneNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Farmers");
        }
    }
}
