using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "library");

            migrationBuilder.CreateTable(
                name: "author",
                schema: "library",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("author_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "genre",
                schema: "library",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("genre_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "library",
                schema: "library",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    priority = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_library", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "book",
                schema: "library",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    pages = table.Column<int>(type: "integer", nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    genreid = table.Column<string>(type: "text", nullable: true),
                    LibraryId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("book_pkey", x => x.id);
                    table.ForeignKey(
                        name: "FK_book_library_LibraryId",
                        column: x => x.LibraryId,
                        principalSchema: "library",
                        principalTable: "library",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "book_genreid_fkey",
                        column: x => x.genreid,
                        principalSchema: "library",
                        principalTable: "genre",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "authorbookjunction",
                schema: "library",
                columns: table => new
                {
                    authorid = table.Column<string>(type: "text", nullable: false),
                    bookid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("authorbookjunction_pkey", x => new { x.authorid, x.bookid });
                    table.ForeignKey(
                        name: "authorbookjunction_authorid_fkey",
                        column: x => x.authorid,
                        principalSchema: "library",
                        principalTable: "author",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "authorbookjunction_bookid_fkey",
                        column: x => x.bookid,
                        principalSchema: "library",
                        principalTable: "book",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_authorbookjunction_bookid",
                schema: "library",
                table: "authorbookjunction",
                column: "bookid");

            migrationBuilder.CreateIndex(
                name: "IX_book_genreid",
                schema: "library",
                table: "book",
                column: "genreid");

            migrationBuilder.CreateIndex(
                name: "IX_book_LibraryId",
                schema: "library",
                table: "book",
                column: "LibraryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "authorbookjunction",
                schema: "library");

            migrationBuilder.DropTable(
                name: "author",
                schema: "library");

            migrationBuilder.DropTable(
                name: "book",
                schema: "library");

            migrationBuilder.DropTable(
                name: "library",
                schema: "library");

            migrationBuilder.DropTable(
                name: "genre",
                schema: "library");
        }
    }
}
