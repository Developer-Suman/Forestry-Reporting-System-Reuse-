using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reuse.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddFinancialStatementTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinancialStatements",
                columns: table => new
                {
                    FinancialStatementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreviousMonthIncome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentMonthIncome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalIncome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreviousMonthExpenses = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentMonthExpenses = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Totalexpenses = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RemainingCashAmount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RemainingBankAmount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalRemaining = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActualRemainingAmountInBank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DifferenceBetweenActualAmountAndRemainingAmount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReasaonForDifference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    MonthId = table.Column<int>(type: "int", nullable: false),
                    FiscalYearId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ApplicantName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationNepaliDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationEnglishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VerifierName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerifiedNepaliDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerifiedEnglishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NepaliDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnglishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApplicantDesignationId = table.Column<int>(type: "int", nullable: true),
                    VerifierDesignationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialStatements", x => x.FinancialStatementId);
                    table.ForeignKey(
                        name: "FK_FinancialStatements_FiscalYear_FiscalYearId",
                        column: x => x.FiscalYearId,
                        principalTable: "FiscalYear",
                        principalColumn: "FiscalYearId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinancialStatements_Months_MonthId",
                        column: x => x.MonthId,
                        principalTable: "Months",
                        principalColumn: "MonthId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinancialStatements_branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "branches",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialStatements_BranchId",
                table: "FinancialStatements",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialStatements_FiscalYearId",
                table: "FinancialStatements",
                column: "FiscalYearId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialStatements_MonthId",
                table: "FinancialStatements",
                column: "MonthId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialStatements");
        }
    }
}
