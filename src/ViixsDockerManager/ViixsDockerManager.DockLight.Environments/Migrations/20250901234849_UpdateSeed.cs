using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViixsDockerManager.DockLight.Environments.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DocklightEnvironment",
                keyColumn: "Id",
                keyValue: 1,
                column: "ApiLocation",
                value: "/var/run/docker.sock");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DocklightEnvironment",
                keyColumn: "Id",
                keyValue: 1,
                column: "ApiLocation",
                value: "/var/docker/docker.sock");
        }
    }
}
