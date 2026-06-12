using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcmeOrder.Migrations.Sqlite
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
                type: "TEXT",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 5, 9, 9, 17, 40, 879, DateTimeKind.Utc).AddTicks(7650));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "date",
                table: "order",
                type: "TEXT",
                nullable: true,
                defaultValue: new DateTime(2022, 5, 9, 9, 17, 40, 879, DateTimeKind.Utc).AddTicks(7650),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");
        }
    }
}
