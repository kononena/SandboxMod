using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using SandboxMod.Items;
using SandboxMod.Projectiles;
using SandboxMod.Tiles;
using System.Linq;

namespace SandboxMod
{
	public class SandboxPlayer : ModPlayer
    {
		public int LifeTime = 0;
        public bool celRanger;
        public bool fist;
        public bool spritePet;
        public int halfSentries;

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            string message = player.name + " was alive for " + LifeTime / 60 + " seconds.";
            Main.NewText(message, Color.Green);
            base.Kill(damage, hitDirection, pvp, damageSource);
            LifeTime = 0;
        }

        public override void ModifyWeaponDamage(Item item, ref float add, ref float mult, ref float flat)
        {
            if (item.summon && item.useTime > 10 && item.useAnimation > 10)
            {
                item.autoReuse = true;
                item.useTime = (int)MathHelper.Max(item.useTime / 3, 10);
                item.useAnimation = (int)MathHelper.Max(item.useAnimation / 3, 10);
            }
            base.ModifyWeaponDamage(item, ref add, ref mult, ref flat);
        }

        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (item.ranged)
            {
                Vector2 newVelocity = new Vector2(speedX, speedY);
                newVelocity = newVelocity.RotatedByRandom(MathHelper.ToRadians(10));
                speedX = newVelocity.X;
                speedY = newVelocity.Y;
                return base.Shoot(item, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
            }
            if (item.melee && item.noMelee)
            {
                speedX *= 2.0f;
                speedY *= 2.0f;
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileType<PewPew>(), damage, knockBack, player.whoAmI);
                foreach (Projectile proj in Main.projectile)
                {
                    if (proj.melee && proj.owner == player.whoAmI) proj.scale = 2.0f;
                }
                return false;
            }
            return base.Shoot(item, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override void MeleeEffects(Item item, Rectangle hitbox)
        {
            item.scale = 10;
            foreach (NPC target in Main.npc) target.immune[player.whoAmI] = 0;
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (celRanger) CelRanger.ApplyDebuffs(target);
            if (fist) Fist.ApplyDebuffs(target);
        }

        public override void ResetEffects()
        {
            if (celRanger)
            {
                player.meleeDamageMult /= 10f;
                player.statDefense += 10;
                player.endurance += 0.1f;
                player.aggro += 400;
                player.maxTurrets += player.maxMinions;
                player.maxMinions = 0;
                player.statLifeMax2 += 8501;
                player.lifeRegen += 100;
                player.blockRange += 40;
            }
            celRanger = false;
            spritePet = false;
            player.maxTurrets += halfSentries / 2;
            halfSentries = 0;
            LifeTime++;
            Main.dayTime = true;

            
        }
    }

	class SandboxMod : Mod
	{
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(this);
            recipe.SetResult(ItemID.Spear);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.SetResult(ItemID.ImpStaff);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.SetResult(ItemID.PossessedHatchet);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.SetResult(ItemID.QueenSpiderStaff);
            recipe.AddRecipe();

            base.AddRecipes();
        }
    }
}