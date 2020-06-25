using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Input;
using static Terraria.ModLoader.ModContent;

namespace SandboxMod.Items
{
	public class TelekinesisStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Grants the ability to move small non-boss NPC");
		}

		public override void SetDefaults()
		{
			item.magic = true;
			item.mana = 3;
			item.width = 68;
			item.height = 26;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.value = 10000;
			item.rare = ItemRarityID.Cyan;
			item.autoReuse = true;
			item.shootSpeed = 1f;
			item.shoot = ProjectileType<Projectiles.Telekinesis>();
			item.channel = true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 mousePosition = Main.MouseWorld;
			if (WorldGen.SolidTile((int)(mousePosition.X / 16), (int)(mousePosition.Y / 16))) return false;
			position = mousePosition;
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 0);
        }
    }
}