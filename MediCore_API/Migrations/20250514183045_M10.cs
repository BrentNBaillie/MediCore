using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediCore_API.Migrations
{
    /// <inheritdoc />
    public partial class M10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Patients_PatientId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_PatientId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Bills");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PatientId",
                table: "Bills",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_PatientId",
                table: "Bills",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Patients_PatientId",
                table: "Bills",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
