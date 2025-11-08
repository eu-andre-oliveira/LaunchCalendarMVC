using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaunchCalendar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDescricaoToEpisodio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Episodios",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Episodios");
        }
    }
}
