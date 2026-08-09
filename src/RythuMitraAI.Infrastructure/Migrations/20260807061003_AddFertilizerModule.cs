using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RythuMitraAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFertilizerModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Weathers_Farmers_FarmerId1",
                table: "Weathers");

            migrationBuilder.DropIndex(
                name: "IX_Weathers_FarmerId1",
                table: "Weathers");

            migrationBuilder.DropColumn(
                name: "FarmerId1",
                table: "Weathers");

            migrationBuilder.CreateTable(
                name: "Fertilizers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FertilizerCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FertilizerName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FertilizerType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nitrogen = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Phosphorus = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Potassium = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    RecommendedCrop = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RecommendedSoil = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fertilizers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fertilizers_FertilizerCode",
                table: "Fertilizers",
                column: "FertilizerCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fertilizers_FertilizerName",
                table: "Fertilizers",
                column: "FertilizerName");

            migrationBuilder.CreateIndex(
                name: "IX_Fertilizers_FertilizerType",
                table: "Fertilizers",
                column: "FertilizerType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fertilizers");

            migrationBuilder.AddColumn<Guid>(
                name: "FarmerId1",
                table: "Weathers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Weathers_FarmerId1",
                table: "Weathers",
                column: "FarmerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Weathers_Farmers_FarmerId1",
                table: "Weathers",
                column: "FarmerId1",
                principalTable: "Farmers",
                principalColumn: "Id");
        }
    }
}
