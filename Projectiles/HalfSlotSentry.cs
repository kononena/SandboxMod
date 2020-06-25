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
	public class HalfSlotSentryStand : ModProjectile
    {
		private Projectile gun;
		private bool justSpawned = true;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shotgunsentry Stand");
			Main.projFrames[projectile.type] = 1;
		}

		public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 28;
			projectile.tileCollide = true;
			projectile.sentry = true;
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
			Main.LocalPlayer.UpdateMaxTurrets();
			Main.LocalPlayer.maxTurrets += 1;
            return base.PreDraw(spriteBatch, lightColor);
        }

        public override void AI()
        {
			if (justSpawned)
            {
				Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0f, 0f, ProjectileType<HalfSlotSentryGun>(), 0, 0f, Main.myPlayer, 1);
				foreach (Projectile proj in Main.projectile)
				{
					if (proj.type == ProjectileType<HalfSlotSentryGun>() && proj.owner == Main.myPlayer && proj.ai[0] == 1)
					{
						gun = proj;
						proj.ai[0] = 2;
						justSpawned = false;
						break;
					}
				}
			}

			Player player = Main.player[projectile.owner];
			player.GetModPlayer<SandboxPlayer>().halfSentries += 1;

			if (projectile.velocity.Y >= 16f) projectile.velocity.Y = 16f;
			else projectile.velocity.Y += 0.3f;

			gun.position.X = projectile.position.X - (gun.spriteDirection > 0 ? 8 : gun.width / 4 + 8);
			gun.position.Y = projectile.position.Y;
		}

        public override void Kill(int timeLeft)
        {
			gun.Kill();
            base.Kill(timeLeft);
        }
    }

    public class HalfSlotSentryGun : ModProjectile
    {
		private int shootTimer = 0;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shotgunsentry Gun");
			Main.projFrames[projectile.type] = 1;
			ProjectileID.Sets.SentryShot[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;
		}

		public override void SetDefaults()
		{
			projectile.width = 43;
			projectile.height = 12;
			projectile.tileCollide = true;
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

		public override void AI()
		{
			Player player = Main.player[projectile.owner];

			projectile.timeLeft = 7200;

			if (shootTimer < 0) shootTimer = 0;
			else shootTimer--;

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

			if (foundTarget && shootTimer == 0)
			{
				shootTimer = 60;
				Vector2 velocity = (targetCenter - projectile.position) / (targetCenter - projectile.position).Length() * 5f;
				Projectile.NewProjectile(projectile.position, velocity, ProjectileID.Bullet, 10, 1f, player.whoAmI);
				Projectile.NewProjectile(projectile.position, velocity.RotatedBy(MathHelper.ToRadians(-5)), ProjectileID.Bullet, 10, 1f, player.whoAmI);
				Projectile.NewProjectile(projectile.position, velocity.RotatedBy(MathHelper.ToRadians(5)), ProjectileID.Bullet, 10, 1f, player.whoAmI);
				projectile.spriteDirection = velocity.X > 0 ? 1 : -1;
				if (velocity.X > 0) projectile.rotation = velocity.ToRotation();
				else projectile.rotation = (-velocity).ToRotation();
			}
		}
	}
}