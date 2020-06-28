using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using SandboxMod.Tiles;
using static Terraria.ModLoader.ModContent;

namespace SandboxMod.Items
{
	public class Transporter : ModItem
	{
		public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("Teleport to this transporter with the Transporter Key");
        }

        public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(silver: 50);
			item.createTile = TileType<Tiles.Transporter>();
		}

        public override void AddRecipes()
        {
			ModRecipe rec = new ModRecipe(mod);
			rec.AddIngredient(ItemID.Teleporter);
			rec.AddIngredient(ItemID.MartianConduitPlating, 10);
			rec.AddTile(TileID.MythrilAnvil);
			rec.SetResult(this);
			rec.AddRecipe();
        }
    }
}