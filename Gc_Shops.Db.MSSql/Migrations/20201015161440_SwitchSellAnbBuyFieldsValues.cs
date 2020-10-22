using Microsoft.EntityFrameworkCore.Migrations;

namespace Gc_Shops.Db.MSSql.Migrations
{
    public partial class SwitchSellAnbBuyFieldsValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "SellCost",
                "ShopItem",
                "SellCost2");
            migrationBuilder.RenameColumn(
                "ItemsInSell",
                "ShopItem",
                "ItemsInSell2");
            migrationBuilder.RenameColumn(
                "BuyCost",
                "ShopItem",
                "BuyCost2");
            migrationBuilder.RenameColumn(
                "ItemsInBuy",
                "ShopItem",
                "ItemsInBuy2");


            migrationBuilder.RenameColumn(
                "SellCost2",
                "ShopItem",
                "BuyCost");
            migrationBuilder.RenameColumn(
                "ItemsInSell2",
                "ShopItem",
                "ItemsInBuy");
            migrationBuilder.RenameColumn(
                "BuyCost2",
                "ShopItem",
                "SellCost");
            migrationBuilder.RenameColumn(
                "ItemsInBuy2",
                "ShopItem",
                "ItemsInSell");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
