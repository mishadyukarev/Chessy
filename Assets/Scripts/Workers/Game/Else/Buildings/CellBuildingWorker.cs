using Assets.Scripts.ECS.Game.General.Entities;
using Assets.Scripts.Workers.Cell;
using Photon.Realtime;
using System;
using static Assets.Scripts.Main;

namespace Assets.Scripts.Workers
{
    public class CellBuildingWorker : MainGeneralWorker
    {
        private static CellBuildingEntsContainer _cellBuildingEntsContainer;


        internal static SpritesData SpritesData => Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SpritesConfig;



        internal CellBuildingWorker(CellBuildingEntsContainer cellBuildingEntsContainer)
        {
            _cellBuildingEntsContainer = cellBuildingEntsContainer;
        }


        #region SpriteRenderer

        private static void SetSpriteRender(BuildingTypes buildingType, params int[] xy)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    _cellBuildingEntsContainer.CellBuildEnt_SpriteRendererCom(xy).SpriteRenderer.sprite = SpritesData.City;
                    break;

                case BuildingTypes.Farm:
                    _cellBuildingEntsContainer.CellBuildEnt_SpriteRendererCom(xy).SpriteRenderer.sprite = SpritesData.Farm;
                    break;

                case BuildingTypes.Woodcutter:
                    _cellBuildingEntsContainer.CellBuildEnt_SpriteRendererCom(xy).SpriteRenderer.sprite = SpritesData.Woodcutter;
                    break;

                case BuildingTypes.Mine:
                    _cellBuildingEntsContainer.CellBuildEnt_SpriteRendererCom(xy).SpriteRenderer.sprite = SpritesData.Mine;
                    break;

                default:
                    throw new Exception();
            }
        }
        private static void EnableSR(bool isActive, int[] xy) => _cellBuildingEntsContainer.CellBuildEnt_SpriteRendererCom(xy).SpriteRenderer.enabled = isActive;

        #endregion


        #region BuildingType

        internal static BuildingTypes BuildingType(int[] xy) => _cellBuildingEntsContainer.CellBuildEnt_BuilTypeCom(xy).BuildingType;
        internal static void SetBuildingType(BuildingTypes buildingType, int[] xy) => _cellBuildingEntsContainer.CellBuildEnt_BuilTypeCom(xy).BuildingType = buildingType;
        internal static bool HaveBuilding(int[] xy) => _cellBuildingEntsContainer.CellBuildEnt_BuilTypeCom(xy).BuildingType != BuildingTypes.None;
        internal static bool IsBuildingType(BuildingTypes buildingType, int[] xy) => _cellBuildingEntsContainer.CellBuildEnt_BuilTypeCom(xy).BuildingType == buildingType;
        internal static void ResetBuildingType(int[] xy) => SetBuildingType(BuildingTypes.None, xy);

        #endregion


        #region Owner

        internal static Player Owner(int[] xy) => _cellBuildingEntsContainer.CellBuildEnt_OwnerCom(xy).Owner;
        internal static void SetOwner(Player newOwner, int[] xy) => _cellBuildingEntsContainer.CellBuildEnt_OwnerCom(xy).Owner = newOwner;
        internal static void ResetOwner(int[] xy) => _cellBuildingEntsContainer.CellBuildEnt_OwnerCom(xy).Owner = default;
        internal static bool HaveOwner(int[] xy) => _cellBuildingEntsContainer.CellBuildEnt_OwnerCom(xy).Owner != default;
        internal static bool IsMasterBuilding(int[] xy) => _cellBuildingEntsContainer.CellBuildEnt_OwnerCom(xy).Owner.IsMasterClient;
        internal static int ActorNumber(int[] xy) => _cellBuildingEntsContainer.CellBuildEnt_OwnerCom(xy).Owner.ActorNumber;
        internal static bool IsHim(Player player, int[] xy) => ActorNumber(xy) == player.ActorNumber;

        #endregion


        #region Bot

        internal static bool IsBot(int[] xy) => _cellBuildingEntsContainer.CellBuildEnt_OwnerBotCom(xy).IsBot;
        internal static void SetIsBot(bool isBot, int[] xy) => _cellBuildingEntsContainer.CellBuildEnt_OwnerBotCom(xy).IsBot = isBot;

        #endregion

        #region Else

        internal static int TimeStepsBuilding(BuildingTypes buildingType, int[] xy)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    throw new Exception();

                case BuildingTypes.Farm:
                    throw new Exception();

                case BuildingTypes.Woodcutter:
                    throw new Exception();

                case BuildingTypes.Mine:
                    return _cellBuildingEntsContainer.CellBuildEnt_CellBuilCom(xy).TimeStepsMine;

                default:
                    throw new Exception();
            }
        }

        internal static void SetTimeStepsBuilding(BuildingTypes buildingType, int value, int[] xy)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    throw new Exception();

                case BuildingTypes.Farm:
                    throw new Exception();

                case BuildingTypes.Woodcutter:
                    throw new Exception();

                case BuildingTypes.Mine:
                    _cellBuildingEntsContainer.CellBuildEnt_CellBuilCom(xy).TimeStepsMine = value;
                    break;

                default:
                    throw new Exception();
            }
        }

        internal static void AddTimeStepsBuilding(BuildingTypes buildingType, int[] xy, int adding = 1)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    throw new Exception();

                case BuildingTypes.Farm:
                    throw new Exception();

                case BuildingTypes.Woodcutter:
                    throw new Exception();

                case BuildingTypes.Mine:
                    _cellBuildingEntsContainer.CellBuildEnt_CellBuilCom(xy).TimeStepsMine += adding;
                    break;

                default:
                    throw new Exception();
            }
        }

        internal static void TakeTimeStepsBuilding(BuildingTypes buildingType, int[] xy, int taking = 1)
        {
            switch (buildingType)
            {
                case BuildingTypes.None:
                    throw new Exception();

                case BuildingTypes.City:
                    throw new Exception();

                case BuildingTypes.Farm:
                    throw new Exception();

                case BuildingTypes.Woodcutter:
                    throw new Exception();

                case BuildingTypes.Mine:
                    _cellBuildingEntsContainer.CellBuildEnt_CellBuilCom(xy).TimeStepsMine -= taking;
                    break;

                default:
                    throw new Exception();
            }
        }

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