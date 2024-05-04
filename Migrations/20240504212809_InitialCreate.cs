using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Family_Meetup.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    creator = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false),
                    location = table.Column<string>(type: "TEXT", nullable: false),
                    creationdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    maxvotesondate = table.Column<int>(type: "INTEGER", nullable: false),
                    maxvotesbyuser = table.Column<int>(type: "INTEGER", nullable: false),
                    userWhiteList = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    username = table.Column<string>(type: "TEXT", nullable: false),
                    comment = table.Column<string>(type: "TEXT", nullable: false),
                    Eventid = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Events_Eventid",
                        column: x => x.Eventid,
                        principalTable: "Events",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "MeetupDateVoteOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    votedusers = table.Column<string>(type: "TEXT", nullable: false),
                    Eventid = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetupDateVoteOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetupDateVoteOptions_Events_Eventid",
                        column: x => x.Eventid,
                        principalTable: "Events",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Eventid",
                table: "Comments",
                column: "Eventid");

            migrationBuilder.CreateIndex(
                name: "IX_MeetupDateVoteOptions_Eventid",
                table: "MeetupDateVoteOptions",
                column: "Eventid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "MeetupDateVoteOptions");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
