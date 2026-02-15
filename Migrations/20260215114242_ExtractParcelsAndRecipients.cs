using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParcelDelivery.Api.Migrations
{
    /// <inheritdoc />
    public partial class ExtractParcelsAndRecipients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // create recipients and parcels tables first
            migrationBuilder.CreateTable(
                name: "Recipients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parcels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RecipientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Department = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parcels_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Parcels_Recipients_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Recipients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_OrderId",
                table: "Parcels",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_RecipientId",
                table: "Parcels",
                column: "RecipientId");

                        // migrate data from Orders.Parcels (JSON) into new Recipients and Parcels tables
                        migrationBuilder.Sql(@"
INSERT INTO Recipients (Id, Name, AddressJson, Phone)
SELECT DISTINCT
    TRY_CAST(JSON_VALUE(JSON_QUERY(p.value,'$.Recipient'),'$.Id') AS uniqueidentifier) AS Id,
    JSON_VALUE(JSON_QUERY(p.value,'$.Recipient'),'$.Name') AS Name,
    JSON_QUERY(JSON_QUERY(p.value,'$.Recipient'),'$.Address') AS AddressJson,
    JSON_VALUE(JSON_QUERY(p.value,'$.Recipient'),'$.Phone') AS Phone
FROM Orders o
CROSS APPLY OPENJSON(o.Parcels) AS p
WHERE JSON_VALUE(JSON_QUERY(p.value,'$.Recipient'),'$.Id') IS NOT NULL;

INSERT INTO Parcels (Id, Weight, Value, RecipientId, Department, Content, OrderId)
SELECT
    TRY_CAST(JSON_VALUE(p.value,'$.Id') AS uniqueidentifier),
    TRY_CAST(JSON_VALUE(p.value,'$.Weight') AS float),
    ISNULL(TRY_CAST(JSON_VALUE(p.value,'$.Value') AS decimal(18,2)), 0),
    TRY_CAST(JSON_VALUE(JSON_QUERY(p.value,'$.Recipient'),'$.Id') AS uniqueidentifier),
    ISNULL(TRY_CAST(JSON_VALUE(p.value,'$.Department') AS int), 1),
    ISNULL(TRY_CAST(JSON_VALUE(p.value,'$.Content') AS int), 0),
    o.Id
FROM Orders o
CROSS APPLY OPENJSON(o.Parcels) AS p
WHERE JSON_VALUE(p.value,'$.Id') IS NOT NULL;
");

                        // now drop the old JSON column
                        migrationBuilder.DropColumn(
                                name: "Parcels",
                                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parcels");

            migrationBuilder.DropTable(
                name: "Recipients");

            migrationBuilder.AddColumn<string>(
                name: "Parcels",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
