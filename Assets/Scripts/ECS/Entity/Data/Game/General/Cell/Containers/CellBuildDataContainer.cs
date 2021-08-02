using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;
using Photon.Realtime;
using System;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;

namespace Assets.Scripts.Workers
{
    public struct CellBuildDataContainer
    {
        private static EcsEntity[,] _cellBuildingEnts;

        internal CellBuildDataContainer(EcsWorld gameWorld)
        {
            _cellBuildingEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

            for (int x = 0; x < CELL_COUNT_X; x++)
                for (int y = 0; y < CELL_COUNT_Y; y++)
                {
                    _cellBuildingEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new TimeStepsComponent())
                        .Replace(new BuildingTypeComponent())
                        .Replace(new OwnerComponent())
                        .Replace(new OwnerBotComponent());
                }
        }



        #region BuildingType

        internal static BuildingTypes GetBuildingType(int[] xy) => _cellBuildingEnts[xy[X], xy[Y]].Get<BuildingTypeComponent>().BuildingType;
        internal static void SetBuildingType(BuildingTypes buildingType, int[] xy) => _cellBuildingEnts[xy[X], xy[Y]].Get<BuildingTypeComponent>().BuildingType = buildingType;

        internal static bool HaveAnyBuilding(int[] xy) => GetBuildingType(xy) != BuildingTypes.None;
        internal static bool IsBuildingType(BuildingTypes buildingType, int[] xy) => GetBuildingType(xy) == buildingType;
        internal static void ResetBuildingType(int[] xy) => SetBuildingType(default, xy);

        #endregion


        #region Owner

        internal static Player Owner(int[] xy) => _cellBuildingEnts[xy[X], xy[Y]].Get<OwnerComponent>().Owner;
        internal static void SetOwner(Player newOwner, int[] xy) => _cellBuildingEnts[xy[X], xy[Y]].Get<OwnerComponent>().Owner = newOwner;
        internal static void ResetOwner(int[] xy) => SetOwner(default, xy);
        internal static bool HaveOwner(int[] xy) => Owner(xy) != default;
        internal static bool IsMasterBuilding(int[] xy) => Owner(xy).IsMasterClient;
        internal static int ActorNumber(int[] xy) => Owner(xy).ActorNumber;
        internal static bool IsHim(Player player, int[] xy) => ActorNumber(xy) == player.ActorNumber;
        internal static bool IsMine(int[] xy) => Owner(xy).IsLocal;

        #endregion


        #region Bot

        internal static bool IsBot(int[] xy) => _cellBuildingEnts[xy[X], xy[Y]].Get<OwnerBotComponent>().IsBot;
        internal static void SetIsBot(bool isBot, int[] xy) => _cellBuildingEnts[xy[X], xy[Y]].Get<OwnerBotComponent>().IsBot = isBot;

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
                    return _cellBuildingEnts[xy[X], xy[Y]].Get<TimeStepsComponent>().TimeSteps;

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
                    _cellBuildingEnts[xy[X], xy[Y]].Get<TimeStepsComponent>().TimeSteps = value;
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