using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using SandboxMod.Projectiles;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;

namespace SandboxMod.Projectiles
{
	public class IchorStatue : ModProjectile
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Ichor Statue");
			Main.projFrames[projectile.type] = 1;
			ProjectileID.Sets.SentryShot[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;
		}

        public override void SetDefaults()
        {
			projectile.width = 32;
			projectile.height = 48;
			projectile.tileCollide = true;
			projectile.sentry = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 7200;
			projectile.damage = 0;
		}

        public override bool? CanCutTiles()
        {
			return false;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
			Main.player[projectile.owner].UpdateMaxTurrets();
			return base.PreDraw(spriteBatch, lightColor);
        }

        public override void AI()
        {
			Player player = Main.player[projectile.owner];

			if (player.dead || !player.active) projectile.timeLeft = 0;

			if (projectile.ai[0] < 0) projectile.ai[0] = 0;
			else projectile.ai[0]--;

			float distanceFromTarget = 700f;
			Vector2 targetCenter = projectile.position;
			bool foundTarget = false;

			if (player.HasMinionAttackTargetNPC)
            {
				NPC npc = Main.npc[player.MinionAttackTargetNPC];
				float between = Vector2.Distance(npc.Center, projectile.Center);
				if (between < 2000f)
                {
					targetCenter = npc.Center;
					foundTarget = true;
                }
            }
			if (!foundTarget)
            {
				for (int i = 0; i < Main.maxNPCs; i++)
                {
					NPC npc = Main.npc[i];
					if (npc.CanBeChasedBy())
                    {
						float between = Vector2.Distance(npc.Center, projectile.Center);
						bool lineOfSight = Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height);
						if (lineOfSight && between < distanceFromTarget)
                        {
							targetCenter = npc.Center;
							foundTarget = true;
						}
					}
                }
            }

			if (foundTarget && projectile.ai[0] == 0)
            {
				projectile.ai[0] = 60;
				Vector2 velocity = (targetCenter - projectile.position) / (targetCenter - projectile.position).Length() * 5f;
				Projectile.NewProjectile(projectile.position, velocity, ProjectileID.IchorSplash, 10, 1f, player.whoAmI);
				projectile.spriteDirection = velocity.X < 0 ? 1 : -1;
            }

			if (projectile.velocity.Y >= 16f) projectile.velocity.Y = 16f;
			else projectile.velocity.Y += 0.1f;
        }

    }
}