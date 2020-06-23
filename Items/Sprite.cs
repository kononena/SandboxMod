using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using SandboxMod.Projectiles;
using SandboxMod.Buffs;
using static Terraria.ModLoader.ModContent;

namespace SandboxMod.Items
{
	public class Sprite : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Delicious goodness");
		}

		public override void SetDefaults() 
		{
			item.CloneDefaults(ItemID.ZephyrFish);
			item.width = 26;
			item.height = 44;
			item.shoot = ProjectileType<Projectiles.Sprite>();
			item.buffType = BuffType<Buffs.Sprite>();
		}

        public override void AddRecipes()
        {
			ModRecipe rec = new ModRecipe(mod);
			rec.SetResult(this);
			rec.AddRecipe();
        }

        public override void UseStyle(Player player)
        {
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0) player.AddBuff(item.buffType, 3600, true);
        }

    }
}