using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TruckSystem.DAL.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_TruckModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TruckModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_Truck",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ManufactureYear = table.Column<int>(nullable: false),
                    ModelYear = table.Column<int>(nullable: false),
                    IdModel = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Truck", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_Truck_TB_TruckModel_IdModel",
                        column: x => x.IdModel,
                        principalTable: "TB_TruckModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_Truck_Id",
                table: "TB_Truck",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Truck_IdModel",
                table: "TB_Truck",
                column: "IdModel");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TruckModel_Id",
                table: "TB_TruckModel",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_Truck");

            migrationBuilder.DropTable(
                name: "TB_TruckModel");
        }
    }
}
