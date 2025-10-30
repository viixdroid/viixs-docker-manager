using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViixsDockerManager.Setup.Migrations
{
    /// <inheritdoc />
    public partial class InitialSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SetupState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SetupId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LastCompletedStep = table.Column<string>(type: "TEXT", nullable: true),
                    CurrentStep = table.Column<string>(type: "TEXT", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetupState", x => x.Id);
                    table.UniqueConstraint("AK_SetupState_SetupId", x => x.SetupId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SetupState");
        }
    }
}
