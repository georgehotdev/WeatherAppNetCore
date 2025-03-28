using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherAppNetCore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MadeForecastReferenceIdUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ForecastReferenceId",
                table: "WeatherForecasts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WeatherForecasts_ForecastReferenceId",
                table: "WeatherForecasts",
                column: "ForecastReferenceId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WeatherForecasts_ForecastReferenceId",
                table: "WeatherForecasts");

            migrationBuilder.AlterColumn<long>(
                name: "ForecastReferenceId",
                table: "WeatherForecasts",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
