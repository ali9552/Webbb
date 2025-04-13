using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace website.dal.Migrations
{
    /// <inheritdoc />
    public partial class t2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "departmentid",
                table: "employee",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_employee_departmentid",
                table: "employee",
                column: "departmentid");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_department_departmentid",
                table: "employee",
                column: "departmentid",
                principalTable: "department",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_department_departmentid",
                table: "employee");

            migrationBuilder.DropIndex(
                name: "IX_employee_departmentid",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "departmentid",
                table: "employee");
        }
    }
}
