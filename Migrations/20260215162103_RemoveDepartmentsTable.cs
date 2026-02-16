using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ParcelDelivery.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDepartmentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parcels_Departments_DepartmentId",
                table: "Parcels");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Parcels_DepartmentId",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Parcels");

            migrationBuilder.AddColumn<int>(
                name: "Department",
                table: "Parcels",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "Parcels");

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "Parcels",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Mail" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Regular" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Heavy" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_DepartmentId",
                table: "Parcels",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcels_Departments_DepartmentId",
                table: "Parcels",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
