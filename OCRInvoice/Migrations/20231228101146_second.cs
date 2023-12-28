using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OCRInvoice.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseContents",
                table: "CourseContents");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "InvoiceMasters");

            migrationBuilder.RenameTable(
                name: "CourseContents",
                newName: "LineItemMasters");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceMasters",
                table: "InvoiceMasters",
                column: "InvoiceID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LineItemMasters",
                table: "LineItemMasters",
                column: "LineItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LineItemMasters",
                table: "LineItemMasters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceMasters",
                table: "InvoiceMasters");

            migrationBuilder.RenameTable(
                name: "LineItemMasters",
                newName: "CourseContents");

            migrationBuilder.RenameTable(
                name: "InvoiceMasters",
                newName: "Courses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseContents",
                table: "CourseContents",
                column: "LineItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "InvoiceID");
        }
    }
}
