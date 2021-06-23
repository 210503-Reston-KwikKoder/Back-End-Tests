﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace UserTestsDL.Migrations
{
    public partial class migation6242021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AverageWPM = table.Column<double>(type: "double precision", nullable: false),
                    AverageAccuracy = table.Column<double>(type: "double precision", nullable: false),
                    NumberOfTests = table.Column<int>(type: "integer", nullable: false),
                    TotalTestTime = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserStatId = table.Column<int>(type: "integer", nullable: false),
                    NumberOfErrors = table.Column<int>(type: "integer", nullable: false),
                    NumberOfWords = table.Column<int>(type: "integer", nullable: false),
                    TimeTaken = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WPM = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypeTests_UserStats_UserStatId",
                        column: x => x.UserStatId,
                        principalTable: "UserStats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserStatCatJoins",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    UserStatId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatCatJoins", x => new { x.UserStatId, x.UserId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_UserStatCatJoins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserStatCatJoins_UserStats_UserStatId",
                        column: x => x.UserStatId,
                        principalTable: "UserStats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TypeTests_UserStatId",
                table: "TypeTests",
                column: "UserStatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserStatCatJoins_UserId",
                table: "UserStatCatJoins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserStatCatJoins_UserStatId",
                table: "UserStatCatJoins",
                column: "UserStatId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TypeTests");

            migrationBuilder.DropTable(
                name: "UserStatCatJoins");

            migrationBuilder.DropTable(
                name: "UserStats");
        }
    }
}
