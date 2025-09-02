using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ViixsDockerManager.DockLight.Environments.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialEnvironments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocklightEnvironment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ApiLocation = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocklightEnvironment", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DocklightEnvironment",
                columns: new[] { "Id", "ApiLocation", "Name" },
                values: new object[,]
                {
                    { 1, "/var/docker/docker.sock", "Local" },
                    { 2, "http://nas", "Nas" },
                    { 3, "http://nuc", "Nuc" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocklightEnvironment");
        }
    }
}
