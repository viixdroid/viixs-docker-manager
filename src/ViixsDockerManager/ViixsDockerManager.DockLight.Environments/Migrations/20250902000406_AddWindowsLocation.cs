using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ViixsDockerManager.DockLight.Environments.Migrations
{
    /// <inheritdoc />
    public partial class AddWindowsLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DocklightEnvironment",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DocklightEnvironment",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "DocklightEnvironment",
                columns: new[] { "Id", "ApiLocation", "EnvironmentId", "Name" },
                values: new object[,]
                {
                    { 2, "\\pipe\\docker_engine", new Guid("65670bc8-fb6e-413a-9492-3ba24fd4f8ff"), "LocalWindows" },
                    { 3, "http://nas", new Guid("1acf8e3c-495a-4258-a280-cc7acf504c0e"), "Nas" },
                    { 4, "http://nuc", new Guid("4d9e6256-d601-41f4-abe6-a42f40100008"), "Nuc" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DocklightEnvironment",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DocklightEnvironment",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DocklightEnvironment",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.InsertData(
                table: "DocklightEnvironment",
                columns: new[] { "Id", "ApiLocation", "EnvironmentId", "Name" },
                values: new object[,]
                {
                    { 2, "http://nas", new Guid("1acf8e3c-495a-4258-a280-cc7acf504c0e"), "Nas" },
                    { 3, "http://nuc", new Guid("4d9e6256-d601-41f4-abe6-a42f40100008"), "Nuc" }
                });
        }
    }
}
