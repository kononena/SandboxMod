using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;

namespace SandboxMod.Tiles
{
	public class SickFireBro2 : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.Height = 1;
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.CoordinateHeights = new[] { 16 };
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.addTile(Type);
			AddMapEntry(Color.Blue);
		}

        public override bool NewRightClick(int i, int j)
        {
			Main.LocalPlayer.statLifeMax2 += 69;
			Main.LocalPlayer.statLife += 69;
			return true;
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
			Main.LocalPlayer.AddBuff(BuffID.OnFire, 119, true);
        }
    }
}