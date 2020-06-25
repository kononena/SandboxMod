using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;

namespace SandboxMod.Projectiles
{
	public class Telekinesis : ModProjectile
	{
		private NPC victim = null;
		private int grabFrameTimer = 0;

		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Telekinesis Hand");
			Main.projFrames[projectile.type] = 3;
		}

        public override void SetDefaults()
        {
			projectile.width = 50;
			projectile.height = 50;
			projectile.tileCollide = true;
			projectile.timeLeft = 100;
		}

        public override bool? CanCutTiles()
        {
			return false;
        }

        public override void AI()
		{
			Player player = Main.player[projectile.owner];
			Vector2 mousePosition = Main.MouseWorld;
			projectile.Center = mousePosition;
			//projectile.Center += (mousePosition - player.Center) / (mousePosition - player.Center).Length() * 2 * 16;

			
			projectile.ai[0] += 1f;
			if (projectile.soundDelay <= 0)
			{
				projectile.soundDelay = 20;
				Main.PlaySound(SoundID.Item15, projectile.position);
			}
			if (Main.myPlayer == projectile.owner && projectile.ai[0] % 10 == 0)
			{
				bool flag12 = player.CheckMana(player.inventory[player.selectedItem], -1, pay: true);
				if (!player.channel || !flag12 || player.noItems || player.CCed) projectile.Kill();
			}

			projectile.timeLeft = 2;
			player.heldProj = projectile.whoAmI;
			player.itemTime = 2;
			player.itemAnimation = 2;
			projectile.rotation = (player.position - projectile.position).ToRotation() - (float)Math.PI / 2;
			projectile.spriteDirection = (player.Center - projectile.Center).X > 0 ? -1 : 1;
			player.itemRotation = (projectile.position - player.position).ToRotation();
			if (projectile.spriteDirection == -1) player.itemRotation = (float)Math.PI + (projectile.position - player.position).ToRotation();
			player.ChangeDir(projectile.spriteDirection);
			drawOriginOffsetY = projectile.spriteDirection == 1 ? -50 : -40;
			drawOriginOffsetX = projectile.spriteDirection == 1 ? 5 : -10;
			drawOffsetX = projectile.spriteDirection == 1 ? -21 : 0;

			if (victim == null)
            {
				int sizeLimit = 50;
				foreach (NPC npc in Main.npc)
				{
					if (!npc.boss && npc.aiStyle != 6 && npc.width < sizeLimit && npc.height < sizeLimit && (npc.Center - mousePosition).Length() < 16f * 3 && Collision.CanHitLine(mousePosition, 1, 1, npc.position, npc.width, npc.height))
					{
						victim = npc;
						grabFrameTimer = 10;
						projectile.frame++;
						break;
					}
				}
			}
			else
            {
				if (WorldGen.SolidTile((int)(mousePosition.X / 16), (int)(mousePosition.Y / 16)) || !Collision.CanHitLine(mousePosition, 1, 1, victim.position, victim.width, victim.height)) projectile.Kill();
				else
				{
					if (grabFrameTimer > 0) grabFrameTimer--;
					else if (grabFrameTimer == 0) { projectile.frame++; grabFrameTimer = -1; }
					victim.Center = mousePosition;
					victim.velocity *= 0f;
				}
            }
		}

    }
}