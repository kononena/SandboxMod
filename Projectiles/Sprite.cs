using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using SandboxMod.Projectiles;
using SandboxMod.Buffs;
using static Terraria.ModLoader.ModContent;

namespace SandboxMod.Projectiles
{
	public class Sprite : ModProjectile
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Sprite");
			Main.projFrames[projectile.type] = 1;
			Main.projPet[projectile.type] = true;
		}

		public override void SetDefaults() 
		{
			projectile.CloneDefaults(ProjectileID.ZephyrFish);
			aiType = ProjectileID.ZephyrFish;
		}

        public override bool PreAI()
        {
			Main.player[projectile.owner].zephyrfish = false;
			return true;
        }

        public override void AI()
        {
			Player player = Main.player[projectile.owner];
			SandboxPlayer modPlayer = player.GetModPlayer<SandboxPlayer>();
			if (player.dead) modPlayer.spritePet = false;
			if (modPlayer.spritePet) projectile.timeLeft = 2;
        }
    }
}