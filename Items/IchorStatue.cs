using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using SandboxMod.Projectiles;
using static Terraria.ModLoader.ModContent;

namespace SandboxMod.Items
{
	public class IchorStatue : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Ichor Statue");
			Tooltip.SetDefault("Summons an ichor statue to fight for you\n" +
				"It's everywhere");
			ItemID.Sets.GamepadWholeScreenUseRange[item.type] = true;
			ItemID.Sets.LockOnIgnoresCollision[item.type] = true;
		}

		public override void SetDefaults() 
		{
			item.damage = 1;
			item.knockBack = 1f;
			item.mana = 10;
			item.width = 20;
			item.height = 20;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = Item.buyPrice(gold: 5);
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item44;

			item.noMelee = true;
			item.summon = true;
			item.sentry = true;
			item.shoot = ProjectileType<Projectiles.IchorStatue>();
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			position = Main.MouseWorld;
			player.UpdateMaxTurrets();
			return true;
        }

        public override void AddRecipes()
        {
			ModRecipe rec = new ModRecipe(mod);
			rec.SetResult(this);
			rec.AddRecipe();
        }

    }
}