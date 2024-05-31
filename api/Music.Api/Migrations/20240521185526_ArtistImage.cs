using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Music.Api.Migrations
{
    /// <inheritdoc />
    public partial class ArtistImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Track_Albums_AlbumId",
                table: "Track");

            migrationBuilder.DropTable(
                name: "AlbumTracks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Track",
                table: "Track");

            migrationBuilder.RenameTable(
                name: "Track",
                newName: "Tracks");

            migrationBuilder.RenameIndex(
                name: "IX_Track_AlbumId",
                table: "Tracks",
                newName: "IX_Tracks_AlbumId");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Artists",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CoverImageId",
                table: "Albums",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Tracks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Tracks",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tracks",
                table: "Tracks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Albums_AlbumId",
                table: "Tracks",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Albums_AlbumId",
                table: "Tracks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tracks",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "CoverImageId",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Tracks");

            migrationBuilder.RenameTable(
                name: "Tracks",
                newName: "Track");

            migrationBuilder.RenameIndex(
                name: "IX_Tracks_AlbumId",
                table: "Track",
                newName: "IX_Track_AlbumId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Track",
                table: "Track",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AlbumTracks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TrackId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Position = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumTracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlbumTracks_Track_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Track",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlbumTracks_TrackId",
                table: "AlbumTracks",
                column: "TrackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Track_Albums_AlbumId",
                table: "Track",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
