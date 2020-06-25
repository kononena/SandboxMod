using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SandboxMod.Items
{
	public class RocketJumpRocket : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("boing");
		}

		public override void SetDefaults()
		{
			item.ranged = true;
			item.width = 68;
			item.height = 26;
			item.useTime = 45;
			item.useAnimation = 45;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.value = 10000;
			item.rare = ItemRarityID.Cyan;
			item.UseSound = SoundID.Item14;
			item.autoReuse = true;
			item.shootSpeed = 16f;
			item.shoot = ProjectileID.Twinkle;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			player.velocity -= new Vector2(speedX, speedY) * 1f;
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

        public override Vector2? HoldoutOffset()
        {
			return new Vector2(-item.width / 2 + 5, 0);
        }
    }
}