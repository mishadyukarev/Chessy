using Photon.Realtime;
using static Assets.Scripts.Main;

namespace Assets.Scripts.Static
{
    public static class CellBuildingWorker
    {
        internal static void SetPlayerBuilding(bool withEconomy, BuildingTypes buildingType, Player owner, params int[] xy)
        {
            Instance.EGGM.CellBuildEnt_BuilTypeCom(xy).SetBuildingType(buildingType);
            Instance.EGGM.CellBuildEnt_OwnerCom(xy).SetOwner(owner);
            Instance.EGGM.CellBuildEnt_CellBuilCom(xy).EnabledPlayerSR(true, Instance.EGGM.CellBuildEnt_BuilTypeCom(xy).BuildingType, owner);

            if (withEconomy)
            {
                Instance.EGGM.BuildingsEnt_BuildingsCom.AddAmountBuildings
                    (Instance.EGGM.CellBuildEnt_BuilTypeCom(xy).BuildingType, Instance.EGGM.CellBuildEnt_OwnerCom(xy).IsMasterClient);
            }
        }

        internal static void SetBotBuilding(BuildingTypes buildingType, params int[] xy)
        {
            Instance.EGGM.CellBuildEnt_BuilTypeCom(xy).SetBuildingType(buildingType);
            Instance.EGGM.CellBuildEnt_CellOwnerBotCom(xy).SetBot(true);
            Instance.EGGM.CellBuildEnt_CellBuilCom(xy).EnabledBotSR(true, Instance.EGGM.CellBuildEnt_BuilTypeCom(xy).BuildingType);
        }

        internal static void ResetBuilding(bool withEconomy, params int[] xy)
        {
            var buildType = Instance.EGGM.CellBuildEnt_BuilTypeCom(xy).BuildingType;

            if (withEconomy)
            {
                if (Instance.EGGM.CellBuildEnt_OwnerCom(xy).HaveOwner)
                {
                    if(buildType == BuildingTypes.City)
                    {
                        Instance.EGGM.BuildingsEnt_BuildingsCom.IsSettedCityDict[Instance.EGGM.CellBuildEnt_OwnerCom(xy).IsMasterClient] = false;
                        Instance.EGGM.BuildingsEnt_BuildingsCom.XySettedCityDict[Instance.EGGM.CellBuildEnt_OwnerCom(xy).IsMasterClient] = default;
                    }                  

                    Instance.EGGM.BuildingsEnt_BuildingsCom.TakeAmountBuildings(buildType, Instance.EGGM.CellBuildEnt_OwnerCom(xy).IsMasterClient);
                }
            }

            if (Instance.EGGM.CellBuildEnt_OwnerCom(xy).HaveOwner)
            {
                Instance.EGGM.CellBuildEnt_CellBuilCom(xy).EnabledPlayerSR(false, buildType);
                Instance.EGGM.CellBuildEnt_OwnerCom(xy).SetOwner(default);
            }

            else if (Instance.EGGM.CellBuildEnt_CellOwnerBotCom(xy).HaveBot)
            {
                Instance.EGGM.CellBuildEnt_CellBuilCom(xy).EnabledBotSR(false, buildType);       
                Instance.EGGM.CellBuildEnt_OwnerCom(xy).SetOwner(default);
            }

            Instance.EGGM.CellBuildEnt_BuilTypeCom(xy).ResetBuildingType();
        }
    }
}