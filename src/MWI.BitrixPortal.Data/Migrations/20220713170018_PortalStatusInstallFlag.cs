using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MWI.BitrixPortal.Data.Migrations
{
    public partial class PortalStatusInstallFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InstallStatus",
                table: "Portals",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstallStatus",
                table: "Portals");
        }
    }
}
