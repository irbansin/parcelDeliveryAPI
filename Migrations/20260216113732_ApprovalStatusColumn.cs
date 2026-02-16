using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParcelDelivery.Api.Migrations
{
    /// <inheritdoc />
    public partial class ApprovalStatusColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Parcels");

            migrationBuilder.AddColumn<int>(
                name: "ApprovalStatus",
                table: "Parcels",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalStatus",
                table: "Parcels");

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Parcels",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
