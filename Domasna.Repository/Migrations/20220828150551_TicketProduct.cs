using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domasna.Repository.Migrations
{
    public partial class TicketProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TicketProductName = table.Column<string>(nullable: false),
                    TicketProductImage = table.Column<string>(nullable: false),
                    TicketProductDescription = table.Column<string>(nullable: false),
                    TicketProductsMacros = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketProducts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketProducts");
        }
    }
}
