using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace TutorialMod.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class flatcap : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Pappalätsä");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 18;
            item.value = 1;
            item.rare = ItemRarityID.Green;
            item.defense = 1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}