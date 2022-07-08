using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MWI.BitrixPortal.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Portals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MemberId = table.Column<string>(type: "varchar(40)", nullable: false),
                    Domain = table.Column<string>(type: "varchar(100)", nullable: false),
                    Language = table.Column<string>(type: "varchar(4)", nullable: false),
                    ApplicationToken = table.Column<string>(type: "varchar(40)", nullable: false),
                    BitrixAccountStatus = table.Column<string>(type: "char", nullable: false),
                    AdminUserName = table.Column<string>(type: "varchar(100)", nullable: false),
                    Email = table.Column<string>(type: "varchar(254)", nullable: true),
                    RefreshToken = table.Column<string>(type: "varchar(40)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    WizardMode = table.Column<bool>(type: "bit", nullable: false),
                    PortalStatus = table.Column<string>(type: "char", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portals", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Portals");
        }
    }
}
