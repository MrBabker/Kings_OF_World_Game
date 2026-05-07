using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace king.Migrations
{
    /// <inheritdoc />
    public partial class capitaledite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_players",
                table: "players");

            migrationBuilder.RenameTable(
                name: "players",
                newName: "Players");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "Players",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Players",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Players",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Players",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Players",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Players",
                table: "Players",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Players",
                table: "Players");

            migrationBuilder.RenameTable(
                name: "Players",
                newName: "players");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "players",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "players",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "players",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "players",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "players",
                newName: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_players",
                table: "players",
                column: "id");
        }
    }
}
