using System.Collections.Generic;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Menus;
using xTile;
using xTile.ObjectModel;
using xTile.Tiles;

namespace WitchPrincess
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            LocationEvents.CurrentLocationChanged += this.LocationEvents_CurrentLocationChanged;
            MenuEvents.MenuChanged += this.MenuEvents_MenuChanged;
        }


        /*********
        ** Private methods
        *********/
        /// <summary>The method called after the player enters a new location.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void LocationEvents_CurrentLocationChanged(object sender, EventArgsCurrentLocationChanged e)
        {
            if (e.NewLocation is FarmHouse && Game1.player.spouse == "Wizard")
                this.LoadSpouseRoom();
        }

        /// <summary>The method called after the current menu changes.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void MenuEvents_MenuChanged(object sender, EventArgsClickableMenuChanged e)
        {
            // check dialogue conditions
            var dialogue = e.NewMenu as DialogueBox;
            if (dialogue == null || Game1.currentSpeaker?.name != "Wizard" || Game1.CurrentEvent?.FestivalName == null)
                return;

            // check dialogue text
            var dialogueStr = dialogue?.getCurrentString();
            if (!dialogueStr.Contains("用") && !dialogueStr.Contains("...") && !dialogueStr.Contains("sí") && !dialogueStr.Contains("么") && !dialogueStr.Contains("の"))
                return;

            // check for flower festival
            var festivalData = this.Helper.Content.Load<Dictionary<string, string>>(@"Data\Festivals\spring24", ContentSource.GameContent);
            if (!festivalData.TryGetValue("name", out string festivalName) || Game1.CurrentEvent.FestivalName != festivalName)
                return;

            // swap dialogue
            Game1.activeClickableMenu = new DialogueBox(new Dialogue("hehehe..", Game1.getCharacterFromName("Wizard")));
            string text = this.Helper.Translation.Get("laugh");
            Game1.activeClickableMenu = new DialogueBox(new Dialogue(text, Game1.getCharacterFromName("Wizard")));
        }

        /// <summary>Add the witch princess' spouse room to the farmhouse.</summary>
        public void LoadSpouseRoom()
        {
            // get farmhouse
            FarmHouse farmhouse = (FarmHouse)Game1.getLocationFromName("FarmHouse");

            // load custom map
            Map map = this.Helper.Content.Load<Map>(@"Content\WitchRoom.xnb");
            TileSheet room = new TileSheet(farmhouse.map, this.Helper.Content.GetActualAssetKey(@"Content\SRWitch.xnb"), map.TileSheets[0].SheetSize, map.TileSheets[0].TileSize) { Id = "ZZZ-WIZARD-SPOUSE-ROOM" };
            farmhouse.map.AddTileSheet(room);
            farmhouse.map.LoadTileSheets(Game1.mapDisplayDevice);

            // patch farmhouse
            farmhouse.map.Properties.Remove("DayTiles");
            farmhouse.map.Properties.Remove("NightTiles");
            TileSheet roomForFarmhouse = farmhouse.map.TileSheets[farmhouse.map.TileSheets.IndexOf(room)];

            int num = 0;
            Point point = new Point(num % 5 * 6, num / 5 * 9);
            Rectangle staticTile = farmhouse.upgradeLevel == 1 ? new Rectangle(29, 1, 6, 9) : new Rectangle(35, 10, 6, 9);
            for (int i = 0; i < staticTile.Width; i++)
            {
                for (int j = 0; j < staticTile.Height; j++)
                {
                    if (map.GetLayer("Back").Tiles[point.X + i, point.Y + j] != null)
                        farmhouse.map.GetLayer("Back").Tiles[staticTile.X + i, staticTile.Y + j] = new StaticTile(farmhouse.map.GetLayer("Back"), roomForFarmhouse, BlendMode.Alpha, map.GetLayer("Back").Tiles[point.X + i, point.Y + j].TileIndex);
                    if (map.GetLayer("Buildings").Tiles[point.X + i, point.Y + j] == null)
                        farmhouse.map.GetLayer("Buildings").Tiles[staticTile.X + i, staticTile.Y + j] = null;
                    else
                        farmhouse.map.GetLayer("Buildings").Tiles[staticTile.X + i, staticTile.Y + j] = new StaticTile(farmhouse.map.GetLayer("Buildings"), roomForFarmhouse, BlendMode.Alpha, map.GetLayer("Buildings").Tiles[point.X + i, point.Y + j].TileIndex);
                    if (j < staticTile.Height - 1 && map.GetLayer("Front").Tiles[point.X + i, point.Y + j] != null)
                        farmhouse.map.GetLayer("Front").Tiles[staticTile.X + i, staticTile.Y + j] = new StaticTile(farmhouse.map.GetLayer("Front"), roomForFarmhouse, BlendMode.Alpha, map.GetLayer("Front").Tiles[point.X + i, point.Y + j].TileIndex);
                    else if (j < staticTile.Height - 1)
                        farmhouse.map.GetLayer("Front").Tiles[staticTile.X + i, staticTile.Y + j] = null;
                    if (i == 4 && j == 4)
                    {
                        try
                        {
                            KeyValuePair<string, PropertyValue> prop = new KeyValuePair<string, PropertyValue>("NoFurniture", new PropertyValue("T"));

                            farmhouse.map.GetLayer("Back").Tiles[staticTile.X + i, staticTile.Y + j].Properties.Add(prop);
                        }
                        catch
                        {
                            // ignore errors
                        }
                    }
                }
            }
        }
    }
}
