using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Game.General.Components;
using Assets.Scripts.ECS.Game.General.Entities.Containers;
using Leopotam.Ecs;
using UnityEngine;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;

namespace Assets.Scripts.ECS.Game.General.Entities
{
    internal sealed class CellBuildDataContainerEnts
    {
        private EcsEntity[,] _cellBuildingEnts;
        internal ref TimeStepsComponent CellBuildEnt_TimeStepsCom(int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<TimeStepsComponent>();
        internal ref BuildingTypeComponent CellBuildEnt_BuilTypeCom(int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<BuildingTypeComponent>();
        internal ref OwnerComponent CellBuildEnt_OwnerCom(int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<OwnerComponent>();
        internal ref OwnerBotComponent CellBuildEnt_OwnerBotCom(int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<OwnerBotComponent>();


        internal CellBuildDataContainerEnts(EcsWorld gameWorld)
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
    }
}
