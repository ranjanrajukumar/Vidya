using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vidya.Migrations
{
    /// <inheritdoc />
    public partial class UpdateToRights : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblUserRights",
                columns: table => new
                {
                    IDCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuID = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_tblUserRights", x => x.IDCode);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblUserRights");
        }
    }
}
