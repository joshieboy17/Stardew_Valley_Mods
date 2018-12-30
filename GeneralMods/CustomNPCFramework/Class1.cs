using System;
using System.Collections.Generic;
using System.IO;
using CustomNPCFramework.Framework.Enums;
using CustomNPCFramework.Framework.Graphics;
using CustomNPCFramework.Framework.ModularNpcs.ColorCollections;
using CustomNPCFramework.Framework.NPCS;
using CustomNPCFramework.Framework.Utilities;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;

namespace CustomNPCFramework
{
    /// <summary>
    /// BETA VERSION 0.1.0: Lots of ways this can be improved upon.
    /// TODO:
    /// 
    ///
    /// List all asset managers in use.
    /// Have all asset managers list what assets they are using.
    /// 
    /// Have asset info have a var called age.
    ///     ...where 
    ///         0=adult
    ///         1=child
    /// 
    ///    /// Have asset info have a var called bodyType.
    ///     ...where 
    ///         0=thin
    ///         1=normal
    ///         2=muscular
    ///         3=big
    /// 
    /// Load in the assets and go go go.
    ///     -Collect a bunch of assets together to test this thing.
    ///     
    /// Find way to make sideways shirts render correctly.
    /// 
    ///Get suggestions from modding community on requests and ways to improve the mod. 
    /// </summary>
    public class Class1 : Mod
    {
        /// <summary>The mod helper for the mod.</summary>
        public static IModHelper ModHelper;

        /// <summary>The mod monitor for the mod.</summary>
        public static IMonitor ModMonitor;

        /// <summary>The npc tracker for the mod. Keeps track of all npcs added by the custom framework and cleans them up during saving.</summary>
        public static NpcTracker npcTracker;

        /// <summary>Keeps track of all of the asets/textures added in by the framework. Also manages all of the asset managers that are the ones actually managing the textures.</summary>
        public static AssetPool assetPool;

        public static IManifest Manifest;

        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            ModHelper = this.Helper;
            ModMonitor = this.Monitor;
            Manifest = this.ModManifest;

            StardewModdingAPI.Events.SaveEvents.AfterLoad += this.SaveEvents_LoadChar;

            StardewModdingAPI.Events.SaveEvents.BeforeSave += this.SaveEvents_BeforeSave;
            StardewModdingAPI.Events.SaveEvents.AfterSave += this.SaveEvents_AfterSave;

            StardewModdingAPI.Events.PlayerEvents.Warped += this.LocationEvents_CurrentLocationChanged;
            StardewModdingAPI.Events.GameEvents.UpdateTick += this.GameEvents_UpdateTick;
            npcTracker = new NpcTracker();
            assetPool = new AssetPool();
            var assetManager = new AssetManager();
            assetPool.addAssetManager(new KeyValuePair<string, AssetManager>("testNPC", assetManager));
            this.initializeExamples();
            this.initializeAssetPool();
            assetPool.loadAllAssets();
        }

        /// <summary>Initialize the asset pool with some test variables.</summary>
        public void initializeAssetPool()
        {
            string path = Path.Combine(ModHelper.DirectoryPath, "Content", "Graphics", "NPCS");
            assetPool.getAssetManager("testNPC").addPathCreateDirectory(new KeyValuePair<string, string>("characters", path));
        }

        /// <summary>A function that is called when the game finishes saving.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void SaveEvents_AfterSave(object sender, EventArgs e)
        {
            npcTracker.afterSave();
        }

        /// <summary>A function that is called when the game is about to load. Used to clean up all the npcs from the game world to prevent it from crashing.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void SaveEvents_BeforeSave(object sender, EventArgs e)
        {
            npcTracker.cleanUpBeforeSave();
        }

        /// <summary>Called upon 60 times a second. For testing purposes only. Will remove in future release.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void GameEvents_UpdateTick(object sender, EventArgs e)
        {
            /*
            if (Game1.player.currentLocation == null) return;
            if (Game1.activeClickableMenu != null) return;
            foreach (var v in Game1.player.currentLocation.characters)
            {
                v.speed = 1;
                if(v is ExtendedNPC)
                {
                    (v as ExtendedNPC).SetMovingAndMove(Game1.currentGameTime, Game1.viewport, Game1.player.currentLocation, Direction.right, true);
                }
                //v.MovePosition(Game1.currentGameTime, Game1.viewport, Game1.player.currentLocation);
                //ModMonitor.Log(v.sprite.spriteHeight.ToString());
            }
            */
        }

