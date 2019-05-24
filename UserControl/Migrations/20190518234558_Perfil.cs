using Microsoft.EntityFrameworkCore.Migrations;

namespace UserControl.Migrations
{
    public partial class Perfil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Perfil_PerfilId",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Perfil",
                table: "Perfil");

            migrationBuilder.RenameTable(
                name: "Perfil",
                newName: "Perfis");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Perfis",
                table: "Perfis",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Perfis_PerfilId",
                table: "Usuarios",
                column: "PerfilId",
                principalTable: "Perfis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Perfis_PerfilId",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Perfis",
                table: "Perfis");

            migrationBuilder.RenameTable(
                name: "Perfis",
                newName: "Perfil");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Perfil",
                table: "Perfil",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Perfil_PerfilId",
                table: "Usuarios",
                column: "PerfilId",
                principalTable: "Perfil",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
