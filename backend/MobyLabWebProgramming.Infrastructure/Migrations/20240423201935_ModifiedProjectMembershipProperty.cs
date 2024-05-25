#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace MobyLabWebProgramming.Infrastructure.Migrations
{
    public partial class ModifiedProjectMembershipProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Question_QuestionId",
                table: "Answer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectMembership",
                table: "ProjectMembership");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ProjectMembership",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ProjectMembership",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ProjectMembership",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectMembership",
                table: "ProjectMembership",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMembership_ProjectId",
                table: "ProjectMembership",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Question_QuestionId",
                table: "Answer",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Question_QuestionId",
                table: "Answer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectMembership",
                table: "ProjectMembership");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMembership_ProjectId",
                table: "ProjectMembership");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProjectMembership");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ProjectMembership");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ProjectMembership");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectMembership",
                table: "ProjectMembership",
                columns: new[] { "ProjectId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Question_QuestionId",
                table: "Answer",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
