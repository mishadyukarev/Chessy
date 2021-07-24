using Assets.Scripts.Workers.Cell;
using Photon.Realtime;
using System;
using static Assets.Scripts.Main;

namespace Assets.Scripts.Workers
{
    public class CellBuildingWorker : MainGeneralWorker
    {
        internal static SpritesData SpritesData => Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SpritesConfig;

        #region SpriteRenderer

        private static void SetSpriteRender(BuildingTypes buildingType, params int[] xy)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    EGGM.CellBuildEnt_SpriteRendererCom(xy).SpriteRenderer.sprite = SpritesData.City;
                    break;

                case BuildingTypes.Farm:
                    EGGM.CellBuildEnt_SpriteRendererCom(xy).SpriteRenderer.sprite = SpritesData.Farm;
                    break;

                case BuildingTypes.Woodcutter:
                    EGGM.CellBuildEnt_SpriteRendererCom(xy).SpriteRenderer.sprite = SpritesData.Woodcutter;
                    break;

                case BuildingTypes.Mine:
                    EGGM.CellBuildEnt_SpriteRendererCom(xy).SpriteRenderer.sprite = SpritesData.Mine;
                    break;

                default:
                    throw new Exception();
            }
        }
        private static void EnableSR(bool isActive, int[] xy) => Instance.EntGGM.CellBuildEnt_SpriteRendererCom(xy).SpriteRenderer.enabled = isActive;

        #endregion


        #region BuildingType

        internal static BuildingTypes BuildingType(int[] xy) => EGGM.CellBuildEnt_BuilTypeCom(xy).BuildingType;
        internal static void SetBuildingType(BuildingTypes buildingType, int[] xy) => EGGM.CellBuildEnt_BuilTypeCom(xy).BuildingType = buildingType;
        internal static bool HaveBuilding(int[] xy) => EGGM.CellBuildEnt_BuilTypeCom(xy).BuildingType != BuildingTypes.None;
        internal static bool IsBuildingType(BuildingTypes buildingType, int[] xy) => EGGM.CellBuildEnt_BuilTypeCom(xy).BuildingType == buildingType;
        internal static void ResetBuildingType(int[] xy) => SetBuildingType(BuildingTypes.None, xy);

        #endregion


        #region Owner

        internal static Player Owner(int[] xy) => EGGM.CellBuildEnt_OwnerCom(xy).Owner;
        internal static void SetOwner(Player newOwner, int[] xy) => EGGM.CellBuildEnt_OwnerCom(xy).Owner = newOwner;
        internal static void ResetOwner(int[] xy) => EGGM.CellBuildEnt_OwnerCom(xy).Owner = default;
        internal static bool HaveOwner(int[] xy) => EGGM.CellBuildEnt_OwnerCom(xy).Owner != default;
        internal static bool IsMasterBuilding(int[] xy) => EGGM.CellBuildEnt_OwnerCom(xy).Owner.IsMasterClient;
        internal static int ActorNumber(int[] xy) => EGGM.CellBuildEnt_OwnerCom(xy).Owner.ActorNumber;
        internal static bool IsHim(Player player, int[] xy) => ActorNumber(xy) == player.ActorNumber;

        #endregion


        #region Bot

        internal static bool IsBot(int[] xy) => EGGM.CellBuildEnt_OwnerBotCom(xy).IsBot;
        internal static void SetIsBot(bool isBot, int[] xy) => EGGM.CellBuildEnt_OwnerBotCom(xy).IsBot = isBot;

        #endregion


        internal static void CreatePlayerBuilding(BuildingTypes buildingType, Player owner, int[] xy)
        {
            SetBuildingType(buildingType, xy);
            SetOwner(owner, xy);
            EnableSR(true, xy);
            SetSpriteRender(buildingType, xy);
        }
        internal static void SetBotBuilding(BuildingTypes buildingType, params int[] xy)
        {
            SetBuildingType(buildingType, xy);
            SetIsBot(true, xy);

            EnableSR(true, xy);
            SetSpriteRender(buildingType, xy);
        }
        internal static void ResetPlayerBuilding(params int[] xy)
        {
            if (HaveOwner(xy))
            {
                EnableSR(false, xy);

                ResetOwner(xy);
            }

            else if (IsBot(xy))
            {
                EnableSR(false, xy);
                ResetOwner(xy);
            }

            ResetBuildingType(xy);
        }
    }
}