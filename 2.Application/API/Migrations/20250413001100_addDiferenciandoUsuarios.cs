using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class addDiferenciandoUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Usuario",
                table: "Users",
                newName: "UsuarioUser");

            migrationBuilder.RenameColumn(
                name: "Usuario",
                table: "Persons",
                newName: "UsuarioPerson");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsuarioUser",
                table: "Users",
                newName: "Usuario");

            migrationBuilder.RenameColumn(
                name: "UsuarioPerson",
                table: "Persons",
                newName: "Usuario");
        }
    }
}
