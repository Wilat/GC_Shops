using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gc_Shops.Db.MSSql.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    StackSize = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "MetroOwners",
                columns: table => new
                {
                    MetroOwnerId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetroOwners", x => x.MetroOwnerId);
                });

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    ShopId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Coordinate_X = table.Column<int>(nullable: true),
                    Coordinate_Y = table.Column<int>(nullable: true),
                    Coordinate_Z = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.ShopId);
                });

            migrationBuilder.CreateTable(
                name: "MetroStation",
                columns: table => new
                {
                    MetroStationId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    OwnerMetroOwnerId = table.Column<Guid>(nullable: true),
                    ShopId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetroStation", x => x.MetroStationId);
                    table.ForeignKey(
                        name: "FK_MetroStation_MetroOwners_OwnerMetroOwnerId",
                        column: x => x.OwnerMetroOwnerId,
                        principalTable: "MetroOwners",
                        principalColumn: "MetroOwnerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MetroStation_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "ShopId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ShopItem",
                columns: table => new
                {
                    ShopId = table.Column<Guid>(nullable: false),
                    ItemId = table.Column<string>(nullable: false),
                    SellCost = table.Column<decimal>(nullable: false),
                    ItemsInSell = table.Column<long>(nullable: false),
                    BuyCost = table.Column<decimal>(nullable: false),
                    ItemsInBuy = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopItem", x => new { x.ShopId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_ShopItem_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopItem_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "ShopId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ControlBlocks",
                columns: table => new
                {
                    ControlBlockId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Coordinate_X = table.Column<int>(nullable: true),
                    Coordinate_Y = table.Column<int>(nullable: true),
                    Coordinate_Z = table.Column<int>(nullable: true),
                    SouthCbControlBlockId = table.Column<int>(nullable: true),
                    NorthCbControlBlockId = table.Column<int>(nullable: true),
                    EastCbControlBlockId = table.Column<int>(nullable: true),
                    WestCbControlBlockId = table.Column<int>(nullable: true),
                    MetroStationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlBlocks", x => x.ControlBlockId);
                    table.ForeignKey(
                        name: "FK_ControlBlocks_ControlBlocks_EastCbControlBlockId",
                        column: x => x.EastCbControlBlockId,
                        principalTable: "ControlBlocks",
                        principalColumn: "ControlBlockId");
                    table.ForeignKey(
                        name: "FK_ControlBlocks_MetroStation_MetroStationId",
                        column: x => x.MetroStationId,
                        principalTable: "MetroStation",
                        principalColumn: "MetroStationId");
                    table.ForeignKey(
                        name: "FK_ControlBlocks_ControlBlocks_NorthCbControlBlockId",
                        column: x => x.NorthCbControlBlockId,
                        principalTable: "ControlBlocks",
                        principalColumn: "ControlBlockId");
                    table.ForeignKey(
                        name: "FK_ControlBlocks_ControlBlocks_SouthCbControlBlockId",
                        column: x => x.SouthCbControlBlockId,
                        principalTable: "ControlBlocks",
                        principalColumn: "ControlBlockId");
                    table.ForeignKey(
                        name: "FK_ControlBlocks_ControlBlocks_WestCbControlBlockId",
                        column: x => x.WestCbControlBlockId,
                        principalTable: "ControlBlocks",
                        principalColumn: "ControlBlockId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ControlBlocks_EastCbControlBlockId",
                table: "ControlBlocks",
                column: "EastCbControlBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlBlocks_MetroStationId",
                table: "ControlBlocks",
                column: "MetroStationId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlBlocks_NorthCbControlBlockId",
                table: "ControlBlocks",
                column: "NorthCbControlBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlBlocks_SouthCbControlBlockId",
                table: "ControlBlocks",
                column: "SouthCbControlBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlBlocks_WestCbControlBlockId",
                table: "ControlBlocks",
                column: "WestCbControlBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlBlocks_Coordinate_X_Coordinate_Y_Coordinate_Z",
                table: "ControlBlocks",
                columns: new[] { "Coordinate_X", "Coordinate_Y", "Coordinate_Z" });

            migrationBuilder.CreateIndex(
                name: "IX_MetroStation_OwnerMetroOwnerId",
                table: "MetroStation",
                column: "OwnerMetroOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_MetroStation_ShopId",
                table: "MetroStation",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItem_ItemId",
                table: "ShopItem",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControlBlocks");

            migrationBuilder.DropTable(
                name: "ShopItem");

            migrationBuilder.DropTable(
                name: "MetroStation");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "MetroOwners");

            migrationBuilder.DropTable(
                name: "Shops");
        }
    }
}
