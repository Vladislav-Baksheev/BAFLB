using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BAFLB.Migrations
{
    /// <inheritdoc />
    public partial class five : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "users",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "MaxShot",
                table: "users",
                newName: "maxShot");

            migrationBuilder.RenameColumn(
                name: "IsDead",
                table: "users",
                newName: "isDead");

            migrationBuilder.RenameColumn(
                name: "CurrentShot",
                table: "users",
                newName: "currentShot");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "round",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "round",
                newName: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "maxShot",
                table: "users",
                newName: "MaxShot");

            migrationBuilder.RenameColumn(
                name: "isDead",
                table: "users",
                newName: "IsDead");

            migrationBuilder.RenameColumn(
                name: "currentShot",
                table: "users",
                newName: "CurrentShot");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "round",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "round",
                newName: "Id");
        }
    }
}
