using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TutorialMod.Items
{
	public class SniperBlaster : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("pew pew");
		}

		public override void SetDefaults() 
		{
			item.damage = 1;
			item.ranged = true;
			item.width = 40;
			item.height = 20;
			item.useTime = 5;
			item.useAnimation = 5;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 50;
			item.value = 10000;
			item.rare = ItemRarityID.Cyan;
			item.UseSound = SoundID.Item36;
			item.autoReuse = true;
			item.shootSpeed = 16f;

			item.shoot = ProjectileID.BlackBolt;
			//item.useAmmo = AmmoID.Bullet;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float num = 3;
			float rot = MathHelper.ToRadians(10);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 10f;
			for (int i = 0; i < num; i++)
            {
				Vector2 projSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rot, rot, i / (num - 1))) * 2f;
				Projectile.NewProjectile(position.X, position.Y, projSpeed.X, projSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
			player.velocity -= new Vector2(speedX, speedY) * 0.1f;
			return false;
        }

        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.SetResult(ItemID.MusketBall);
			recipe.AddRecipe();
		}

        public override bool ConsumeAmmo(Player player)
        {
			return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-15, 0);
        }
    }
}