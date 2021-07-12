using Photon.Realtime;
using static Assets.Scripts.Main;

namespace Assets.Scripts.Static
{
    public static class CellBuildingWorker
    {
        internal static void SetPlayerBuilding(bool withEconomy, BuildingTypes buildingType, Player owner, params int[] xy)
        {
            Instance.EntGGM.CellBuildEnt_BuilTypeCom(xy).SetBuildingType(buildingType);
            Instance.EntGGM.CellBuildEnt_OwnerCom(xy).SetOwner(owner);
            Instance.EntGGM.CellBuildEnt_CellBuilCom(xy).EnabledPlayerSR(true, Instance.EntGGM.CellBuildEnt_BuilTypeCom(xy).BuildingType, owner);

            if (withEconomy)
            {
                Instance.EntGGM.BuildingsEnt_BuildingsCom.AddAmountBuildings
                    (Instance.EntGGM.CellBuildEnt_BuilTypeCom(xy).BuildingType, Instance.EntGGM.CellBuildEnt_OwnerCom(xy).IsMasterClient);
            }
        }

        internal static void SetBotBuilding(BuildingTypes buildingType, params int[] xy)
        {
            Instance.EntGGM.CellBuildEnt_BuilTypeCom(xy).SetBuildingType(buildingType);
            Instance.EntGGM.CellBuildEnt_CellOwnerBotCom(xy).SetBot(true);
            Instance.EntGGM.CellBuildEnt_CellBuilCom(xy).EnabledBotSR(true, Instance.EntGGM.CellBuildEnt_BuilTypeCom(xy).BuildingType);
        }

        internal static void ResetBuilding(bool withEconomy, params int[] xy)
        {
            var buildType = Instance.EntGGM.CellBuildEnt_BuilTypeCom(xy).BuildingType;

            if (withEconomy)
            {
                if (Instance.EntGGM.CellBuildEnt_OwnerCom(xy).HaveOwner)
                {
                    if(buildType == BuildingTypes.City)
                    {
                        Instance.EntGGM.BuildingsEnt_BuildingsCom.IsSettedCityDict[Instance.EntGGM.CellBuildEnt_OwnerCom(xy).IsMasterClient] = false;
                        Instance.EntGGM.BuildingsEnt_BuildingsCom.XySettedCityDict[Instance.EntGGM.CellBuildEnt_OwnerCom(xy).IsMasterClient] = default;
                    }                  

                    Instance.EntGGM.BuildingsEnt_BuildingsCom.TakeAmountBuildings(buildType, Instance.EntGGM.CellBuildEnt_OwnerCom(xy).IsMasterClient);
                }
            }

            if (Instance.EntGGM.CellBuildEnt_OwnerCom(xy).HaveOwner)
            {
                Instance.EntGGM.CellBuildEnt_CellBuilCom(xy).EnabledPlayerSR(false, buildType);
                Instance.EntGGM.CellBuildEnt_OwnerCom(xy).SetOwner(default);
            }

            else if (Instance.EntGGM.CellBuildEnt_CellOwnerBotCom(xy).HaveBot)
            {
                Instance.EntGGM.CellBuildEnt_CellBuilCom(xy).EnabledBotSR(false, buildType);       
                Instance.EntGGM.CellBuildEnt_OwnerCom(xy).SetOwner(default);
            }

            Instance.EntGGM.CellBuildEnt_BuilTypeCom(xy).ResetBuildingType();
        }
    }
}