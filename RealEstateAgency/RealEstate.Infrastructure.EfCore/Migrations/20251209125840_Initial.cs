using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RealEstate.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "counterparty",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    full_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    passport_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    phone = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_counterparty", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "real_estate_object",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<int>(type: "integer", nullable: false),
                    purpose = table.Column<int>(type: "integer", nullable: false),
                    cadastral_number = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    address = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    floors_total = table.Column<int>(type: "integer", nullable: false),
                    total_area = table.Column<double>(type: "double precision", precision: 12, scale: 2, nullable: false),
                    rooms = table.Column<int>(type: "integer", nullable: false),
                    ceiling_height = table.Column<double>(type: "double precision", precision: 4, scale: 2, nullable: true),
                    floor = table.Column<int>(type: "integer", nullable: true),
                    has_encumbrances = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_real_estate_object", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "real_estate_request",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    client_id = table.Column<int>(type: "integer", nullable: false),
                    property_id = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    created_at = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_real_estate_request", x => x.id);
                    table.ForeignKey(
                        name: "fk_real_estate_request_client",
                        column: x => x.client_id,
                        principalTable: "counterparty",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_real_estate_request_property",
                        column: x => x.property_id,
                        principalTable: "real_estate_object",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "counterparty",
                columns: new[] { "id", "full_name", "passport_number", "phone" },
                values: new object[,]
                {
                    { 1, "Иванов Иван", "4010 111111", "+7-900-000-01-01" },
                    { 2, "Петров Пётр", "4010 222222", "+7-900-000-02-02" },
                    { 3, "Сидоров Степан", "4010 333333", "+7-900-000-03-03" },
                    { 4, "Антонова Анна", "4010 444444", "+7-900-000-04-04" },
                    { 5, "Кузнецов Кирилл", "4010 555555", "+7-900-000-05-05" },
                    { 6, "Соколова Света", "4010 666666", "+7-900-000-06-06" },
                    { 7, "Романов Роман", "4010 777777", "+7-900-000-07-07" },
                    { 8, "Фёдорова Фаина", "4010 888888", "+7-900-000-08-08" },
                    { 9, "Морозов Максим", "4010 999999", "+7-900-000-09-09" },
                    { 10, "Ким Денис", "4010 101010", "+7-900-000-10-10" },
                    { 11, "Осипова Олеся", "4010 111112", "+7-900-000-11-11" },
                    { 12, "Громов Григорий", "4010 121212", "+7-900-000-12-12" }
                });

            migrationBuilder.InsertData(
                table: "real_estate_object",
                columns: new[] { "id", "address", "cadastral_number", "ceiling_height", "floor", "floors_total", "has_encumbrances", "purpose", "rooms", "total_area", "type" },
                values: new object[,]
                {
                    { 1, "Москва, ул. Первая, 1", "77:01:0001001:1", 2.7000000000000002, 7, 17, false, 0, 2, 52.299999999999997, 0 },
                    { 2, "МО, Мытищи, ул. Лесная, 3", "50:01:0002002:2", 3.0, null, 2, false, 0, 5, 180.0, 1 },
                    { 3, "СПб, Невский 10", "78:01:0003003:3", 3.2000000000000002, 3, 8, true, 1, 4, 95.0, 3 },
                    { 4, "Краснодарский край, уч. 45", "23:01:0004004:4", 0.0, null, 0, false, 0, 0, 1000.0, 6 },
                    { 5, "Екатеринбург, Промзона 12", "66:01:0005005:5", 6.0, 1, 1, true, 1, 1, 450.0, 5 },
                    { 6, "Москва, ул. Вторая, 5", "77:01:0006006:6", 2.6000000000000001, 16, 25, false, 0, 1, 40.0, 0 },
                    { 7, "Москва, ул. Третья, 7", "77:01:0007007:7", 2.6000000000000001, 12, 25, false, 0, 1, 36.0, 0 },
                    { 8, "МО, Балашиха, Заречная 8", "50:01:0008008:8", 2.8999999999999999, null, 2, false, 0, 4, 140.0, 1 },
                    { 9, "СПб, Сенная, 2", "78:01:0009009:9", 3.2999999999999998, 6, 12, false, 1, 5, 120.0, 3 },
                    { 10, "Москва, ул. Новая, 9", "77:01:0010010:0", 2.7999999999999998, 9, 22, false, 0, 3, 58.0, 0 },
                    { 11, "Краснодарский край, уч. 17", "23:01:0011011:1", 0.0, null, 0, false, 0, 0, 800.0, 6 },
                    { 12, "Екатеринбург, ТЦ «Океан»", "66:01:0012012:2", 3.3999999999999999, 1, 3, false, 1, 2, 75.0, 4 }
                });

            migrationBuilder.InsertData(
                table: "real_estate_request",
                columns: new[] { "id", "amount", "client_id", "created_at", "property_id", "type" },
                values: new object[,]
                {
                    { 1, 5000000m, 1, new DateOnly(2024, 6, 15), 1, 1 },
                    { 2, 10000000m, 2, new DateOnly(2024, 7, 20), 2, 1 },
                    { 3, 15000000m, 3, new DateOnly(2024, 5, 10), 3, 1 },
                    { 4, 2000000m, 4, new DateOnly(2024, 6, 5), 4, 1 },
                    { 5, 3000000m, 1, new DateOnly(2023, 1, 1), 5, 1 },
                    { 6, 3500000m, 5, new DateOnly(2024, 6, 18), 6, 0 },
                    { 7, 1000000m, 6, new DateOnly(2024, 6, 19), 7, 0 },
                    { 8, 5000000m, 7, new DateOnly(2024, 7, 1), 8, 0 },
                    { 9, 2000000m, 6, new DateOnly(2024, 7, 2), 9, 0 },
                    { 10, 1000000m, 8, new DateOnly(2024, 7, 3), 10, 0 },
                    { 11, 1200000m, 8, new DateOnly(2024, 7, 4), 11, 0 },
                    { 12, 2200000m, 9, new DateOnly(2024, 7, 5), 12, 0 },
                    { 13, 4100000m, 5, new DateOnly(2024, 8, 1), 1, 0 },
                    { 14, 4200000m, 5, new DateOnly(2024, 8, 2), 2, 0 },
                    { 15, 7000000m, 3, new DateOnly(2024, 7, 15), 6, 1 },
                    { 16, 12000000m, 2, new DateOnly(2024, 8, 10), 7, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "ix_counterparty_passport_number",
                table: "counterparty",
                column: "passport_number");

            migrationBuilder.CreateIndex(
                name: "ix_real_estate_request_client_id",
                table: "real_estate_request",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_real_estate_request_property_id",
                table: "real_estate_request",
                column: "property_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "real_estate_request");

            migrationBuilder.DropTable(
                name: "counterparty");

            migrationBuilder.DropTable(
                name: "real_estate_object");
        }
    }
}
