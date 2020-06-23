using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TutorialMod.Items
{
	public class CelRanger : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("TutorialSword"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("This is... cellery.");
		}

		public override void SetDefaults() 
		{
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			item.value = 10;
			item.rare = ItemRarityID.Cyan;
		}

		public static void ApplyDebuffs(NPC target)
        {
			target.AddBuff(BuffID.ShadowFlame, 60);
			target.AddBuff(BuffID.CursedInferno, 60);
			target.AddBuff(BuffID.OnFire, 60);
			target.AddBuff(BuffID.Frostburn, 60);
			target.AddBuff(203, 60);
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<TutorialPlayer>().celRanger = true;
        }

        public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}

	public class Fist : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("TutorialSword"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Le fists.\nSets the enemy on... stuff.");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			item.value = 10;
			item.rare = ItemRarityID.Cyan;
		}

		public static void ApplyDebuffs(NPC target)
		{
			target.AddBuff(BuffID.ShadowFlame, 60);
			target.AddBuff(BuffID.Ichor, 60);
			target.AddBuff(BuffID.OnFire, 60);
			target.AddBuff(BuffID.Venom, 60);
			target.AddBuff(BuffID.Poisoned, 60);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<TutorialPlayer>().fist = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}