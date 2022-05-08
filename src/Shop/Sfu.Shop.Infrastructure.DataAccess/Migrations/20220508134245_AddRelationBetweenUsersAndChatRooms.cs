using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sfu.Shop.Infrastructure.DataAccess.Migrations
{
    public partial class AddRelationBetweenUsersAndChatRooms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ChatRooms_ChatRoomId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ChatRoomId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ChatRoomId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "ChatRoomUser",
                columns: table => new
                {
                    FollowersId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubscriptionsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRoomUser", x => new { x.FollowersId, x.SubscriptionsId });
                    table.ForeignKey(
                        name: "FK_ChatRoomUser_AspNetUsers_FollowersId",
                        column: x => x.FollowersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatRoomUser_ChatRooms_SubscriptionsId",
                        column: x => x.SubscriptionsId,
                        principalTable: "ChatRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatRoomUser_SubscriptionsId",
                table: "ChatRoomUser",
                column: "SubscriptionsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatRoomUser");

            migrationBuilder.AddColumn<Guid>(
                name: "ChatRoomId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ChatRoomId",
                table: "AspNetUsers",
                column: "ChatRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ChatRooms_ChatRoomId",
                table: "AspNetUsers",
                column: "ChatRoomId",
                principalTable: "ChatRooms",
                principalColumn: "Id");
        }
    }
}
