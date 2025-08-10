using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vidya.Migrations
{
    /// <inheritdoc />
    public partial class UpdateToMenuList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuItem",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    MenuName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MenuDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MenuUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MenuUrlPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MenuOrder = table.Column<int>(type: "int", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthAdd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthLstEdt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthDel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddOnDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditOnDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DelOnDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DelStatus = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItem", x => x.MenuId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuItem");
        }
    }
}
