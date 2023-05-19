using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Housemate.Application.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class RenameCreationTaskName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AssignedAt",
                table: "HousingTasks",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "HousingTasks",
                newName: "AssignedAt");
        }
    }
}
