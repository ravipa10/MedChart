using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MedChart.Migrations
{
    public partial class Initial_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "BloodPressures",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExamDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "GetUtcDate()"),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    SystolicReading = table.Column<int>(nullable: false),
                    DiastolicReading = table.Column<int>(nullable: false),
                    HeartRate = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodPressures", x => x.Id);
                });

            migrationBuilder.Sql(@"Insert into BloodPressures values(NEWID(), GETUTCDATE(), GETUTCDATE(), null, 120, 80, 80);");
            migrationBuilder.Sql(@"Insert into BloodPressures values(NEWID(), GETUTCDATE(), GETUTCDATE(), null, 121, 81, 80);");
            migrationBuilder.Sql(@"Insert into BloodPressures values(NEWID(), GETUTCDATE(), GETUTCDATE(), null, 122, 82, 82);");
            migrationBuilder.Sql(@"Insert into BloodPressures values(NEWID(), GETUTCDATE(), GETUTCDATE(), null, 123, 83, 84);");
            migrationBuilder.Sql(@"Insert into BloodPressures values(NEWID(), GETUTCDATE(), GETUTCDATE(), null, 124, 84, 86);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BloodPressures",
                schema: "dbo");
        }
    }
}
