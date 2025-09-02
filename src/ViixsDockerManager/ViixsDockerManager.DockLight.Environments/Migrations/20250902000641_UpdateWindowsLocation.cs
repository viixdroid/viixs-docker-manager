using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViixsDockerManager.DockLight.Environments.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWindowsLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DocklightEnvironment",
                keyColumn: "Id",
                keyValue: 2,
                column: "ApiLocation",
                value: "/pipe/docker_engine");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DocklightEnvironment",
                keyColumn: "Id",
                keyValue: 2,
                column: "ApiLocation",
                value: "\\pipe\\docker_engine");
        }
    }
}
