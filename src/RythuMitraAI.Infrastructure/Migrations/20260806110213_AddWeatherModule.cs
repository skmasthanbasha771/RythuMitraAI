using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RythuMitraAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWeatherModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Weathers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WeatherCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FarmerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WeatherDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Temperature = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Humidity = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Rainfall = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    WindSpeed = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    WeatherCondition = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FarmerId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weathers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weathers_Farmers_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "Farmers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Weathers_Farmers_FarmerId1",
                        column: x => x.FarmerId1,
                        principalTable: "Farmers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Weathers_FarmerId",
                table: "Weathers",
                column: "FarmerId");

            migrationBuilder.CreateIndex(
                name: "IX_Weathers_FarmerId1",
                table: "Weathers",
                column: "FarmerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Weathers_WeatherCode",
                table: "Weathers",
                column: "WeatherCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Weathers_WeatherDate",
                table: "Weathers",
                column: "WeatherDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weathers");
        }
    }
}