        /// <summary>Called when the player's location changes.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void LocationEvents_CurrentLocationChanged(object sender, StardewModdingAPI.Events.EventArgsPlayerWarped e) { }

        /// <summary>Used to spawn a custom npc just as an example. Don't keep this code. GENERATE NPC AND CALL THE CODE</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void SaveEvents_LoadChar(object sender, EventArgs e)
        {
            ExtendedNpc myNpc3 = assetPool.generateNPC(Genders.female, 0, 1, new StandardColorCollection(null, null, Color.Blue, null, Color.Yellow, null));
            MerchantNpc merch = new MerchantNpc(new List<Item>()
            {
                new StardewValley.Object(475,999)
            }, myNpc3);
            npcTracker.addNewNpcToLocation(Game1.getLocationFromName("BusStop", false), merch, new Vector2(2, 23));
        }

        /// <summary>Used to initialize examples for other modders to look at as reference.</summary>
        public void initializeExamples()
        {
            return;
            string dirPath = Path.Combine(ModHelper.DirectoryPath, "Content", "Templates");
            var aManager = assetPool.getAssetManager("testNPC");
            aManager.addPathCreateDirectory(new KeyValuePair<string, string>("templates", dirPath));
            string filePath = Path.Combine(dirPath, "Example.json");
            if (!File.Exists(filePath))
            {
                string getRelativePath = getShortenedDirectory(filePath);
                ModMonitor.Log("THIS IS THE PATH::: " + getRelativePath);
                AssetInfo info = new AssetInfo("MyExample", new NamePairings("StandingExampleL", "StandingExampleR", "StandingExampleU", "StandingExampleD"), new NamePairings("MovingExampleL", "MovingExampleR", "MovingExampleU", "MovingExampleD"), new NamePairings("SwimmingExampleL", "SwimmingExampleR", "SwimmingExampleU", "SwimmingExampleD"), new NamePairings("SittingExampleL", "SittingExampleR", "SittingExampleU", "SittingExampleD"), new Vector2(16, 16), false);
                info.writeToJson(filePath);

            }
            string filePath2 = Path.Combine(dirPath, "AdvancedExample.json");
            if (!File.Exists(filePath2))
            {

                ExtendedAssetInfo info2 = new ExtendedAssetInfo("AdvancedExample", new NamePairings("AdvancedStandingExampleL", "AdvancedStandingExampleR", "AdvancedStandingExampleU", "AdvancedStandingExampleD"), new NamePairings("AdvancedMovingExampleL", "AdvancedMovingExampleR", "AdvancedMovingExampleU", "AdvancedMovingExampleD"), new NamePairings("AdvancedSwimmingExampleL", "AdvancedSwimmingExampleR", "AdvancedSwimmingExampleU", "AdvancedSwimmingExampleD"), new NamePairings("AdvancedSittingExampleL", "AdvancedSittingExampleR", "AdvancedSittingExampleU", "AdvancedSittingExampleD"), new Vector2(16, 16), false, Genders.female, new List<Seasons>()
            {
                Seasons.spring,
                Seasons.summer
            }, PartType.hair
                );
                info2.writeToJson(filePath2);
            }
        }

        /// <summary>Used to splice the mod directory to get relative paths.</summary>
        public static string getShortenedDirectory(string path)
        {
            string lol = (string)path.Clone();
            string[] spliter = lol.Split(new string[] { ModHelper.DirectoryPath }, StringSplitOptions.None);
            try
            {
                return spliter[1];
            }
            catch
            {
                return spliter[0];
            }
        }

        /// <summary>Used to finish cleaning up absolute asset paths into a shortened relative path.</summary>
        public static string getRelativeDirectory(string path)
        {
            string s = getShortenedDirectory(path);
            return s.Remove(0, 1);
        }
    }
}
