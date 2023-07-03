using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anubis.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRefreshTokens",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Created = table.Column<DateTime>(type: "DateTime", nullable: false),
                    Expires = table.Column<DateTime>(type: "DateTime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRefreshTokens", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_UserRefreshTokens_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRefreshTokens");
        }
    }
}
