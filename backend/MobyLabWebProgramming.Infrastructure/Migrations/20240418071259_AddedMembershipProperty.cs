#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace MobyLabWebProgramming.Infrastructure.Migrations
{
    public partial class AddedMembershipProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeamMembership_UserId",
                table: "TeamMembership");

            migrationBuilder.AddColumn<Guid>(
                name: "MembershipId",
                table: "User",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TeamMembership",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "TeamMembership",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TeamMembership",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembership_UserId",
                table: "TeamMembership",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeamMembership_UserId",
                table: "TeamMembership");

            migrationBuilder.DropColumn(
                name: "MembershipId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TeamMembership");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TeamMembership");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TeamMembership");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembership_UserId",
                table: "TeamMembership",
                column: "UserId");
        }
    }
}
