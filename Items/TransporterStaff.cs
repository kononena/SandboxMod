using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Runtime.Remoting.Messaging;

namespace SandboxMod.Items
{
	public class TransporterStaff : ModItem
	{
		public override string Texture => "Terraria/Item_" + ItemID.SharkFin;

        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("Teleports you to the chosen transporter" +
				"Right-click to cycle between transporters" +
				"Left-click to teleport" +
				"Throw and pick up item to update world transporters");
        }

        public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.useTurn = true;
			item.autoReuse = false;
			item.useAnimation = 30;
			item.useTime = 30;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(gold: 1);
		}

        public override bool OnPickup(Player player)
        {
			var modPlayer = player.GetModPlayer<SandboxPlayer>();
			modPlayer.UpdateTransporters();
			if (modPlayer.transporterLocations.Count > 0) modPlayer.chosenTransporter = modPlayer.transporterLocations.Keys.First();
			return true;
        }

        public override bool AltFunctionUse(Player player) => true;

		public override bool UseItem(Player player)
		{
			var locations = player.GetModPlayer<SandboxPlayer>().transporterLocations;
			var choice = Main.LocalPlayer.GetModPlayer<SandboxPlayer>().chosenTransporter;
			if (locations.Count <= 0) return false;

			if (choice == (0, 0)) choice = locations.Keys.First();

			#region Choosing Transporter
			if (player.altFunctionUse == 2)
			{
				var transXY = locations.Keys.ToList();
				if (transXY.Contains(choice))
				{
					int index = transXY.IndexOf(choice);
					choice = transXY[index + 1 == transXY.Count ? 0 : index + 1];
				}
				else choice = transXY[0];

				Main.LocalPlayer.GetModPlayer<SandboxPlayer>().chosenTransporter = choice;
			}
			#endregion
			#region Teleport Somewhere
			else
			{
				if (locations.Count <= 0) return false;

				foreach ((int x, int y) in locations.Keys)
				{
					Tile tile = Main.tile[x, y];
					if (tile == null || tile.type != TileType<Tiles.Transporter>())
					{
						Main.NewText("Transporter removed. Update your choice.");
						return false;
					}
				}

				player.Teleport(new Vector2(Main.leftWorld + choice.x * 16, Main.topWorld + choice.y * 16 - 2 * 16));
			}
			#endregion
			return true;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			var locations = Main.LocalPlayer.GetModPlayer<SandboxPlayer>().transporterLocations;
			var choice = Main.LocalPlayer.GetModPlayer<SandboxPlayer>().chosenTransporter;


			if (locations.Count > 0)
			{
				if (!locations.ContainsKey(choice)) tooltips.Add(new TooltipLine(mod, "TransporterChoice", "No transporter selected"));
				else tooltips.Add(new TooltipLine(mod, "TransporterChoice", "Current transporter: " + locations[choice]));
			}
		}

		public override void AddRecipes()
        {
			ModRecipe rec = new ModRecipe(mod);
			rec.SetResult(this);
			rec.AddRecipe();
        }
    }
}