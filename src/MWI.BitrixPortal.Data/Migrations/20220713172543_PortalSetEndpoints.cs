using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MWI.BitrixPortal.Data.Migrations
{
    public partial class PortalSetEndpoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientEndpoint",
                table: "Portals",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServerEndpoint",
                table: "Portals",
                type: "varchar(100)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientEndpoint",
                table: "Portals");

            migrationBuilder.DropColumn(
                name: "ServerEndpoint",
                table: "Portals");
        }
    }
}
