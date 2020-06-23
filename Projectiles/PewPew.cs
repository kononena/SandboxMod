using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Steamworks;
using System.Linq;

namespace SandboxMod.Projectiles
{
	public class PewPew : ModProjectile
	{
		private bool closeToCursor;

		public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.tileCollide = false;
			projectile.light = 0.5f;
			projectile.timeLeft = 300;
		}

        public override void AI()
        {
			Vector2 center = Main.MouseWorld;
			Vector2 displacement = projectile.position - center;
			float circleRadius = 100;

			List<Vector2> directions = new List<Vector2>();
			for (int i = 0; i < 200; i++)
			{
				NPC target = Main.npc[i];
				if (!target.dontTakeDamage && !target.friendly && target.active)
				{
					directions.Add(projectile.position - target.position);
				}
			}

			if (directions.Count > 0)
			{
				Vector2 newDir = directions.OrderBy(dir => dir.Length()).First();
				projectile.velocity = -newDir / newDir.Length() * 16f;
			}
			else 
            {
				closeToCursor = displacement.Length() < circleRadius;
				if (closeToCursor) projectile.velocity = displacement.RotatedBy(MathHelper.ToRadians(90)) / displacement.Length() * 5f;
				else projectile.velocity = -displacement / displacement.Length() * 16f;
            }

			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
			projectile.spriteDirection = projectile.direction;
		}
    }
}