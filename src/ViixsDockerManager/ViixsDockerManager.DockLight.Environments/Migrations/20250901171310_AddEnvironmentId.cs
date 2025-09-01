using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ViixsDockerManager.DockLight.Environments.Migrations
{
    /// <inheritdoc />
    public partial class AddEnvironmentId : Migration
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
                    EnvironmentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ApiLocation = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocklightEnvironment", x => x.Id);
                    table.UniqueConstraint("AK_DocklightEnvironment_EnvironmentId", x => x.EnvironmentId);
                });

            migrationBuilder.InsertData(
                table: "DocklightEnvironment",
                columns: new[] { "Id", "ApiLocation", "EnvironmentId", "Name" },
                values: new object[,]
                {
                    { 1, "/var/docker/docker.sock", new Guid("34805c86-9086-45e1-b264-241983acc044"), "Local" },
                    { 2, "http://nas", new Guid("1acf8e3c-495a-4258-a280-cc7acf504c0e"), "Nas" },
                    { 3, "http://nuc", new Guid("4d9e6256-d601-41f4-abe6-a42f40100008"), "Nuc" }
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
