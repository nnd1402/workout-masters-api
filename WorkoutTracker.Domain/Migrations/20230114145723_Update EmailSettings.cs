using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutTracker.Domain.Migrations
{
    public partial class UpdateEmailSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "verifyAccountTemplate",
                table: "EmailSettings",
                newName: "VerifyAccountTemplate");

            migrationBuilder.RenameColumn(
                name: "forgotPasswordTemplate",
                table: "EmailSettings",
                newName: "ForgotPasswordTemplate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VerifyAccountTemplate",
                table: "EmailSettings",
                newName: "verifyAccountTemplate");

            migrationBuilder.RenameColumn(
                name: "ForgotPasswordTemplate",
                table: "EmailSettings",
                newName: "forgotPasswordTemplate");
        }
    }
}
