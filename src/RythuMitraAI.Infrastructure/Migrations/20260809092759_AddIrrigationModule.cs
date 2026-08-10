using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RythuMitraAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIrrigationModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Irrigations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IrrigationCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FarmerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CropId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IrrigationType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    WaterSource = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IrrigationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DurationInMinutes = table.Column<int>(type: "int", nullable: false),
                    WaterQuantity = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    WaterUnit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Irrigations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Irrigations_Crops_CropId",
                        column: x => x.CropId,
                        principalTable: "Crops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Irrigations_Farmers_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "Farmers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Irrigations_CropId",
                table: "Irrigations",
                column: "CropId");

            migrationBuilder.CreateIndex(
                name: "IX_Irrigations_FarmerId",
                table: "Irrigations",
                column: "FarmerId");

            migrationBuilder.CreateIndex(
                name: "IX_Irrigations_IrrigationCode",
                table: "Irrigations",
                column: "IrrigationCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Irrigations_IrrigationDate",
                table: "Irrigations",
                column: "IrrigationDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Irrigations");
        }
    }
}
