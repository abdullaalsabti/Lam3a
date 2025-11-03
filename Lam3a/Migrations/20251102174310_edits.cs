using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lam3a.Migrations
{
    /// <inheritdoc />
    public partial class edits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Clients_ClientUserId",
                table: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_ClientUserId",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "ClientUserId",
                table: "Vehicle");

            migrationBuilder.AddColumn<int>(
                name: "CarType",
                table: "Vehicle",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                table: "Vehicle",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_ClientId",
                table: "Vehicle",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Clients_ClientId",
                table: "Vehicle",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Clients_ClientId",
                table: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_ClientId",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "CarType",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Vehicle");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientUserId",
                table: "Vehicle",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_ClientUserId",
                table: "Vehicle",
                column: "ClientUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Clients_ClientUserId",
                table: "Vehicle",
                column: "ClientUserId",
                principalTable: "Clients",
                principalColumn: "UserId");
        }
    }
}
