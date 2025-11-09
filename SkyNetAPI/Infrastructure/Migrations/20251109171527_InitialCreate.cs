using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyNetAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Waybill",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WaybillNumber = table.Column<string>(type: "text", nullable: true),
                    ServiceType = table.Column<string>(type: "text", nullable: false),
                    SenderSuburb = table.Column<string>(type: "text", nullable: false),
                    SenderPostalCode = table.Column<string>(type: "text", nullable: false),
                    RecipientSuburb = table.Column<string>(type: "text", nullable: false),
                    RecipientPostalCode = table.Column<string>(type: "text", nullable: false),
                    CreateOnDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedOnDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Waybill", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parcel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ParcelNumber = table.Column<string>(type: "text", nullable: false),
                    Length = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    Breadth = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    Height = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    Mass = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    WaybillId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateOnDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedOnDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parcel_Waybill_WaybillId",
                        column: x => x.WaybillId,
                        principalTable: "Waybill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parcel_ParcelNumber",
                table: "Parcel",
                column: "ParcelNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Parcel_WaybillId",
                table: "Parcel",
                column: "WaybillId");

            migrationBuilder.CreateIndex(
                name: "IX_Waybill_WaybillNumber",
                table: "Waybill",
                column: "WaybillNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parcel");

            migrationBuilder.DropTable(
                name: "Waybill");
        }
    }
}
