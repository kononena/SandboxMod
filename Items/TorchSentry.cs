using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using SandboxMod.Projectiles;
using static Terraria.ModLoader.ModContent;

namespace SandboxMod.Items
{
	public class TorchSentry : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Torch Sentry");
			Tooltip.SetDefault("Summons a torch to light for you\n" +
				"Let there be light");
			ItemID.Sets.GamepadWholeScreenUseRange[item.type] = true;
			ItemID.Sets.LockOnIgnoresCollision[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.mana = 10;
			item.width = 28;
			item.height = 46;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = Item.buyPrice(gold: 5);
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item44;

			item.noMelee = true;
			item.summon = true;
			item.sentry = true;
			item.shoot = ProjectileType<Projectiles.TorchSentry>();
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