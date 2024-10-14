using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    public partial class InitialCreate : Migration
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
                    updatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    avatarUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    projectkey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
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
                name: "Room",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RoomName = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: true),
                    Token = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.Id);
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
                        name: "FK__USERS__roleId__44FF419A",
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
                    table.PrimaryKey("PK__Assigned__7BD78415E9523B53", x => x.ProjectAssignedId);
                    table.ForeignKey(
                        name: "FK__AssignedP__Proje__412EB0B6",
                        column: x => x.ProjectId,
                        principalTable: "PROJECTS",
                        principalColumn: "ProjectId");
                    table.ForeignKey(
                        name: "FK__AssignedP__userI__403A8C7D",
                        column: x => x.userId,
                        principalTable: "USERS",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    ticketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ticketsummary = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ticketdescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    assignedTo = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    reportedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    projectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ticketpriority = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ticketstatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    tickettype = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.ticketId);
                    table.ForeignKey(
                        name: "FK__Tickets__assigne__4222D4EF",
                        column: x => x.assignedTo,
                        principalTable: "USERS",
                        principalColumn: "userId");
                    table.ForeignKey(
                        name: "FK__Tickets__project__440B1D61",
                        column: x => x.projectId,
                        principalTable: "PROJECTS",
                        principalColumn: "ProjectId");
                    table.ForeignKey(
                        name: "FK__Tickets__reporte__4316F928",
                        column: x => x.reportedBy,
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
                name: "IX_Tickets_assignedTo",
                table: "Tickets",
                column: "assignedTo");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_projectId",
                table: "Tickets",
                column: "projectId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_reportedBy",
                table: "Tickets",
                column: "reportedBy");

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
                name: "Room");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "USERS");

            migrationBuilder.DropTable(
                name: "PROJECTS");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
