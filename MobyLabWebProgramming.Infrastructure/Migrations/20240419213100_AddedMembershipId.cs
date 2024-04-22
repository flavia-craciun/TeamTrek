using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobyLabWebProgramming.Infrastructure.Migrations
{
    public partial class AddedMembershipId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamMembership",
                table: "TeamMembership");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamMembership",
                table: "TeamMembership",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembership_TeamId",
                table: "TeamMembership",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamMembership",
                table: "TeamMembership");

            migrationBuilder.DropIndex(
                name: "IX_TeamMembership_TeamId",
                table: "TeamMembership");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamMembership",
                table: "TeamMembership",
                columns: new[] { "TeamId", "UserId" });
        }
    }
}
