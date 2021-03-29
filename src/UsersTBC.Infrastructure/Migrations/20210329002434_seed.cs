using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UsersTBC.Infrastructure.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CreateDate", "IsActive", "Name", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 3, 29, 4, 24, 33, 681, DateTimeKind.Local).AddTicks(1566), "true", "Tbilisi", new DateTime(2021, 3, 29, 4, 24, 33, 681, DateTimeKind.Local).AddTicks(1583) },
                    { 3, new DateTime(2021, 3, 29, 4, 24, 33, 681, DateTimeKind.Local).AddTicks(1630), "true", "Khashuri", new DateTime(2021, 3, 29, 4, 24, 33, 681, DateTimeKind.Local).AddTicks(1632) }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Code", "CreateDate", "IsActive", "Name", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "en-US", new DateTime(2021, 3, 29, 4, 24, 33, 678, DateTimeKind.Local).AddTicks(4170), null, "English", new DateTime(2021, 3, 29, 4, 24, 33, 679, DateTimeKind.Local).AddTicks(1605) },
                    { 2, "ka-GE", new DateTime(2021, 3, 29, 4, 24, 33, 679, DateTimeKind.Local).AddTicks(2378), null, "Georgia", new DateTime(2021, 3, 29, 4, 24, 33, 679, DateTimeKind.Local).AddTicks(2385) }
                });

            migrationBuilder.InsertData(
                table: "City_Translations",
                columns: new[] { "Id", "CityId", "CreateDate", "LanguageId", "NameTranslate", "UpdateDate" },
                values: new object[] { 1, 1, new DateTime(2021, 3, 29, 4, 24, 33, 681, DateTimeKind.Local).AddTicks(4605), 2, "თბილისი", new DateTime(2021, 3, 29, 4, 24, 33, 681, DateTimeKind.Local).AddTicks(4614) });

            migrationBuilder.InsertData(
                table: "City_Translations",
                columns: new[] { "Id", "CityId", "CreateDate", "LanguageId", "NameTranslate", "UpdateDate" },
                values: new object[] { 2, 3, new DateTime(2021, 3, 29, 4, 24, 33, 681, DateTimeKind.Local).AddTicks(4669), 2, "ხაშური", new DateTime(2021, 3, 29, 4, 24, 33, 681, DateTimeKind.Local).AddTicks(4670) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "City_Translations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "City_Translations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
