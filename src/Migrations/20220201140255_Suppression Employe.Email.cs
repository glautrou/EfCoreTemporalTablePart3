using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfCoreTemporalTablePart3.Migrations
{
    public partial class SuppressionEmployeEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Employe")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "EmployeHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Employe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
