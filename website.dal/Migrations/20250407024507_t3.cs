using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace website.dal.Migrations
{
    /// <inheritdoc />
    public partial class t3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_department_departmentid",
                table: "employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_employee",
                table: "employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_department",
                table: "department");

            migrationBuilder.RenameTable(
                name: "employee",
                newName: "employees");

            migrationBuilder.RenameTable(
                name: "department",
                newName: "departments");

            migrationBuilder.RenameIndex(
                name: "IX_employee_departmentid",
                table: "employees",
                newName: "IX_employees_departmentid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_employees",
                table: "employees",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_departments",
                table: "departments",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_employees_departments_departmentid",
                table: "employees",
                column: "departmentid",
                principalTable: "departments",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_departments_departmentid",
                table: "employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_employees",
                table: "employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_departments",
                table: "departments");

            migrationBuilder.RenameTable(
                name: "employees",
                newName: "employee");

            migrationBuilder.RenameTable(
                name: "departments",
                newName: "department");

            migrationBuilder.RenameIndex(
                name: "IX_employees_departmentid",
                table: "employee",
                newName: "IX_employee_departmentid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_employee",
                table: "employee",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_department",
                table: "department",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_department_departmentid",
                table: "employee",
                column: "departmentid",
                principalTable: "department",
                principalColumn: "id");
        }
    }
}
