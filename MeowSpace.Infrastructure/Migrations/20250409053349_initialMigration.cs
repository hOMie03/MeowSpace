using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeowSpace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PetPosts",
                columns: table => new
                {
                    PostID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCreationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PostUpdationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetPosts", x => x.PostID);
                });

            migrationBuilder.CreateTable(
                name: "PostComments",
                columns: table => new
                {
                    CommentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PetPostPostID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostComments", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_PostComments_PetPosts_PetPostPostID",
                        column: x => x.PetPostPostID,
                        principalTable: "PetPosts",
                        principalColumn: "PostID");
                });

            migrationBuilder.CreateTable(
                name: "PostLikes",
                columns: table => new
                {
                    PostLikeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LikesCounter = table.Column<int>(type: "int", nullable: false),
                    PetPostPostID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostLikes", x => x.PostLikeID);
                    table.ForeignKey(
                        name: "FK_PostLikes_PetPosts_PetPostPostID",
                        column: x => x.PetPostPostID,
                        principalTable: "PetPosts",
                        principalColumn: "PostID");
                });

            migrationBuilder.CreateTable(
                name: "PostCommentLikes",
                columns: table => new
                {
                    CommentLikeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentLikesCounter = table.Column<int>(type: "int", nullable: false),
                    PostCommentCommentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCommentLikes", x => x.CommentLikeID);
                    table.ForeignKey(
                        name: "FK_PostCommentLikes_PostComments_PostCommentCommentID",
                        column: x => x.PostCommentCommentID,
                        principalTable: "PostComments",
                        principalColumn: "CommentID");
                });

            migrationBuilder.InsertData(
                table: "PetPosts",
                columns: new[] { "PostID", "PostCreationDate", "PostDescription", "PostImage", "PostTitle", "PostUpdationDate", "UserID" },
                values: new object[] { 1, new DateOnly(2025, 4, 9), "Dharampal", "https://i.pinimg.com/736x/bc/08/e9/bc08e964513eb63238efabde6cd193d4.jpg", "Dharampal", new DateOnly(2025, 4, 9), "41776062 - 2222 - 1bbb - a222 - 2879a6680b9a" });

            migrationBuilder.CreateIndex(
                name: "IX_PostCommentLikes_PostCommentCommentID",
                table: "PostCommentLikes",
                column: "PostCommentCommentID");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_PetPostPostID",
                table: "PostComments",
                column: "PetPostPostID");

            migrationBuilder.CreateIndex(
                name: "IX_PostLikes_PetPostPostID",
                table: "PostLikes",
                column: "PetPostPostID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostCommentLikes");

            migrationBuilder.DropTable(
                name: "PostLikes");

            migrationBuilder.DropTable(
                name: "PostComments");

            migrationBuilder.DropTable(
                name: "PetPosts");
        }
    }
}
