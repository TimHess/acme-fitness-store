using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcmeOrder.Migrations.Postgres
{
    /// <inheritdoc />
    public partial class UpdateDateDefaultToSql : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "date",
                table: "order",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 5, 9, 8, 55, 19, 197, DateTimeKind.Unspecified).AddTicks(5020));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "date",
                table: "order",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2022, 5, 9, 8, 55, 19, 197, DateTimeKind.Unspecified).AddTicks(5020),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");
        }
    }
}
