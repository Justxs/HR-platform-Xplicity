using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XplicityHRplatformBackEnd.Migrations
{
    public partial class CandidateCallDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CallDateCandidate");

            migrationBuilder.DropTable(
                name: "CandidateTechnology");

            migrationBuilder.CreateTable(
                name: "CandidateCalldates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CAndidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CallDateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateCalldates", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateCalldates");

            migrationBuilder.CreateTable(
                name: "CallDateCandidate",
                columns: table => new
                {
                    CallDatesid = table.Column<int>(type: "int", nullable: false),
                    CandidatesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallDateCandidate", x => new { x.CallDatesid, x.CandidatesId });
                    table.ForeignKey(
                        name: "FK_CallDateCandidate_Calldates_CallDatesid",
                        column: x => x.CallDatesid,
                        principalTable: "Calldates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CallDateCandidate_Candidates_CandidatesId",
                        column: x => x.CandidatesId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CandidateTechnology",
                columns: table => new
                {
                    CandidatesId = table.Column<int>(type: "int", nullable: false),
                    TechnologiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateTechnology", x => new { x.CandidatesId, x.TechnologiesId });
                    table.ForeignKey(
                        name: "FK_CandidateTechnology_Candidates_CandidatesId",
                        column: x => x.CandidatesId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateTechnology_Technologies_TechnologiesId",
                        column: x => x.TechnologiesId,
                        principalTable: "Technologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CallDateCandidate_CandidatesId",
                table: "CallDateCandidate",
                column: "CandidatesId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateTechnology_TechnologiesId",
                table: "CandidateTechnology",
                column: "TechnologiesId");
        }
    }
}
