using Photon.Realtime;
using static Assets.Scripts.Main;

namespace Assets.Scripts.Static
{
    public static class CellBuildingWorker
    {
        internal static void SetPlayerBuilding(bool withEconomy, BuildingTypes buildingType, Player owner, params int[] xy)
        {
            Instance.EGM.CellBuildEnt_BuilTypeCom(xy).BuildingType = buildingType;
            Instance.EGM.CellBuildEnt_OwnerCom(xy).SetOwner(owner);
            Instance.EGM.CellBuildEnt_CellBuilCom(xy).EnabledPlayerSR(true, Instance.EGM.CellBuildEnt_BuilTypeCom(xy).BuildingType, owner);

            if (withEconomy)
            {
                Instance.EGM.BuildingsEnt_BuildingsCom.AddAmountBuildings
                    (Instance.EGM.CellBuildEnt_BuilTypeCom(xy).BuildingType, Instance.EGM.CellBuildEnt_OwnerCom(xy).IsMasterClient);
            }
        }

        internal static void SetBotBuilding(BuildingTypes buildingType, params int[] xy)
        {
            Instance.EGM.CellBuildEnt_BuilTypeCom(xy).BuildingType = buildingType;
            Instance.EGM.CellBuildEnt_CellOwnerBotCom(xy).HaveBot = true;
            Instance.EGM.CellBuildEnt_CellBuilCom(xy).EnabledBotSR(true, Instance.EGM.CellBuildEnt_BuilTypeCom(xy).BuildingType);
        }

        internal static void ResetBuilding(bool withEconomy, params int[] xy)
        {
            var buildType = Instance.EGM.CellBuildEnt_BuilTypeCom(xy).BuildingType;

            if (withEconomy)
            {
                if (Instance.EGM.CellBuildEnt_OwnerCom(xy).HaveOwner)
                {
                    if (buildType == BuildingTypes.City)
                    {
                        Instance.EGM.BuildingsEnt_BuildingsCom.IsSettedCityDict[Instance.EGM.CellBuildEnt_OwnerCom(xy).IsMasterClient] = false;
                        Instance.EGM.BuildingsEnt_BuildingsCom.XySettedCityDict[Instance.EGM.CellBuildEnt_OwnerCom(xy).IsMasterClient] = default;
                    }

                    Instance.EGM.BuildingsEnt_BuildingsCom.TakeAmountBuildings(buildType, Instance.EGM.CellBuildEnt_OwnerCom(xy).IsMasterClient);
                }
            }

            if (Instance.EGM.CellBuildEnt_OwnerCom(xy).HaveOwner)
            {
                Instance.EGM.CellBuildEnt_CellBuilCom(xy).EnabledPlayerSR(false, buildType);
                Instance.EGM.CellBuildEnt_BuilTypeCom(xy).BuildingType = BuildingTypes.None;
                Instance.EGM.CellBuildEnt_OwnerCom(xy).SetOwner(default);
            }

            else if (Instance.EGM.CellBuildEnt_CellOwnerBotCom(xy).HaveBot)
            {
                Instance.EGM.CellBuildEnt_CellBuilCom(xy).EnabledBotSR(false, buildType);
                Instance.EGM.CellBuildEnt_BuilTypeCom(xy).BuildingType = BuildingTypes.None;
                Instance.EGM.CellBuildEnt_OwnerCom(xy).SetOwner(default);
            }
        }
    }
}