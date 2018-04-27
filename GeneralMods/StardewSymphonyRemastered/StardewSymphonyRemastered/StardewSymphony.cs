﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Audio;
using StardewModdingAPI;
using StardewValley;
using StardewSymphonyRemastered.Framework;
using System.IO;
using StardustCore.UIUtilities;

namespace StardewSymphonyRemastered
{

    /// <summary>
    /// BIG WIP. Don't use this at all because it does nothing right now.
    /// TODO:
    /// -Make way to swap between album menu draw modes
    /// -make a currently playing menu off to the side to tell you what song is playing from what album.
    /// Make songs work for festivals and events.
    /// Finish making triggers menus
    /// 3.Make interface.
    /// 5.Release
    /// 6.Make videos documenting how to make this mod work.
    /// 7.Make way to generate new music packs.
    /// 
    /// 
    /// Add mod config to have silent rain option.
    /// Add in song delay function.
    /// Add in shuffle song button that just selects music but probably plays a different song. same as musicManager.selectmusic(getConditionalString);
    /// 
    /// 
    /// Notes:
    /// All mods must add events/locations/festivals/menu information to this mod during the Entry function of their mod because once the game does it's first update tick, that's when all of the packs are initialized with all of their music.
    /// </summary>
    public class StardewSymphony : Mod
    {
        public static WaveBank DefaultWaveBank;
        public static SoundBank DefaultSoundBank;


        public static IModHelper ModHelper;
        public static IMonitor ModMonitor;

        public static MusicManager musicManager;

        private string MusicPath;
        public static string WavMusicDirectory;
        public static string XACTMusicDirectory;
        public static string TemplateMusicDirectory;

        public bool musicPacksInitialized;



        public static bool festivalStart;
        public static bool eventStart;

        public static bool menuChangedMusic;



        public static TextureManager textureManager;
        /// <summary>
        /// Entry point for the mod.
        /// </summary>
        /// <param name="helper"></param>
        public override void Entry(IModHelper helper)
        {
            DefaultSoundBank = Game1.soundBank;
            DefaultWaveBank = Game1.waveBank;
            ModHelper = helper;
            ModMonitor = Monitor;

            StardewModdingAPI.Events.SaveEvents.AfterLoad += SaveEvents_AfterLoad;
            StardewModdingAPI.Events.LocationEvents.CurrentLocationChanged += LocationEvents_CurrentLocationChanged;
            StardewModdingAPI.Events.GameEvents.UpdateTick += GameEvents_UpdateTick;
            StardewModdingAPI.Events.ControlEvents.KeyPressed += ControlEvents_KeyPressed;
            StardewModdingAPI.Events.SaveEvents.BeforeSave += SaveEvents_BeforeSave;

            StardewModdingAPI.Events.MenuEvents.MenuChanged += MenuEvents_MenuChanged;
            StardewModdingAPI.Events.MenuEvents.MenuClosed += MenuEvents_MenuClosed;

            StardewModdingAPI.Events.GameEvents.FirstUpdateTick += GameEvents_FirstUpdateTick;


            musicManager = new MusicManager();

            MusicPath = Path.Combine(ModHelper.DirectoryPath, "Content", "Music");
            WavMusicDirectory = Path.Combine(MusicPath, "Wav");
            XACTMusicDirectory = Path.Combine(MusicPath, "XACT");
            TemplateMusicDirectory = Path.Combine(MusicPath, "Templates");


            textureManager = new TextureManager();
            this.createDirectories();
            this.createBlankXACTTemplate();
            this.createBlankWAVTemplate();

            musicPacksInitialized = false;
            menuChangedMusic = false;


            //Initialize all of the lists upon creation during entry.
            SongSpecifics.initializeMenuList();
            SongSpecifics.initializeEventsList();
            SongSpecifics.initializeFestivalsList();

            initializeMusicPacks();
        }

