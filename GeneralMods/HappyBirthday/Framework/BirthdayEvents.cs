using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Omegasis.HappyBirthday.Framework.EventPreconditions;
using StardustCore.Events;
using StardewValley;
using Microsoft.Xna.Framework;
using StardustCore.IlluminateFramework;
using StardustCore.Utilities;
using Omegasis.HappyBirthday.Framework.Constants;
using Omegasis.HappyBirthday.Framework.Utilities;
using Omegasis.StardustCore.Events.Preconditions.TimeSpecific;
using Omegasis.StardustCore.Events.Preconditions;
using Omegasis.StardustCore.Events.Preconditions.PlayerSpecific;
using Omegasis.StardustCore.Events.Preconditions.NPCSpecific;

namespace Omegasis.HappyBirthday.Framework
{
    public class BirthdayEvents
    {

        /// <summary>
        /// Creates the junimo birthday party event.
        /// </summary>
        /// <returns></returns>
        public static EventHelper CommunityCenterJunimoBirthday()
        {
            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new GameLocationPrecondition(Game1.getLocationFromName("CommunityCenter")));
            conditions.Add(new TimeOfDayPrecondition(600, 2600));


            conditions.Add(new CanReadJunimoEventPrecondition());
            conditions.Add(new IsJojaMemberEventPrecondition(false));


            //conditions.Add(new HasUnlockedCommunityCenter()); //Infered by the fact that you must enter the community center to trigger this event anyways.
            EventHelper e = new EventHelper(EventIds.JunimoCommunityCenterBirthday, 19950, conditions, new EventStartData("playful", 32, 12, new EventStartData.FarmerData(32, 22, EventHelper.FacingDirection.Up), new List<EventStartData.NPCData>()));

            e.AddInJunimoActor("Juni", new Microsoft.Xna.Framework.Vector2(32, 10), Colors.getRandomJunimoColor());
            e.AddInJunimoActor("Juni2", new Microsoft.Xna.Framework.Vector2(30, 11), Colors.getRandomJunimoColor());
            e.AddInJunimoActor("Juni3", new Microsoft.Xna.Framework.Vector2(34, 11), Colors.getRandomJunimoColor());
            e.AddInJunimoActor("Juni4", new Microsoft.Xna.Framework.Vector2(26, 11), Colors.getRandomJunimoColor());
            e.AddInJunimoActor("Juni5", new Microsoft.Xna.Framework.Vector2(28, 11), Colors.getRandomJunimoColor());
            e.AddInJunimoActor("Juni6Tank", new Vector2(38, 10), Colors.getRandomJunimoColor());
            e.AddInJunimoActor("Juni7", new Vector2(27, 16), Colors.getRandomJunimoColor());
            e.AddInJunimoActor("Juni8", new Vector2(40, 15), Colors.getRandomJunimoColor());
            e.AddJunimoAdvanceMoveTiles(new JunimoAdvanceMoveData("Juni6Tank", new List<Point>()
            {
                new Point(38,10),
                new Point(38,11),
                new Point(39,11),
                new Point(40,11),
                new Point(41,11),
                new Point(42,11),
                new Point(42,10),
                new Point(41,10),
                new Point(40,10),
                new Point(39,10),

            }, 60, 1, true)); ;

            e.FlipJunimoActor("Juni5", true);
            e.junimoFaceDirection("Juni4", EventHelper.FacingDirection.Right); //Make a junimo face right.
            e.junimoFaceDirection("Juni5", EventHelper.FacingDirection.Left);
            e.junimoFaceDirection("Juni7", EventHelper.FacingDirection.Down);
            e.animate("Juni", true, true, 250, new List<int>()
            {
                28,
                29,
                30,
                31
            });
            e.animate("Juni7", false, true, 250, new List<int>()
            {
                44,45,46,47
            });
            e.animate("Juni8", false, true, 250, new List<int>()
            {
                12,13,14,15
            });

            e.globalFadeIn();

            e.moveFarmerUp(10, EventHelper.FacingDirection.Up, true);

            e.junimoFaceDirection("Juni4", EventHelper.FacingDirection.Down);
            e.junimoFaceDirection("Juni5", EventHelper.FacingDirection.Down);
            e.RemoveJunimoAdvanceMove("Juni6Tank");
            e.junimoFaceDirection("Juni6Tank", EventHelper.FacingDirection.Down);
            e.junimoFaceDirection("Juni7", EventHelper.FacingDirection.Right);
            e.FlipJunimoActor("Juni8", true);
            e.junimoFaceDirection("Juni8", EventHelper.FacingDirection.Left);

            e.playSound("junimoMeep1");

