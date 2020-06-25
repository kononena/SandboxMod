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
	public class TorchSentry : ModProjectile
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Torch Sentry");
			Main.projFrames[projectile.type] = 1;
			ProjectileID.Sets.SentryShot[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;
		}

        public override void SetDefaults()
        {
			projectile.width = 28;
			projectile.height = 46;
			projectile.tileCollide = true;
			projectile.sentry = true;
			projectile.timeLeft = 7200;
			projectile.damage = 0;
			projectile.light = 4f;
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

			if (projectile.velocity.Y >= 16f) projectile.velocity.Y = 16f;
			else projectile.velocity.Y += 0.3f;
        }

    }
}