        /// <summary>
        /// Ran once all of teh entry methods are ran. This will ensure that all custom music from other mods has been properly loaded in.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameEvents_FirstUpdateTick(object sender, EventArgs e)
        {
            if (musicPacksInitialized == false)
            {
                musicManager.initializeMenuMusic(); //Initialize menu music that has been added to SongSpecifics.menus from all other mods during their Entry function.
                musicManager.initializeFestivalMusic();//Initialize festival music that has been added to SongSpecifics.menus from all other mods during their Entry function.
                musicManager.initializeEventMusic();//Initialize event music that has been added to SongSpecifics.menus from all other mods during their Entry function.
                //Note that locations should also be added to SongSpecifics.locations during the mod's respective Entry function.
                musicPacksInitialized = true;
                //musicManager.selectMusic(SongSpecifics.getCurrentConditionalString());
            }
        }

        /// <summary>
        /// Events to occur after the game has loaded in.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveEvents_AfterLoad(object sender, EventArgs e)
        {

            //Locaion initialization MUST occur after load. Anything else can occur before.
            SongSpecifics.initializeLocationsList(); //Gets all Game locations once the player has loaded the game, and all buildings on the player's farm and adds them to a location list.
            musicManager.initializeSeasonalMusic(); //Initialize the seasonal music using all locations gathered in the location list.

        }


        /// <summary>
        /// Choose new music when a menu is closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuEvents_MenuClosed(object sender, StardewModdingAPI.Events.EventArgsClickableMenuClosed e)
        {
            if (menuChangedMusic == true)
            {
                musicManager.selectMusic(SongSpecifics.getCurrentConditionalString());
            }
        }

        /// <summary>
        /// Choose new music when a menu is opened.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuEvents_MenuChanged(object sender, StardewModdingAPI.Events.EventArgsClickableMenuChanged e)
        {
            musicManager.selectMusic(SongSpecifics.getCurrentConditionalString());
        }

        private void SaveEvents_BeforeSave(object sender, EventArgs e)
        {
            /* THIS IS WAY TO LONG to run. Better make it save individual lists when I am editing songs.
            foreach(var musicPack in musicManager.musicPacks)
            {
                musicPack.Value.writeToJson();
            }
            */
        }

        /// <summary>
        /// Fires when a key is pressed to open the music selection menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlEvents_KeyPressed(object sender, StardewModdingAPI.Events.EventArgsKeyPressed e)
        {
            if (e.KeyPressed == Microsoft.Xna.Framework.Input.Keys.K)
            {
                Game1.activeClickableMenu = new Framework.Menus.MusicManagerMenu(Game1.viewport.Width,Game1.viewport.Height);
            }
        }


        /// <summary>
        /// Raised every frame. Mainly used just to initiate the music packs. Probably not needed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameEvents_UpdateTick(object sender, EventArgs e)
        {
            // ModMonitor.Log("HELLO WORLD");
            if (Game1.currentSong != null)
            {
                //ModMonitor.Log("STOP THE MUSIC!!!");
                Game1.currentSong.Stop(AudioStopOptions.Immediate); //stop the normal songs from playing over the new songs
                Game1.currentSong.Stop(AudioStopOptions.AsAuthored);
                Game1.nextMusicTrack = "";  //same as above line
            }
      
        }

        /// <summary>
        /// Raised when the player changes locations. This should determine the next song to play.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LocationEvents_CurrentLocationChanged(object sender, StardewModdingAPI.Events.EventArgsCurrentLocationChanged e)
        {
            musicManager.selectMusic(SongSpecifics.getCurrentConditionalString());
        }



        /// <summary>
        /// Load in the music packs to the music manager.
        /// </summary>
        public void initializeMusicPacks()
        {
            //load in all packs here.
            loadXACTMusicPacks();
            loadWAVMusicPacks();
        }

