using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediCore_API.Migrations
{
    /// <inheritdoc />
    public partial class M4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffMembers_StaffRoles_StaffRoleId",
                table: "StaffMembers");

            migrationBuilder.RenameColumn(
                name: "StaffRoleId",
                table: "StaffMembers",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_StaffMembers_StaffRoleId",
                table: "StaffMembers",
                newName: "IX_StaffMembers_RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffMembers_StaffRoles_RoleId",
                table: "StaffMembers",
                column: "RoleId",
                principalTable: "StaffRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffMembers_StaffRoles_RoleId",
                table: "StaffMembers");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "StaffMembers",
                newName: "StaffRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_StaffMembers_RoleId",
                table: "StaffMembers",
                newName: "IX_StaffMembers_StaffRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffMembers_StaffRoles_StaffRoleId",
                table: "StaffMembers",
                column: "StaffRoleId",
                principalTable: "StaffRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
