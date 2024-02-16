using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    public partial class DialerMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    bookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    bookName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    bookAuthor = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    bookDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imgUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.bookId);
                });

            migrationBuilder.CreateTable(
                name: "PROJECTS",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Projectname = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Projectdescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROJECTS", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    roleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    roleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.roleId);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fullname = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    pass = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    roleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.userId);
                    table.ForeignKey(
                        name: "FK__USERS__roleId__3E1D39E1",
                        column: x => x.roleId,
                        principalTable: "Roles",
                        principalColumn: "roleId");
                });

            migrationBuilder.CreateTable(
                name: "AssignedProject",
                columns: table => new
                {
                    ProjectAssignedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    isLead = table.Column<bool>(type: "bit", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Assigned__7BD7841502347B7E", x => x.ProjectAssignedId);
                    table.ForeignKey(
                        name: "FK__AssignedP__Proje__503BEA1C",
                        column: x => x.ProjectId,
                        principalTable: "PROJECTS",
                        principalColumn: "ProjectId");
                    table.ForeignKey(
                        name: "FK__AssignedP__userI__4F47C5E3",
                        column: x => x.userId,
                        principalTable: "USERS",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignedProject_ProjectId",
                table: "AssignedProject",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedProject_userId",
                table: "AssignedProject",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_USERS_roleId",
                table: "USERS",
                column: "roleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignedProject");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "PROJECTS");

            migrationBuilder.DropTable(
                name: "USERS");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
