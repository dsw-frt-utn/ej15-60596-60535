using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dsw2026Ej15.Data.Migrations
{
    public partial class SeedSpecialities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Specialities",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("8a1f3b78-3f66-4d68-8d6e-1c5b9c7a2f41"), "Especialidad médica infantil", "Pediatría" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Specialities",
                keyColumn: "Id",
                keyValue: new Guid("8a1f3b78-3f66-4d68-8d6e-1c5b9c7a2f41"));
        }
    }
}
