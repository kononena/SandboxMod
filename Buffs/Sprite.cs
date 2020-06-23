using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using SandboxMod.Projectiles;
using SandboxMod.Buffs;
using static Terraria.ModLoader.ModContent;

namespace SandboxMod.Buffs
{
	public class Sprite : ModBuff
	{
		public override void SetDefaults() 
		{
			DisplayName.SetDefault("Sprite");
			Description.SetDefault("Don't drink him up");
			Main.buffNoTimeDisplay[Type] = true;
			Main.vanityPet[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
			player.buffTime[buffIndex] = 18000;
			player.GetModPlayer<SandboxPlayer>().spritePet = true;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[ProjectileType<Projectiles.Sprite>()] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
				Projectile.NewProjectile(player.position.X, player.position.Y, 0f, 0f, ProjectileType<Projectiles.Sprite>(), 0, 0f, player.whoAmI);
            }
        }
    }
}