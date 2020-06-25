using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using SandboxMod.Projectiles;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using System;

namespace SandboxMod.Projectiles
{
	public class Telekinesis : ModProjectile
	{
		private NPC victim = null;
		private bool victimGravity;

		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Telekinesis Circle");
			Main.projFrames[projectile.type] = 1;
		}

        public override void SetDefaults()
        {
			projectile.width = 28;
			projectile.height = 46;
			projectile.tileCollide = true;
			projectile.timeLeft = 100;
		}

        public override bool? CanCutTiles()
        {
			return false;
        }

        public override void AI()
		{
			Vector2 mousePosition = Main.MouseWorld;
			projectile.Center = mousePosition;

			Player player = Main.player[projectile.owner];
			
			projectile.ai[0] += 1f;
			//if (Main.myPlayer == projectile.owner) projectile.netUpdate = true;
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

			projectile.spriteDirection = projectile.direction;
			projectile.timeLeft = 2;
			player.ChangeDir(projectile.direction);
			player.heldProj = projectile.whoAmI;
			player.itemTime = 2;
			player.itemAnimation = 2;
			player.itemRotation = (player.Center - projectile.Center).ToRotation();
			projectile.rotation = player.itemRotation - (float)Math.PI / 2;

			if (victim == null)
            {
				int sizeLimit = 50;
				foreach (NPC npc in Main.npc)
				{
					if (!npc.boss && npc.aiStyle != 6 && npc.width < sizeLimit && npc.height < sizeLimit && (npc.Center - mousePosition).Length() < 16f * 3 && Collision.CanHitLine(mousePosition, 1, 1, npc.position, npc.width, npc.height))
					{
						victim = npc;
						break;
					}
				}
			}
			else
            {
				if (WorldGen.SolidTile((int)(mousePosition.X / 16), (int)(mousePosition.Y / 16)) || !Collision.CanHitLine(mousePosition, 1, 1, victim.position, victim.width, victim.height)) projectile.Kill();
				else
				{
					victim.Center = projectile.Center;
					victim.velocity *= 0f;
				}
            }
		}

    }
}