        /// <summary>
        /// Create the core directories needed by the mod.
        /// </summary>
        public void createDirectories()
        {
            string path = Path.Combine(ModHelper.DirectoryPath, "Content", "Graphics", "MusicMenu");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            string musicNote = Path.Combine(path, "MusicNote.png");
            string musicCD = Path.Combine(path, "MusicDisk.png");
            string outlineBox = Path.Combine(path, "OutlineBox.png");
            textureManager.addTexture("MusicNote",new Texture2DExtended(ModHelper,StardustCore.Utilities.getRelativeDirectory("StardewSymphonyRemastered", musicNote)));
            textureManager.addTexture("MusicDisk", new Texture2DExtended(ModHelper, StardustCore.Utilities.getRelativeDirectory("StardewSymphonyRemastered", musicCD)));
            textureManager.addTexture("MusicCD", new Texture2DExtended(ModHelper, StardustCore.Utilities.getRelativeDirectory("StardewSymphonyRemastered", musicCD)));
            textureManager.addTexture("OutlineBox", new Texture2DExtended(ModHelper, StardustCore.Utilities.getRelativeDirectory("StardewSymphonyRemastered", outlineBox)));

            if (!Directory.Exists(MusicPath)) Directory.CreateDirectory(MusicPath);
            if (!Directory.Exists(WavMusicDirectory)) Directory.CreateDirectory(WavMusicDirectory);
            if (!Directory.Exists(XACTMusicDirectory)) Directory.CreateDirectory(XACTMusicDirectory);
            if (!Directory.Exists(TemplateMusicDirectory)) Directory.CreateDirectory(TemplateMusicDirectory);
        }


        /// <summary>
        /// Used to create a blank XACT music pack example.
        /// </summary>
        public void createBlankXACTTemplate()
        {
            string path= Path.Combine(TemplateMusicDirectory, "XACT");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if(!File.Exists(Path.Combine(path, "MusicPackInformation.json"))){
                MusicPackMetaData blankMetaData = new MusicPackMetaData("Omegas's Music Data Example","Omegasis","Just a simple example of how metadata is formated for music packs. Feel free to copy and edit this one!","1.0.0 CoolExample","Icon.png");
                blankMetaData.writeToJson(Path.Combine(path, "MusicPackInformation.json"));
            }
            if (!File.Exists(Path.Combine(path, "readme.txt")))
            {
                string info = "Place the Wave Bank.xwb file and Sound Bank.xsb file you created in XACT in a similar directory in Content/Music/XACT/SoundPackName.\nModify MusicPackInformation.json as desire!\nRun the mod!";
                File.WriteAllText(Path.Combine(path, "readme.txt"),info);
            }
        }

        /// <summary>
        /// USed to create a blank WAV music pack example.
        /// </summary>
        public void createBlankWAVTemplate()
        {
            string path = Path.Combine(TemplateMusicDirectory, "WAV");
            string pathSongs = Path.Combine(path, "Songs");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!Directory.Exists(pathSongs))
            {
                Directory.CreateDirectory(pathSongs);
            }
            if (!File.Exists(Path.Combine(path, "MusicPackInformation.json")))
            {
                MusicPackMetaData blankMetaData = new MusicPackMetaData("Omegas's Music Data Example", "Omegasis", "Just a simple example of how metadata is formated for music packs. Feel free to copy and edit this one!", "1.0.0 CoolExample","Icon");
                blankMetaData.writeToJson(Path.Combine(path, "MusicPackInformation.json"));
            }
            if (!File.Exists(Path.Combine(path, "readme.txt")))
            {
                string info = "Place the .wav song files in the Songs folder, modify the MusicPackInformation.json as desired, and then run!";
                File.WriteAllText(Path.Combine(path, "readme.txt"), info);
            }
        }