            e.emoteFarmer_ExclamationMark();
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"JunimoBirthdayParty_0");
            e.emoteFarmer_Heart();
            e.globalFadeOut(0.010);
            e.setViewportPosition(-100, -100);
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"JunimoBirthdayParty_1");
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"PartyOver");
            e.addObjectToPlayersInventory(220, 1, false);

            e.end();

            return e;
        }


        /// <summary>
        /// Birthday event for when the player is dating Penny.
        /// Status: Completed.
        /// </summary>
        /// <returns></returns>
        public static EventHelper DatingBirthday_Penny()
        {

            NPC penny = Game1.getCharacterFromName("Penny");
            NPC pam = Game1.getCharacterFromName("Pam");

            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new GameLocationPrecondition(Game1.getLocationFromName("Trailer")));
            conditions.Add(new TimeOfDayPrecondition(600, 2600));
            conditions.Add(new DatingNPCEventPrecondition(penny));

            //conditions.Add(new StardustCore.Events.Preconditions.NPCSpecific.DatingNPC(Game1.getCharacterFromName("Penny"));
            EventHelper e = new EventHelper(EventIds.BirthdayDatingPennyTrailer, 19951, conditions, new EventStartData("playful", 12, 8, new EventStartData.FarmerData(12, 9, EventHelper.FacingDirection.Up), new List<EventStartData.NPCData>() {
                new EventStartData.NPCData(penny,12,7, EventHelper.FacingDirection.Up),
                new EventStartData.NPCData(pam,15,4, EventHelper.FacingDirection.Down)
            }));

            e.globalFadeIn();

            e.moveFarmerUp(1, EventHelper.FacingDirection.Up, false);

            e.actorFaceDirection("Penny", EventHelper.FacingDirection.Down);
            //starting = starting.Replace("@", Game1.player.Name);
            e.speak(penny, GetEventString("DatingPennyBirthday_Penny:0"));
            e.speak(pam, GetEventString("DatingPennyBirthday_Pam:0"));
            e.speak(penny, GetEventString("DatingPennyBirthday_Penny:1"));
            e.speak(pam, GetEventString("DatingPennyBirthday_Pam:1"));
            e.emote_Angry("Penny");
            e.speak(penny, GetEventString("DatingPennyBirthday_Penny:2")); //penny2
            e.speak(penny, GetEventString("DatingPennyBirthday_Penny:3")); //penny3

            e.moveActorLeft("Penny", 3, EventHelper.FacingDirection.Up, true);
            e.moveFarmerRight(2, EventHelper.FacingDirection.Up, false);
            e.moveFarmerUp(3, EventHelper.FacingDirection.Down, false);
            e.moveActorRight("Penny", 5, EventHelper.FacingDirection.Up, true);
            e.moveActorUp("Penny", 1, EventHelper.FacingDirection.Up, true);
            e.speak(pam, GetEventString("DatingPennyBirthday_Pam:2")); //pam2
            e.speak(penny, GetEventString("DatingPennyBirthday_Penny:4"));//penny4

            e.emoteFarmer_Heart();
            e.emote_Heart("Penny");
            e.globalFadeOut(0.010);
            e.setViewportPosition(-100, -100);
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"DatingPennyBirthday_Finish:0"); //penny party finish 0
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"DatingPennyBirthday_Finish:1");// penny party finish 1
            e.addObjectToPlayersInventory(220, 1, false);
            e.addObjectToPlayersInventory(346, 1, false);

            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"PartyOver");

            e.end();

            return e;
        }

        public static EventHelper DatingBirthday_Penny_BigHome()
        {

            NPC penny = Game1.getCharacterFromName("Penny");
            NPC pam = Game1.getCharacterFromName("Pam");

            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new GameLocationPrecondition(Game1.getLocationFromName("Trailer_Big")));
            conditions.Add(new TimeOfDayPrecondition(600, 2600));
            conditions.Add(new DatingNPCEventPrecondition(penny));

            //conditions.Add(new StardustCore.Events.Preconditions.NPCSpecific.DatingNPC(Game1.getCharacterFromName("Penny"));
            EventHelper e = new EventHelper(EventIds.BirthdayDatingPennyHouse, 19951, conditions, new EventStartData("playful", 14, 8, new EventStartData.FarmerData(12, 11, EventHelper.FacingDirection.Up), new List<EventStartData.NPCData>() {
                new EventStartData.NPCData(penny,12,7, EventHelper.FacingDirection.Up),
                new EventStartData.NPCData(pam,15,4, EventHelper.FacingDirection.Down)
            }));

            e.globalFadeIn();

            e.moveFarmerUp(3, EventHelper.FacingDirection.Up, false);

            e.actorFaceDirection("Penny", EventHelper.FacingDirection.Down);
            //starting = starting.Replace("@", Game1.player.Name);
            e.speak(penny, GetEventString("DatingPennyBirthday_Penny:0"));
            e.speak(pam, GetEventString("DatingPennyBirthday_Pam:0"));
            e.speak(penny, GetEventString("DatingPennyBirthday_Penny:1"));
            e.speak(pam, GetEventString("DatingPennyBirthday_Pam:1"));
            e.emote_Angry("Penny");
            e.speak(penny, GetEventString("DatingPennyBirthday_Penny:2")); //penny2
            e.speak(penny, GetEventString("DatingPennyBirthday_Penny:3")); //penny3

            e.moveActorLeft("Penny", 3, EventHelper.FacingDirection.Up, true);
            e.moveFarmerRight(2, EventHelper.FacingDirection.Up, false);
            e.moveFarmerUp(3, EventHelper.FacingDirection.Down, false);
            e.moveActorRight("Penny", 5, EventHelper.FacingDirection.Up, true);
            e.moveActorUp("Penny", 1, EventHelper.FacingDirection.Up, true);
            e.speak(pam, GetEventString("DatingPennyBirthday_Pam:2")); //pam2
            e.speak(penny, GetEventString("DatingPennyBirthday_Penny:4"));//penny4

            e.emoteFarmer_Heart();
            e.emote_Heart("Penny");
            e.globalFadeOut(0.010);
            e.setViewportPosition(-100, -100);
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"DatingPennyBirthday_Finish:0"); //penny party finish 0
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"DatingPennyBirthday_Finish:1");// penny party finish 1
            e.addObjectToPlayersInventory(220, 1, false);
            e.addObjectToPlayersInventory(346, 1, false);

            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"PartyOver");

            e.end();

            return e;
        }

        /// <summary>
        /// Birthday event for when the player is dating Maru.
        /// Finished.
        /// </summary>
        /// <returns></returns>
        public static EventHelper DatingBirthday_Maru()
        {
            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new GameLocationPrecondition(Game1.getLocationFromName("ScienceHouse")));
            conditions.Add(new TimeOfDayPrecondition(600, 2600));

            NPC maru = Game1.getCharacterFromName("Maru");
            NPC sebastian = Game1.getCharacterFromName("Sebastian");
            NPC robin = Game1.getCharacterFromName("Robin");
            NPC demetrius = Game1.getCharacterFromName("Demetrius");

            conditions.Add(new DatingNPCEventPrecondition(maru));

            EventHelper e = new EventHelper(EventIds.BirthdayDatingMaru, 19952, conditions, new EventStartData("playful", 28, 12, new EventStartData.FarmerData(23, 12, EventHelper.FacingDirection.Right), new List<EventStartData.NPCData>() {
                new EventStartData.NPCData(maru,27,11, EventHelper.FacingDirection.Down),
                new EventStartData.NPCData(sebastian,26,13, EventHelper.FacingDirection.Up),
                new EventStartData.NPCData(robin,28,9, EventHelper.FacingDirection.Up),
                new EventStartData.NPCData(demetrius,30,11, EventHelper.FacingDirection.Left)
            }));
            e.globalFadeIn();

            e.moveFarmerRight(3, EventHelper.FacingDirection.Right, true);
            e.npcFaceDirection(maru, EventHelper.FacingDirection.Left);
            e.npcFaceDirection(demetrius, EventHelper.FacingDirection.Left);
            //Seb is already facing up.
            e.npcFaceDirection(robin, EventHelper.FacingDirection.Down);

            //Dialogue goes here.
            //Seriously improve dialogue lines. Maru is probably the NPC I know the least about.
            e.speak(maru, GetEventString("DatingMaruBirthday_Maru:0")); //maru 0
            e.speak(demetrius, GetEventString("DatingMaruBirthday_Demetrius:0")); //demetrius 0
            e.speak(maru, GetEventString("DatingMaruBirthday_Maru:1"));//Maru 1 //Spoiler she doesn't.
            e.speak(sebastian, GetEventString("DatingMaruBirthday_Sebastian:0")); //sebastian 0
            e.speak(robin, GetEventString("DatingMaruBirthday_Robin:0")); //robin 0
            e.speak(demetrius, GetEventString("DatingMaruBirthday_Demetrius:1")); //demetrius 1
            e.emote_ExclamationMark("Robin");
            e.npcFaceDirection(robin, EventHelper.FacingDirection.Up);
            e.speak(robin, GetEventString("DatingMaruBirthday_Robin:1")); //robin 1
            e.npcFaceDirection(robin, EventHelper.FacingDirection.Down);
            e.moveActorDown("Robin", 1, EventHelper.FacingDirection.Down, false);
            e.addObject(27, 12, 220);

            e.speak(maru, GetEventString("DatingMaruBirthday_Maru:2")); //maru 2
            e.emoteFarmer_Thinking();
            e.speak(sebastian, GetEventString("DatingMaruBirthday_Sebastian:1")); //Sebastian 1
            e.speak(maru, GetEventString("DatingMaruBirthday_Maru:3")); //maru 3

            //Event finish commands.
            e.emoteFarmer_Heart();
            e.emote_Heart("Maru");
            e.globalFadeOut(0.010);
            e.setViewportPosition(-100, -100);
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"DatingMaruBirthday_Finish:0"); //maru party finish 0
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"DatingMaruBirthday_Finish:1"); //maru party finish 0
            e.addObjectToPlayersInventory(220, 1, false);

            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"PartyOver");
            e.end();
            return e;
        }

        /// <summary>
        /// Birthday event for when the player is dating Leah.
        /// Finished.
        /// </summary>
        /// <returns></returns>
        public static EventHelper DatingBirthday_Leah()
        {
            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new GameLocationPrecondition(Game1.getLocationFromName("LeahHouse")));
            conditions.Add(new TimeOfDayPrecondition(600, 2600));

            NPC leah = Game1.getCharacterFromName("Leah");

            conditions.Add(new DatingNPCEventPrecondition(leah));

            EventHelper e = new EventHelper(EventIds.BirthdayDatingLeah, 19954, conditions, new EventStartData("playful", 12, 7, new EventStartData.FarmerData(7, 9, EventHelper.FacingDirection.Up), new List<EventStartData.NPCData>() {
                new EventStartData.NPCData(leah,14,11, EventHelper.FacingDirection.Left),
            }));
            e.addObject(11, 11, 220);
            e.globalFadeIn();
            e.moveFarmerUp(2, EventHelper.FacingDirection.Up, false);
            e.moveFarmerRight(5, EventHelper.FacingDirection.Down, false);
            e.npcFaceDirection(leah, EventHelper.FacingDirection.Up);
            e.speak(leah, GetEventString("DatingLeahBirthday_Leah:0")); //0
            e.moveFarmerDown(2, EventHelper.FacingDirection.Down, false);
            e.moveFarmerRight(1, EventHelper.FacingDirection.Down, false);
            e.moveFarmerDown(1, EventHelper.FacingDirection.Down, false);
            e.speak(leah, GetEventString("DatingLeahBirthday_Leah:1")); //1
            e.emoteFarmer_Happy();
            e.speak(leah, GetEventString("DatingLeahBirthday_Leah:2"));//2
            e.speak(leah, GetEventString("DatingLeahBirthday_Leah:3"));//3
            e.speak(leah, GetEventString("DatingLeahBirthday_Leah:4"));//4


            e.emoteFarmer_Heart();
            e.emote_Heart("Leah");
            e.globalFadeOut(0.010);
            e.setViewportPosition(-100, -100);
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"DatingLeahBirthday_Finish:0"); //maru party finish 0
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"DatingLeahBirthday_Finish:1"); //maru party finish 0
            e.addObjectToPlayersInventory(220, 1, false);
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"PartyOver");
            e.end();
            return e;
        }

        /// <summary>
        /// Birthday event for when the player is dating Abigail.
        /// Finished.
        /// </summary>
        /// <returns></returns>
        public static EventHelper DatingBirthday_Abigail_Seedshop()
        {
            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new GameLocationPrecondition(Game1.getLocationFromName("SeedShop")));
            conditions.Add(new TimeOfDayPrecondition(600, 2600));

            if (Game1.player.hasCompletedCommunityCenter() == false)
            {
                conditions.Add(new DayOfWeekPrecondition(true, true, true, false, true, true, true));
            }

            NPC abigail = Game1.getCharacterFromName("Abigail");
            NPC pierre = Game1.getCharacterFromName("Pierre");
            NPC caroline = Game1.getCharacterFromName("Caroline");

            conditions.Add(new DatingNPCEventPrecondition(abigail));

            EventHelper e = new EventHelper(EventIds.BirthdayDatingAbigailSeedShop, 19955, conditions, new EventStartData("playful", 35, 7, new EventStartData.FarmerData(31, 11, EventHelper.FacingDirection.Up), new List<EventStartData.NPCData>() {
                new EventStartData.NPCData(abigail,36,9, EventHelper.FacingDirection.Left),
                new EventStartData.NPCData(pierre,33,6, EventHelper.FacingDirection.Down),
                new EventStartData.NPCData(caroline,35,5, EventHelper.FacingDirection.Up),
            }));
            e.globalFadeIn();

            //Dialogue here.
            e.moveFarmerUp(2, EventHelper.FacingDirection.Right, false);
            e.moveFarmerRight(4, EventHelper.FacingDirection.Right, false);

            e.speak(abigail, GetEventString("DatingAbigailBirthday_Abigail:0")); //abi 0

            e.npcFaceDirection(caroline, EventHelper.FacingDirection.Down);

            e.speak(pierre, GetEventString("DatingAbigailBirthday_Pierre:0")); //pie 0
            e.speak(caroline, GetEventString("DatingAbigailBirthday_Caroline:0")); //car 0
            e.addObject(35, 5, 220);
            e.speak(abigail, GetEventString("DatingAbigailBirthday_Abigail:1")); //abi 1
            e.speak(pierre, GetEventString("DatingAbigailBirthday_Pierre:1")); //pie 1
            e.speak(caroline, GetEventString("DatingAbigailBirthday_Caroline:1")); //car 1
            e.speak(caroline, GetEventString("DatingAbigailBirthday_Caroline:2")); //car 2
            e.speak(abigail, GetEventString("DatingAbigailBirthday_Abigail:2")); //abi 2
            e.emoteFarmer_Thinking();
            e.speak(abigail, GetEventString("DatingAbigailBirthday_Abigail:3"));//abi 3
            e.speak(abigail, GetEventString("DatingAbigailBirthday_Abigail:4"));///abi 4

            e.emoteFarmer_Heart();
            e.emote_Heart("Abigail");
            e.globalFadeOut(0.010);
            e.setViewportPosition(-100, -100);
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"DatingAbigailBirthday_Finish:0"); //abi party finish 0
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"DatingAbigailBirthday_Finish:1"); //abi party finish 0
            e.addObjectToPlayersInventory(220, 1, false);
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"PartyOver");
            e.end();
            return e;

        }


        public static EventHelper DatingBirthday_Abigail_Mine()
        {
            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new GameLocationPrecondition(Game1.getLocationFromName("Mine")));
            conditions.Add(new TimeOfDayPrecondition(600, 2600));

            var v=new IsJojaMemberEventPrecondition(true);
            if (v.meetsCondition())
            {
                conditions.Add(new DayOfWeekPrecondition(false, false, false, true, false, false, false));
            }
            else
            {
                if (Game1.player.hasCompletedCommunityCenter() == false)
                {
                    conditions.Add(new DayOfWeekPrecondition(false, false, false, true, false, false, false));
                }
            }

            NPC abigail = Game1.getCharacterFromName("Abigail");

            conditions.Add(new DatingNPCEventPrecondition(abigail));

            EventHelper e = new EventHelper(EventIds.BirthdayDatingAbigailMines, 19955, conditions, new EventStartData("playful", 18, 8, new EventStartData.FarmerData(18, 12, EventHelper.FacingDirection.Up), new List<EventStartData.NPCData>() {
                new EventStartData.NPCData(abigail,18,4, EventHelper.FacingDirection.Down),
            }));
            e.globalFadeIn();

            //Dialogue here.
            e.moveFarmerUp(7, EventHelper.FacingDirection.Up, false);

            e.speak(abigail, GetEventString("DatingAbigailBirthday_Mine_Abigail:0")); //abi 0

            e.speak(abigail, GetEventString("DatingAbigailBirthday_Mine_Abigail:1")); //abi 1
            e.emoteFarmer_QuestionMark();
            e.speak(abigail, GetEventString("DatingAbigailBirthday_Mine_Abigail:2")); //abi 2
            e.speak(abigail, GetEventString("DatingAbigailBirthday_Mine_Abigail:3"));//abi 3
            e.emoteFarmer_Thinking();
            e.speak(abigail, GetEventString("DatingAbigailBirthday_Mine_Abigail:4"));///abi 4

            e.emoteFarmer_Heart();
            e.emote_Heart("Abigail");
            e.globalFadeOut(0.010);
            e.setViewportPosition(-100, -100);
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"DatingAbigailBirthday_Mine_Finish:0"); //abi party finish 0
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"DatingAbigailBirthday_Mine_Finish:1"); //abi party finish 0
            e.addObjectToPlayersInventory(220, 1, false);
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"PartyOver");
            e.end();
            return e;

        }

        public static EventHelper DatingBirthday_Emily()
        {
            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new GameLocationPrecondition(Game1.getLocationFromName("HaleyHouse")));
            conditions.Add(new TimeOfDayPrecondition(600, 2600));

            NPC emily = Game1.getCharacterFromName("Emily");

            conditions.Add(new DatingNPCEventPrecondition(emily));

            EventHelper e = new EventHelper(EventIds.BirthdayDatingEmily, 19956, conditions, new EventStartData("playful", 20, 18, new EventStartData.FarmerData(11, 20, EventHelper.FacingDirection.Right), new List<EventStartData.NPCData>() {
                new EventStartData.NPCData(emily,20,17, EventHelper.FacingDirection.Down),
            }));
            e.globalFadeIn();

            //Dialogue here.
            e.moveFarmerRight(9, EventHelper.FacingDirection.Up, false);

            e.speak(emily, GetEventString("DatingEmilyBirthday_Emily:0")); //emi 0
            e.speak(emily, GetEventString("DatingEmilyBirthday_Emily:1")); //emi 0
            e.emoteFarmer_Happy();
            e.speak(emily, GetEventString("DatingEmilyBirthday_Emily:2")); //emi 0
            e.speak(emily, GetEventString("DatingEmilyBirthday_Emily:3")); //emi 0
            e.speak(emily, GetEventString("DatingEmilyBirthday_Emily:4")); //emi 0
            e.emoteFarmer_Thinking();
            e.speak(emily, GetEventString("DatingEmilyBirthday_Emily:5")); //emi 0


            e.emoteFarmer_Heart();
            e.emote_Heart("Emily");
            e.globalFadeOut(0.010);
            e.setViewportPosition(-100, -100);
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"DatingEmilyBirthday_Finish:0"); //abi party finish 0
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"DatingEmilyBirthday_Finish:1"); //abi party finish 0
            e.addObjectToPlayersInventory(220, 1, false);
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"PartyOver");
            e.end();
            return e;
        }


        public static EventHelper DatingBirthday_Haley()
        {

            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new GameLocationPrecondition(Game1.getLocationFromName("HaleyHouse")));
            conditions.Add(new TimeOfDayPrecondition(600, 2600));

            NPC haley = Game1.getCharacterFromName("Haley");

            conditions.Add(new DatingNPCEventPrecondition(haley));

            EventHelper e = new EventHelper(EventIds.BirthdayDatingHaley, 19957, conditions, new EventStartData("playful", 20, 18, new EventStartData.FarmerData(11, 20, EventHelper.FacingDirection.Right), new List<EventStartData.NPCData>() {
                new EventStartData.NPCData(haley,20,17, EventHelper.FacingDirection.Down),
            }));
            e.globalFadeIn();

            //Dialogue here.
            e.moveFarmerRight(9, EventHelper.FacingDirection.Up, false);

            e.speak(haley, GetEventString("DatingHaleyBirthday_Haley:0"));
            e.speak(haley, GetEventString("DatingHaleyBirthday_Haley:1"));
            e.emoteFarmer_Happy();
            e.speak(haley, GetEventString("DatingHaleyBirthday_Haley:2"));
            e.speak(haley, GetEventString("DatingHaleyBirthday_Haley:3"));
            e.emoteFarmer_Thinking();
            e.speak(haley, GetEventString("DatingHaleyBirthday_Haley:4"));


            e.emoteFarmer_Heart();
            e.emote_Heart("Haley");
            e.globalFadeOut(0.010);
            e.setViewportPosition(-100, -100);
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"DatingHaleyBirthday_Finish:0"); //abi party finish 0
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"DatingHaleyBirthday_Finish:1"); //abi party finish 0
            e.addObjectToPlayersInventory(221, 1, false);
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"PartyOver");
            e.end();
            return e;

        }

        public static EventHelper DatingBirthday_Sam()
        {
            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new GameLocationPrecondition(Game1.getLocationFromName("SamHouse")));
            conditions.Add(new TimeOfDayPrecondition(600, 2600));

            NPC sam = Game1.getCharacterFromName("Sam");

            conditions.Add(new DatingNPCEventPrecondition(sam));

            EventHelper e = new EventHelper(EventIds.BirthdayDatingSam, 19959, conditions, new EventStartData("playful", 3, 6, new EventStartData.FarmerData(7, 9, EventHelper.FacingDirection.Up), new List<EventStartData.NPCData>() {
                new EventStartData.NPCData(sam,3,5, EventHelper.FacingDirection.Down),
            }));
            e.globalFadeIn();

            //Dialogue here.
            e.moveFarmerUp(4, EventHelper.FacingDirection.Up, false);
            e.moveFarmerLeft(3, EventHelper.FacingDirection.Left, false);
            e.npcFaceDirection(sam, EventHelper.FacingDirection.Right);

            e.speak(sam, GetEventString("DatingSamBirthday_Sam:0"));
            e.speak(sam, GetEventString("DatingSamBirthday_Sam:1"));
            e.speak(sam, GetEventString("DatingSamBirthday_Sam:2"));
            e.speak(sam, GetEventString("DatingSamBirthday_Sam:3"));
            e.emoteFarmer_Heart();
            e.emote_Heart("Sam");
            e.globalFadeOut(0.010);
            e.setViewportPosition(-100, -100);
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"DatingSamBirthday_Finish:0"); //sam party finish 0
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"DatingSamBirthday_Finish:1"); //sam party finish 0
            e.addObjectToPlayersInventory(206, 1, false);
            e.addObjectToPlayersInventory(167, 1, false);
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"PartyOver");
            e.end();
            return e;
        }

        /// <summary>
        /// Event that occurs when the player is dating Sebastian.
        /// Status: Finished.
        /// </summary>
        /// <returns></returns>
        public static EventHelper DatingBirthday_Sebastian()
        {
            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new GameLocationPrecondition(Game1.getLocationFromName("ScienceHouse")));
            conditions.Add(new TimeOfDayPrecondition(600, 2600));

            NPC maru = Game1.getCharacterFromName("Maru");
            NPC sebastian = Game1.getCharacterFromName("Sebastian");
            NPC robin = Game1.getCharacterFromName("Robin");
            NPC demetrius = Game1.getCharacterFromName("Demetrius");

            conditions.Add(new DatingNPCEventPrecondition(sebastian));

            EventHelper e = new EventHelper(EventIds.BirthdayDatingSebastian, 19952, conditions, new EventStartData("playful", 28, 12, new EventStartData.FarmerData(23, 12, EventHelper.FacingDirection.Right), new List<EventStartData.NPCData>() {
                new EventStartData.NPCData(maru,27,11, EventHelper.FacingDirection.Down),
                new EventStartData.NPCData(sebastian,26,13, EventHelper.FacingDirection.Up),
                new EventStartData.NPCData(robin,28,9, EventHelper.FacingDirection.Up),
                new EventStartData.NPCData(demetrius,30,11, EventHelper.FacingDirection.Left)
            }));
            e.globalFadeIn();

            e.moveFarmerRight(3, EventHelper.FacingDirection.Right, true);
            e.npcFaceDirection(maru, EventHelper.FacingDirection.Left);
            e.npcFaceDirection(demetrius, EventHelper.FacingDirection.Left);
            //Seb is already facing up.
            e.npcFaceDirection(robin, EventHelper.FacingDirection.Down);

            //Dialogue goes here.
            //Seriously improve dialogue lines. Maru is probably the NPC I know the least about.
            e.speak(sebastian, GetEventString("DatingSebastianBirthday_Sebastian:0")); //sebastian 0
            e.speak(robin, GetEventString("DatingSebastianBirthday_Robin:0")); //maru 0
            e.speak(maru, GetEventString("DatingSebastianBirthday_Maru:0"));//Maru 0
            e.speak(robin, GetEventString("DatingSebastianBirthday_Robin:1")); //robin 0
            e.speak(demetrius, GetEventString("DatingSebastianBirthday_Demetrius:0")); //demetrius 0
            e.speak(sebastian, GetEventString("DatingSebastianBirthday_Sebastian:1")); //Sebastian 1
            e.emote_ExclamationMark("Robin");
            e.npcFaceDirection(robin, EventHelper.FacingDirection.Up);
            e.speak(robin, GetEventString("DatingSebastianBirthday_Robin:2")); //robin 1
            e.npcFaceDirection(robin, EventHelper.FacingDirection.Down);
            e.moveActorDown("Robin", 1, EventHelper.FacingDirection.Down, false);
            e.addObject(27, 12, 220);
            e.speak(demetrius, GetEventString("DatingSebastianBirthday_Demetrius:1")); //maru 2
            e.emoteFarmer_Thinking();
            e.speak(maru, GetEventString("DatingSebastianBirthday_Maru:1")); //maru 3
            e.speak(sebastian, GetEventString("DatingSebastianBirthday_Sebastian:2")); //Sebastian 1

            //Event finish commands.
            e.emoteFarmer_Heart();
            e.emote_Heart("Sebastian");
            e.globalFadeOut(0.010);
            e.setViewportPosition(-100, -100);
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"DatingSebastianBirthday_Finish:0"); //maru party finish 0
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"DatingSebastianBirthday_Finish:1"); //maru party finish 0
            e.addObjectToPlayersInventory(220, 1, false);

            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"PartyOver");
            e.end();
            return e;
        }



        public static EventHelper DatingBirthday_Elliott()
        {
            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new GameLocationPrecondition(Game1.getLocationFromName("ElliottHouse")));
            conditions.Add(new TimeOfDayPrecondition(600, 2600));

            NPC elliott = Game1.getCharacterFromName("Elliott");

            conditions.Add(new DatingNPCEventPrecondition(elliott));

            EventHelper e = new EventHelper(EventIds.BirthdayDatingElliott, 19958, conditions, new EventStartData("playful", 3, 5, new EventStartData.FarmerData(3, 8, EventHelper.FacingDirection.Up), new List<EventStartData.NPCData>() {
                new EventStartData.NPCData(elliott,3,5, EventHelper.FacingDirection.Down),
            }));
            e.globalFadeIn();

            //Dialogue here.
            e.moveFarmerUp(2, EventHelper.FacingDirection.Up, false);
            e.speak(elliott, GetEventString("DatingElliottBirthday_Elliott:0"));
            e.speak(elliott, GetEventString("DatingElliottBirthday_Elliott:1"));
            e.speak(elliott, GetEventString("DatingElliottBirthday_Elliott:2"));
            e.speak(elliott, GetEventString("DatingElliottBirthday_Elliott:3"));
            e.speak(elliott, GetEventString("DatingElliottBirthday_Elliott:4"));
            e.emoteFarmer_Thinking();
            e.speak(elliott, GetEventString("DatingElliottBirthday_Elliott:5"));
            e.emoteFarmer_Heart();
            e.emote_Heart("Elliott");
            e.globalFadeOut(0.010);
            e.setViewportPosition(-100, -100);
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"DatingElliottBirthday_Finish:0"); //abi party finish 0
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"DatingElliottBirthday_Finish:1"); //abi party finish 0
            e.addObjectToPlayersInventory(220, 1, false);
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"PartyOver");
            e.end();
            return e;
        }


        public static EventHelper DatingBirthday_Shane()
        {

            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new GameLocationPrecondition(Game1.getLocationFromName("AnimalShop")));
            conditions.Add(new TimeOfDayPrecondition(600, 2600));

            NPC shane = Game1.getCharacterFromName("Shane");

            conditions.Add(new DatingNPCEventPrecondition(shane));

            EventHelper e = new EventHelper(EventIds.BirthdayDatingShane, 19960, conditions, new EventStartData("playful", 26, 15, new EventStartData.FarmerData(19, 18, EventHelper.FacingDirection.Left), new List<EventStartData.NPCData>() {
                new EventStartData.NPCData(shane,25,16, EventHelper.FacingDirection.Down),
            }));
            e.globalFadeIn();

            //Dialogue here.
            e.moveFarmerRight(3, EventHelper.FacingDirection.Right, false);
            e.moveFarmerUp(2, EventHelper.FacingDirection.Up, false);
            e.moveFarmerRight(2, EventHelper.FacingDirection.Right, false);
            e.npcFaceDirection(shane, EventHelper.FacingDirection.Left);

            e.speak(shane, GetEventString("DatingShaneBirthday_Shane:0"));
            e.speak(shane, GetEventString("DatingShaneBirthday_Shane:1"));
            e.speak(shane, GetEventString("DatingShaneBirthday_Shane:2"));
            e.speak(shane, GetEventString("DatingShaneBirthday_Shane:3"));
            e.emoteFarmer_Heart();
            e.emote_Heart("Shane");
            e.globalFadeOut(0.010);
            e.setViewportPosition(-100, -100);
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"DatingShaneBirthday_Finish:0"); //sam party finish 0
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"DatingShaneBirthday_Finish:1"); //sam party finish 0
            e.addObjectToPlayersInventory(206, 1, false);
            e.addObjectToPlayersInventory(167, 1, false);
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"PartyOver");
            e.end();
            return e;
        }

        public static EventHelper DatingBirthday_Harvey()
        {
            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new GameLocationPrecondition(Game1.getLocationFromName("HarveyRoom")));
            conditions.Add(new TimeOfDayPrecondition(600, 2600));

            NPC harvey = Game1.getCharacterFromName("Harvey");

            conditions.Add(new DatingNPCEventPrecondition(harvey));

            EventHelper e = new EventHelper(EventIds.BirthdayDatingHarvey, 19957, conditions, new EventStartData("playful", 6, 6, new EventStartData.FarmerData(6, 11, EventHelper.FacingDirection.Up), new List<EventStartData.NPCData>() {
                new EventStartData.NPCData(harvey,3,6, EventHelper.FacingDirection.Down),
            }));
            e.globalFadeIn();

            //Dialogue here.
            e.moveFarmerUp(5, EventHelper.FacingDirection.Up, false);
            e.moveFarmerLeft(2, EventHelper.FacingDirection.Left, false);
            e.npcFaceDirection(harvey, EventHelper.FacingDirection.Right);
            e.speak(harvey, GetEventString("DatingHarveyBirthday_Harvey:0"));
            e.speak(harvey, GetEventString("DatingHarveyBirthday_Harvey:1"));
            e.emoteFarmer_QuestionMark();
            e.speak(harvey, GetEventString("DatingHarveyBirthday_Harvey:2"));
            e.speak(harvey, GetEventString("DatingHarveyBirthday_Harvey:3"));


            e.emoteFarmer_Heart();
            e.emote_Heart("Harvey");
            e.globalFadeOut(0.010);
            e.setViewportPosition(-100, -100);
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"DatingHarveyBirthday_Finish:0"); //abi party finish 0
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"DatingHarveyBirthday_Finish:1"); //abi party finish 0
            e.addObjectToPlayersInventory(237, 1, false);
            e.addObjectToPlayersInventory(348, 1, false);
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"PartyOver");
            e.end();
            return e;
        }


        public static EventHelper DatingBirthday_Alex()
        {
            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new GameLocationPrecondition(Game1.getLocationFromName("JoshHouse")));
            conditions.Add(new TimeOfDayPrecondition(600, 2600));

            NPC alex = Game1.getCharacterFromName("Alex");

            conditions.Add(new DatingNPCEventPrecondition(alex));

            EventHelper e = new EventHelper(EventIds.BirthdayDatingAlex, 19959, conditions, new EventStartData("playful", 3, 20, new EventStartData.FarmerData(7, 19, EventHelper.FacingDirection.Left), new List<EventStartData.NPCData>() {
                new EventStartData.NPCData(alex,3,19, EventHelper.FacingDirection.Down),
            }));
            e.globalFadeIn();

            //Dialogue here.
            e.moveFarmerLeft(3, EventHelper.FacingDirection.Left, false);
            e.npcFaceDirection(alex, EventHelper.FacingDirection.Right);

            e.speak(alex, GetEventString("DatingAlexBirthday_Alex:0"));
            e.speak(alex, GetEventString("DatingAlexBirthday_Alex:1"));
            e.speak(alex, GetEventString("DatingAlexBirthday_Alex:2"));
            e.speak(alex, GetEventString("DatingAlexBirthday_Alex:3"));
            e.emoteFarmer_Heart();
            e.emote_Heart("Alex");
            e.globalFadeOut(0.010);
            e.setViewportPosition(-100, -100);
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"DatingAlexBirthday_Finish:0"); //sam party finish 0
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"DatingAlexBirthday_Finish:1"); //sam party finish 0
            e.addObjectToPlayersInventory(206, 1, false);
            e.addObjectToPlayersInventory(167, 1, false);
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"PartyOver");
            e.end();
            return e;
        }


        /// <summary>
        /// Todo: Finish this.
        /// </summary>
        /// <returns></returns>
        public static EventHelper CommunityBirthday()
        {
            List<EventPrecondition> conditions = new List<EventPrecondition>();
            conditions.Add(new FarmerBirthdayPrecondition());
            conditions.Add(new GameLocationPrecondition(Game1.getLocationFromName("CommunityCenter")));
            conditions.Add(new TimeOfDayPrecondition(600, 2600));
            conditions.Add(new IsJojaMemberEventPrecondition(false));
            conditions.Add(new CommunityCenterCompletedEventPreconditon(true));
            //conditions.Add(new HasUnlockedCommunityCenter()); //Infered by the fact that you must enter the community center to trigger this event anyways.
            EventHelper e = new EventHelper("CommunityCenterBirthday_All", 19961, conditions, new EventStartData("playful", -100, -100, new EventStartData.FarmerData(32, 22, EventHelper.FacingDirection.Up), new List<EventStartData.NPCData>()
            {
                new EventStartData.NPCData(Game1.getCharacterFromName("Lewis"),32,12, EventHelper.FacingDirection.Down),
                

            }));

            e.globalFadeIn();

            e.moveFarmerUp(10, EventHelper.FacingDirection.Up, true);

            e.showMessage("Shhh. I think they are here.");
            e.showMessage("Somebody turn on the lights.");
            e.setViewportPosition(32, 12);


            e.emoteFarmer_ExclamationMark();
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"CommunityBirthdayParty_0");
            e.emoteFarmer_Heart();
            e.globalFadeOut(0.010);
            e.setViewportPosition(-100, -100);
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"CommunityBirthdayParty_1");
            BirthdayEventUtilities.addTranslatedMessageToBeShown(e,"PartyOver");
            e.addObjectToPlayersInventory(220, 1, false);

            e.end();

            return e;
        }

        /*

        public static EventHelper MarriedBirthday()
        {

        }




        public static EventHelper JojaBirthday()
        {

        }
        */


        public static string GetEventString(string Key)
        {
            return HappyBirthdayModCore.Instance.translationInfo.getTranslatedContentPackString(Key);
        }

    }
}
