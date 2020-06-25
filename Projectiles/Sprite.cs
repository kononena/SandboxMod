using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using SandboxMod.Projectiles;
using SandboxMod.Buffs;
using static Terraria.ModLoader.ModContent;
using System;

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
			projectile.width = 26;
			projectile.height = 44;
			projectile.tileCollide = true;
			projectile.light = 0;
		}

		//AI code adapted from decompiled vanilla code for the crimson heart
		//Some elements may be unnecessary, but this currently does the job
        public override void AI()
        {
			Player player = Main.player[projectile.owner];
			SandboxPlayer modPlayer = player.GetModPlayer<SandboxPlayer>();
			if (player.dead) modPlayer.spritePet = false;
			if (modPlayer.spritePet) projectile.timeLeft = 2;

			Vector2 minionPosition = player.Center;
			
			minionPosition.X -= (15 + player.width / 2) * player.direction;
			minionPosition.X -= 40 * player.direction;
			
			projectile.rotation += projectile.velocity.X / 20f;

			if (projectile.ai[0] == 1f)
			{
				projectile.tileCollide = false;
				float velocityNudge = 0.2f;
				float velocityFactor = 10f;
				if (velocityFactor < Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y))
				{
					velocityFactor = Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y);
				}
				Vector2 separation = player.Center - projectile.Center;
				float separationLength = separation.Length();
				if (separationLength > 2000f)
				{
					projectile.position = player.Center - new Vector2(projectile.width, projectile.height) / 2f;
				}
				if (separationLength < 200f && player.velocity.Y == 0f && projectile.position.Y + (float)projectile.height <= player.position.Y + (float)player.height && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
				{
					projectile.ai[0] = 0f;
					projectile.netUpdate = true;
					if (projectile.velocity.Y < -6f)
					{
						projectile.velocity.Y = -6f;
					}
				}
				if (!(separationLength < 60f))
				{
					separation.Normalize();
					separation *= velocityFactor;
					if (projectile.velocity.X < separation.X)
					{
						projectile.velocity.X += velocityNudge;
						if (projectile.velocity.X < 0f)
						{
							projectile.velocity.X += velocityNudge * 1.5f;
						}
					}
					if (projectile.velocity.X > separation.X)
					{
						projectile.velocity.X -= velocityNudge;
						if (projectile.velocity.X > 0f)
						{
							projectile.velocity.X -= velocityNudge * 1.5f;
						}
					}
					if (projectile.velocity.Y < separation.Y)
					{
						projectile.velocity.Y += velocityNudge;
						if (projectile.velocity.Y < 0f)
						{
							projectile.velocity.Y += velocityNudge * 1.5f;
						}
					}
					if (projectile.velocity.Y > separation.Y)
					{
						projectile.velocity.Y -= velocityNudge;
						if (projectile.velocity.Y > 0f)
						{
							projectile.velocity.Y -= velocityNudge * 1.5f;
						}
					}
				}
				if (projectile.velocity.X != 0f)
				{
					projectile.spriteDirection = Math.Sign(projectile.velocity.X);
				}
			}
			if (projectile.ai[0] == 2f)
			{
				projectile.friendly = true;
				projectile.spriteDirection = projectile.direction;
				projectile.rotation = 0f;
				projectile.velocity.Y += 0.4f;
				if (projectile.velocity.Y > 10f)
				{
					projectile.velocity.Y = 10f;
				}
				projectile.ai[1]--;
				if (projectile.ai[1] <= 0f)
				{
					projectile.ai[1] = 0f;
					projectile.ai[0] = 0f;
					projectile.friendly = false;
					projectile.netUpdate = true;
					return;
				}
			}
			if (projectile.ai[0] == 0f)
			{
				float num846 = 200f;
				if (Main.player[projectile.owner].rocketDelay2 > 0)
				{
					projectile.ai[0] = 1f;
					projectile.netUpdate = true;
				}
				Vector2 vector68 = player.Center - projectile.Center;
				if (vector68.Length() > 2000f)
				{
					projectile.position = player.Center - new Vector2(projectile.width, projectile.height) / 2f;
				}
				else if (vector68.Length() > num846 || Math.Abs(vector68.Y) > 300f)
				{
					projectile.ai[0] = 1f;
					projectile.netUpdate = true;
					if (projectile.velocity.Y > 0f && vector68.Y < 0f)
					{
						projectile.velocity.Y = 0f;
					}
					if (projectile.velocity.Y < 0f && vector68.Y > 0f)
					{
						projectile.velocity.Y = 0f;
					}
				}
			}
			if (projectile.ai[0] == 0f)
			{
				projectile.tileCollide = true;
				float num847 = 0.5f;
				float num848 = 4f;
				float num849 = 4f;
				float num850 = 0.1f;
				if (num849 < Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y))
				{
					num849 = Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y);
					num847 = 0.7f;
				}
				int num852 = 0;
				bool flag22 = false;
				float num853 = minionPosition.X - projectile.Center.X;
				if (Math.Abs(num853) > 5f)
				{
					if (num853 < 0f)
					{
						num852 = -1;
						if (projectile.velocity.X > 0f - num848)
						{
							projectile.velocity.X -= num847;
						}
						else
						{
							projectile.velocity.X -= num850;
						}
					}
					else
					{
						num852 = 1;
						if (projectile.velocity.X < num848)
						{
							projectile.velocity.X += num847;
						}
						else
						{
							projectile.velocity.X += num850;
						}
					}
					flag22 = true;
				}
				else
				{
					projectile.velocity.X *= 0.9f;
					if (Math.Abs(projectile.velocity.X) < num847 * 2f)
					{
						projectile.velocity.X = 0f;
					}
				}
				if (num852 != 0)
				{
					int num854 = (int)(projectile.position.X + (float)(projectile.width / 2)) / 16;
					int num855 = (int)projectile.position.Y / 16;
					num854 += num852;
					num854 += (int)projectile.velocity.X;
					for (int num856 = num855; num856 < num855 + projectile.height / 16 + 1; num856++)
					{
						if (WorldGen.SolidTile(num854, num856))
						{
							flag22 = true;
						}
					}
				}
				if (projectile.velocity.X != 0f)
				{
					flag22 = true;
				}
				Collision.StepUp(ref projectile.position, ref projectile.velocity, projectile.width, projectile.height, ref projectile.stepSpeed, ref projectile.gfxOffY);
				if (projectile.velocity.Y == 0f && flag22)
				{
					for (int num857 = 0; num857 < 3; num857++)
					{
						int num858 = (int)(projectile.position.X + (float)(projectile.width / 2)) / 16;
						if (num857 == 0)
						{
							num858 = (int)projectile.position.X / 16;
						}
						if (num857 == 2)
						{
							num858 = (int)(projectile.position.X + (float)projectile.width) / 16;
						}
						int num859 = (int)(projectile.position.Y + (float)projectile.height) / 16;
						if (!WorldGen.SolidTile(num858, num859) && !Main.tile[num858, num859].halfBrick() && Main.tile[num858, num859].slope() <= 0 && (!TileID.Sets.Platforms[Main.tile[num858, num859].type] || !Main.tile[num858, num859].active() || Main.tile[num858, num859].inActive()))
						{
							continue;
						}
						try
						{
							num858 = (int)(projectile.position.X + (float)(projectile.width / 2)) / 16;
							num859 = (int)(projectile.position.Y + (float)(projectile.height / 2)) / 16;
							num858 += num852;
							num858 += (int)projectile.velocity.X;
							if (!WorldGen.SolidTile(num858, num859 - 1) && !WorldGen.SolidTile(num858, num859 - 2))
							{
								projectile.velocity.Y = -5.1f;
							}
							else if (!WorldGen.SolidTile(num858, num859 - 2))
							{
								projectile.velocity.Y = -7.1f;
							}
							else if (WorldGen.SolidTile(num858, num859 - 5))
							{
								projectile.velocity.Y = -11.1f;
							}
							else if (WorldGen.SolidTile(num858, num859 - 4))
							{
								projectile.velocity.Y = -10.1f;
							}
							else
							{
								projectile.velocity.Y = -9.1f;
							}
						}
						catch
						{
							projectile.velocity.Y = -9.1f;
						}
					}
				}
				if (projectile.velocity.X > num849)
				{
					projectile.velocity.X = num849;
				}
				if (projectile.velocity.X < 0f - num849)
				{
					projectile.velocity.X = 0f - num849;
				}
				if (projectile.velocity.X < 0f)
				{
					projectile.direction = -1;
				}
				if (projectile.velocity.X > 0f)
				{
					projectile.direction = 1;
				}
				if (projectile.velocity.X > num847 && num852 == 1)
				{
					projectile.direction = 1;
				}
				if (projectile.velocity.X < 0f - num847 && num852 == -1)
				{
					projectile.direction = -1;
				}
				projectile.spriteDirection = projectile.direction;
				projectile.velocity.Y += 0.4f;
				if (projectile.velocity.Y > 10f)
				{
					projectile.velocity.Y = 10f;
				}
			}
		}
    }
}