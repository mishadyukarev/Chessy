using Photon.Realtime;
using System;
using static Assets.Scripts.Main;

namespace Assets.Scripts.Static
{
    public static class CellBuildingWorker
    {

        internal static EntitiesGameGeneralManager EGGM => Instance.EntGGM;
        internal static SpritesData SpritesData => Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SpritesConfig;

        private static void SetSpriteRender(BuildingTypes buildingType, params int[] xy)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    EGGM.CellBuildEnt_SpriteRendererCom(xy).SetSprite(SpritesData.City);
                    break;

                case BuildingTypes.Farm:
                    EGGM.CellBuildEnt_SpriteRendererCom(xy).SetSprite(SpritesData.Farm);
                    break;

                case BuildingTypes.Woodcutter:
                    EGGM.CellBuildEnt_SpriteRendererCom(xy).SetSprite(SpritesData.Woodcutter);
                    break;

                case BuildingTypes.Mine:
                    EGGM.CellBuildEnt_SpriteRendererCom(xy).SetSprite(SpritesData.Mine);
                    break;

                default:
                    throw new Exception();
            }
        }



        internal static void SetPlayerBuilding(bool withEconomy, BuildingTypes buildingType, Player owner, params int[] xy)
        {
            Instance.EntGGM.CellBuildEnt_BuilTypeCom(xy).SetBuildingType(buildingType);
            Instance.EntGGM.CellBuildEnt_OwnerCom(xy).SetOwner(owner);
            Instance.EntGGM.CellBuildEnt_SpriteRendererCom(xy).ActivateSR(true);
            SetSpriteRender(buildingType, xy);

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

            EGGM.CellBuildEnt_SpriteRendererCom(xy).ActivateSR(true);
            SetSpriteRender(buildingType, xy);
        }

        internal static void ResetBuilding(bool withEconomy, params int[] xy)
        {
            var buildType = Instance.EntGGM.CellBuildEnt_BuilTypeCom(xy).BuildingType;

            if (withEconomy)
            {
                if (Instance.EntGGM.CellBuildEnt_OwnerCom(xy).HaveOwner)
                {
                    if (buildType == BuildingTypes.City)
                    {
                        Instance.EntGGM.BuildingsEnt_BuildingsCom.IsSettedCityDict[Instance.EntGGM.CellBuildEnt_OwnerCom(xy).IsMasterClient] = false;
                        Instance.EntGGM.BuildingsEnt_BuildingsCom.XySettedCityDict[Instance.EntGGM.CellBuildEnt_OwnerCom(xy).IsMasterClient] = default;
                    }

                    Instance.EntGGM.BuildingsEnt_BuildingsCom.TakeAmountBuildings(buildType, Instance.EntGGM.CellBuildEnt_OwnerCom(xy).IsMasterClient);
                }
            }

            if (Instance.EntGGM.CellBuildEnt_OwnerCom(xy).HaveOwner)
            {
                EGGM.CellBuildEnt_SpriteRendererCom(xy).ActivateSR(false);


                Instance.EntGGM.CellBuildEnt_OwnerCom(xy).SetOwner(default);
            }

            else if (Instance.EntGGM.CellBuildEnt_CellOwnerBotCom(xy).HaveBot)
            {
                EGGM.CellBuildEnt_SpriteRendererCom(xy).ActivateSR(false);
                Instance.EntGGM.CellBuildEnt_OwnerCom(xy).SetOwner(default);
            }

            Instance.EntGGM.CellBuildEnt_BuilTypeCom(xy).ResetBuildingType();
        }
    }
}