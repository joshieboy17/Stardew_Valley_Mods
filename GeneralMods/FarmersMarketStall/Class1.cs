using System;
using EventSystem.Framework.FunctionEvents;
using FarmersMarketStall.Framework.MapEvents;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;

namespace FarmersMarketStall
{
    /// <summary>
    /// TODO:
    /// Make a farmers market menu
    /// MAke a way to store items to sell in a sort of inventory
    /// Make a map event to call the farmers market stall menu
    /// Make way to sell market items at a higher value
    /// Make a selling menu
    /// Make a minigame event for bonus money to earn.
    /// </summary>
    public class Class1 : Mod
    {
        public static IModHelper ModHelper;
        public static IMonitor ModMonitor;
        public static Framework.MarketStall marketStall;

        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            ModHelper = this.Helper;
            ModMonitor = this.Monitor;

            StardewModdingAPI.Events.SaveEvents.BeforeSave += this.SaveEvents_BeforeSave;
            StardewModdingAPI.Events.SaveEvents.AfterLoad += this.SaveEvents_AfterLoad;
            marketStall = new Framework.MarketStall();
        }

        private void SaveEvents_AfterLoad(object sender, EventArgs e)
        {
            EventSystem.EventSystem.eventManager.addEvent(Game1.getLocationFromName("BusStop"), new ShopInteractionEvent("FarmersMarketStall", Game1.getLocationFromName("BusStop"), new Vector2(6, 11), new MouseButtonEvents(null, true), new MouseEntryLeaveEvent(null, null)));
        }

        private void SaveEvents_BeforeSave(object sender, EventArgs e)
        {
            if (marketStall.stock.Count > 0)
            {
                // Game1.endOfNightMenus.Push(new StardewValley.Menus.ShippingMenu(marketStall.stock));
                marketStall.sellAllItems();
            }
        }
    }
}
