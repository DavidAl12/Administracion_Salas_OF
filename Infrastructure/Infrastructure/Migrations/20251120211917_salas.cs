using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class salas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salas_AspNetUsers_ResponsableId1",
                table: "Salas");

            migrationBuilder.DropIndex(
                name: "IX_Salas_ResponsableId1",
                table: "Salas");

            migrationBuilder.DropColumn(
                name: "ResponsableId",
                table: "Salas");

            migrationBuilder.DropColumn(
                name: "ResponsableId1",
                table: "Salas");

            migrationBuilder.AddColumn<string>(
                name: "Responsable",
                table: "Salas",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Responsable",
                table: "Salas");

            migrationBuilder.AddColumn<int>(
                name: "ResponsableId",
                table: "Salas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponsableId1",
                table: "Salas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Salas_ResponsableId1",
                table: "Salas",
                column: "ResponsableId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Salas_AspNetUsers_ResponsableId1",
                table: "Salas",
                column: "ResponsableId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
