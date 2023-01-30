using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutMasters.Domain.Migrations
{
    public partial class AddedEmailSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailSettings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    verifyAccountTemplate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    forgotPasswordTemplate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSettings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailSettings");
        }
    }
}
