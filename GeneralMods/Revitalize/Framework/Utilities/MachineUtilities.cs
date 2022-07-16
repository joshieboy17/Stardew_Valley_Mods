using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Omegasis.Revitalize.Framework.Constants.ItemIds.Objects;
using Omegasis.Revitalize.Framework.World.Objects.InformationFiles;

namespace Omegasis.Revitalize.Framework.Utilities
{
    /// <summary>
    /// Utilities based around machines added by Revitalize and other mods.
    /// </summary>
    public class MachineUtilities
    {

        public static Dictionary<string, List<ResourceInformation>> ResourcesForMachines;


        public static List<ResourceInformation> GetResourcesProducedByThisMachine(string ID)
        {

            if (ResourcesForMachines == null) InitializeResourceList();

            if (ResourcesForMachines.ContainsKey(ID))
                return ResourcesForMachines[ID];
            else if (ID.Equals(Machines.MiningDrillV1))
                return RevitalizeModCore.ModContentManager.objectManager.resources.miningDrillResources.Values.ToList();

            return new List<ResourceInformation>();
        }

        public static void InitializeResourceList()
        {

            ResourcesForMachines = new Dictionary<string, List<ResourceInformation>>()
            {
                /*
            {"Revitalize.Objects.Machines.BatteryBin" ,new List<ResourceInformation>(){
                new ResourceInformation(new StardewValley.Object((int)Enums.SDVObject.BatteryPack,1),1,1,1,1,1,1,0,0,0,0)
            } },
            {"Revitalize.Objects.Machines.Sandbox",new List<ResourceInformation>(){
                new ResourceInformation(ModCore.ObjectManager.GetItem(MiscEarthenResources.Sand,1),1,1,1,1,1,1,0,0,0,0)
            } }
                */
            };
        }

    }
}
