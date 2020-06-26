using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SandboxMod.Projectiles
{
	public class Tornado : ModProjectile
	{
		private readonly List<NPC> victims = new List<NPC>();
		private int angle = 0;
		private readonly Vector2 vortex = new Vector2(16, 0);

		public override string Texture => "Terraria/Projectile_" + ProjectileID.Tempest;

        public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Tornado");
			Main.projFrames[projectile.type] = 6;
		}

        public override void SetDefaults()
        {
			projectile.width = 40;
			projectile.height = 40;
			projectile.tileCollide = true;
			projectile.timeLeft = 120;
			projectile.magic = true;
			projectile.knockBack = 0f;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 30;
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			if (!victims.Contains(target)) victims.Add(target);
        }

        public override void AI()
		{
			projectile.velocity = new Vector2(projectile.ai[0], projectile.ai[1]);
			angle += 15;

			if (++projectile.frameCounter >= 5)
            {
				projectile.frameCounter = 0;
				projectile.frame++;
				projectile.frame %= 6;
            }

			foreach (NPC victim in victims)
            {
				victim.position = projectile.position;
				victim.position += vortex.RotatedBy(MathHelper.ToRadians(angle));
            }

			base.AI();
		}

    }
}