using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SandboxMod.Items
{
	public class TornadoStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Shoots a tornado that traps NPCs");
			Item.staff[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.magic = true;
			item.mana = 10;
			item.damage = 10;
			item.knockBack = 0f;
			item.width = 40;
			item.height = 40;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.value = 10000;
			item.rare = ItemRarityID.Cyan;
			item.autoReuse = true;
			item.shootSpeed = 4f;
			item.shoot = ProjectileType<Projectiles.Tornado>();
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, speedX, speedY);
			return false;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}