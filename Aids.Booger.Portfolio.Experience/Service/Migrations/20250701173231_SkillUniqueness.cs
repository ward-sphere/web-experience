using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.Migrations
{
    /// <inheritdoc />
    public partial class SkillUniqueness : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkSkill_WorkId",
                table: "WorkSkill");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSkill_WorkId_SkillId",
                table: "WorkSkill",
                columns: new[] { "WorkId", "SkillId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skill_Name",
                table: "Skill",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkSkill_WorkId_SkillId",
                table: "WorkSkill");

            migrationBuilder.DropIndex(
                name: "IX_Skill_Name",
                table: "Skill");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSkill_WorkId",
                table: "WorkSkill",
                column: "WorkId");
        }
    }
}