        /// <summary>
        /// Load in the XACT music packs.
        /// </summary>
        public static void loadXACTMusicPacks()
        {
            string[] listOfDirectories= Directory.GetDirectories(XACTMusicDirectory);
            foreach(string folder in listOfDirectories)
            {
                //This chunk essentially allows people to name .xwb and .xsb files whatever they want.
                string[] xwb=Directory.GetFiles(folder, "*.xwb");
                string[] xsb = Directory.GetFiles(folder, "*.xsb");

                string[] debug = Directory.GetFiles(folder);
                if (xwb.Length == 0)
                {
                    ModMonitor.Log("Error loading in attempting to load music pack from: " + folder + ". There is no wave bank music file: .xwb located in this directory. AKA there is no valid music here.", LogLevel.Error);
                    return;
                }
                if (xwb.Length >= 2)
                {
                    ModMonitor.Log("Error loading in attempting to load music pack from: " + folder + ". There are too many wave bank music files or .xwbs located in this directory. Please ensure that there is only one music pack in this folder. You can make another music pack but putting a wave bank file in a different folder.", LogLevel.Error);
                    return;
                }

                if (xsb.Length == 0)
                {
                    ModMonitor.Log("Error loading in attempting to load music pack from: " + folder + ". There is no sound bank music file: .xsb located in this directory. AKA there is no valid music here.", LogLevel.Error);
                    return;
                }
                if (xsb.Length >= 2)
                {
                    ModMonitor.Log("Error loading in attempting to load music pack from: " + folder + ". There are too many sound bank music files or .xsbs located in this directory. Please ensure that there is only one sound reference file in this folder. You can make another music pack but putting a sound file in a different folder.", LogLevel.Error);
                    return;
                }

                string waveBank = xwb[0];
                string soundBank = xsb[0];
                string metaData = Path.Combine(folder, "MusicPackInformation.json");

                if (!File.Exists(metaData))
                {
                    ModMonitor.Log("WARNING! Loading in a music pack from: " + folder + ". There is no MusicPackInformation.json associated with this music pack meaning that while songs can be played from this pack, no information about it will be displayed.", LogLevel.Error);
                }
                StardewSymphonyRemastered.Framework.XACTMusicPack musicPack = new XACTMusicPack(folder, waveBank,soundBank);
                musicManager.addMusicPack(musicPack,true,true);
                
                musicPack.readFromJson();
                
            }
        }

        /// <summary>
        /// Load in WAV music packs.
        /// </summary>
        public static void loadWAVMusicPacks()
        {
            string[] listOfDirectories = Directory.GetDirectories(WavMusicDirectory);
            foreach (string folder in listOfDirectories)
            {
                string metaData = Path.Combine(folder, "MusicPackInformation.json");

                if (!File.Exists(metaData))
                {
                    ModMonitor.Log("WARNING! Loading in a music pack from: " + folder + ". There is no MusicPackInformation.json associated with this music pack meaning that while songs can be played from this pack, no information about it will be displayed.", LogLevel.Error);
                }

                StardewSymphonyRemastered.Framework.WavMusicPack musicPack = new WavMusicPack(folder);
                musicManager.addMusicPack(musicPack,true,true);
                
                musicPack.readFromJson();
                
            }
        }


        /// <summary>
        /// Reset the music files for the game.
        /// </summary>
        public static void Reset()
        {
            Game1.waveBank = DefaultWaveBank;
            Game1.soundBank = DefaultSoundBank;
        }

        /// <summary>
        /// Used to splice the mod directory to get relative paths.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string getShortenedDirectory(string path)
        {
            string lol = (string)path.Clone();
            string[] spliter = lol.Split(new string[] { ModHelper.DirectoryPath }, StringSplitOptions.None);
            try
            {
                return spliter[1];
            }
            catch (Exception err)
            {
                return spliter[0];
            }
        }

        /// <summary>
        /// Used to finish cleaning up absolute asset paths into a shortened relative path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string getRelativeDirectory(string path)
        {
            string s = getShortenedDirectory(path);
            return s.Remove(0, 1);
        }

    }
}
