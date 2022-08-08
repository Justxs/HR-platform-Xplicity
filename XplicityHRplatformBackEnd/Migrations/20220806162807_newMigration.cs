using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XplicityHRplatformBackEnd.Migrations
{
    public partial class newMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CAndidateId",
                table: "CandidateCalldates",
                newName: "CandidateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CandidateId",
                table: "CandidateCalldates",
                newName: "CAndidateId");
        }
    }
}
