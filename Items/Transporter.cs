using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using SandboxMod.Tiles;
using static Terraria.ModLoader.ModContent;

namespace SandboxMod.Items
{
	public class Transporter : ModItem
	{
		public override string Texture => "Terraria/Item_" + ItemID.Teleporter;

        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("bruh dont try this at home");
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
			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(gold: 1);
			item.createTile = TileType<Tiles.Transporter>();
		}

        public override void AddRecipes()
        {
			ModRecipe rec = new ModRecipe(mod);
			rec.SetResult(this);
			rec.AddRecipe();
        }
    }
}