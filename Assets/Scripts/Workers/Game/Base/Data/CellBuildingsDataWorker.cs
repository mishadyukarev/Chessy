using Assets.Scripts.ECS.Game.General.Entities;
using Assets.Scripts.Workers.Cell;
using Photon.Realtime;
using System;

namespace Assets.Scripts.Workers
{
    public class CellBuildingsDataWorker : MainGeneralWorker
    {
        private static CellBuildDataContainerEnts _cellBuildingEntsContainer;


        internal CellBuildingsDataWorker(CellBuildDataContainerEnts cellBuildingEntsContainer)
        {
            _cellBuildingEntsContainer = cellBuildingEntsContainer;
        }



        #region BuildingType

        internal static BuildingTypes GetBuildingType(int[] xy) => _cellBuildingEntsContainer.CellBuildEnt_BuilTypeCom(xy).BuildingType;
        internal static void SetBuildingType(BuildingTypes buildingType, int[] xy) => _cellBuildingEntsContainer.CellBuildEnt_BuilTypeCom(xy).BuildingType = buildingType;

        internal static bool HaveAnyBuilding(int[] xy) => GetBuildingType(xy) != BuildingTypes.None;
        internal static bool IsBuildingType(BuildingTypes buildingType, int[] xy) => GetBuildingType(xy) == buildingType;
        internal static void ResetBuildingType(int[] xy) => SetBuildingType(default, xy);

        #endregion


        #region Owner

        internal static Player Owner(int[] xy) => _cellBuildingEntsContainer.CellBuildEnt_OwnerCom(xy).Owner;
        internal static void SetOwner(Player newOwner, int[] xy) => _cellBuildingEntsContainer.CellBuildEnt_OwnerCom(xy).Owner = newOwner;
        internal static void ResetOwner(int[] xy) => SetOwner(default, xy);
        internal static bool HaveOwner(int[] xy) => Owner(xy) != default;
        internal static bool IsMasterBuilding(int[] xy) => Owner(xy).IsMasterClient;
        internal static int ActorNumber(int[] xy) => Owner(xy).ActorNumber;
        internal static bool IsHim(Player player, int[] xy) => ActorNumber(xy) == player.ActorNumber;
        internal static bool IsMine(int[] xy) => Owner(xy).IsLocal;

        #endregion


        #region Bot

        internal static bool IsBot(int[] xy) => _cellBuildingEntsContainer.CellBuildEnt_OwnerBotCom(xy).IsBot;
        internal static void SetIsBot(bool isBot, int[] xy) => _cellBuildingEntsContainer.CellBuildEnt_OwnerBotCom(xy).IsBot = isBot;

        internal static void ResetIsBot(int[] xy) => SetIsBot(false, xy);

        #endregion


        #region Else

        internal static int GetTimeStepsBuilding(BuildingTypes buildingType, int[] xy)
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
                    return _cellBuildingEntsContainer.CellBuildEnt_TimeStepsCom(xy).TimeSteps;

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
                    _cellBuildingEntsContainer.CellBuildEnt_TimeStepsCom(xy).TimeSteps = value;
                    break;

                default:
                    throw new Exception();
            }
        }

        internal static void AddTimeStepsBuilding(BuildingTypes buildingType, int[] xy, int adding = 1)
            => SetTimeStepsBuilding(buildingType, GetTimeStepsBuilding(buildingType, xy) + adding, xy);
        internal static void TakeTimeStepsBuilding(BuildingTypes buildingType, int[] xy, int taking = 1)
            => SetTimeStepsBuilding(buildingType, GetTimeStepsBuilding(buildingType, xy) - taking, xy);

        #endregion


        internal static void SetPlayerBuilding(BuildingTypes buildingType, Player owner, int[] xy)
        {
            SetBuildingType(buildingType, xy);
            SetOwner(owner, xy);
        }
        internal static void SetBotBuilding(BuildingTypes buildingType, int[] xy)
        {
            SetBuildingType(buildingType, xy);
            SetIsBot(true, xy);
        }
        internal static void ResetBuild(int[] xy)
        {
            ResetOwner(xy);
            ResetIsBot(xy);
            ResetBuildingType(xy);
        }
    }
}