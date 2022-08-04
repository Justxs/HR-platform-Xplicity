using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XplicityHRplatformBackEnd.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CallDateCandidate_calldates_CallDatesid",
                table: "CallDateCandidate");
            
            migrationBuilder.DropPrimaryKey(
                name: "PK_calldates",
                table: "calldates");
            
            migrationBuilder.RenameTable(
                name: "calldates",
                newName: "Calldates");
            
            migrationBuilder.AddPrimaryKey(
                name: "PK_Calldates",
                table: "Calldates",
                column: "id");
            
            migrationBuilder.AddForeignKey(
                name: "FK_CallDateCandidate_Calldates_CallDatesid",
                table: "CallDateCandidate",
                column: "CallDatesid",
                principalTable: "Calldates",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CallDateCandidate_Calldates_CallDatesid",
                table: "CallDateCandidate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Calldates",
                table: "Calldates");

            migrationBuilder.RenameTable(
                name: "Calldates",
                newName: "calldates");

            migrationBuilder.AddPrimaryKey(
                name: "PK_calldates",
                table: "calldates",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CallDateCandidate_calldates_CallDatesid",
                table: "CallDateCandidate",
                column: "CallDatesid",
                principalTable: "calldates",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
