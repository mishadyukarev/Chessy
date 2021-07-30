using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Game.General.Entities.Containers;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts.ECS.Entities.Game.General.Base.View.Containers.Cell
{
    internal sealed class CellUnitsViewContainerEnts : CellEntsAbstractContainer
    {
        private EcsEntity[,] _cellUnitEnts;
        internal ref SpriteRendererComponent CellUnitEnt_SpriteRendererCom(int[] xy) => ref _cellUnitEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


        internal CellUnitsViewContainerEnts(GameObject[,] cellParentGOs, EcsWorld gameWorld) : base(cellParentGOs)
        {
            _cellUnitEnts = new EcsEntity[CellValues.CELL_COUNT_X, CellValues.CELL_COUNT_Y];

            for (int x = 0; x < Xamount; x++)
                for (int y = 0; y < Yamount; y++)
                {
                    var sr = cellParentGOs[x, y].transform.Find("Unit").GetComponent<SpriteRenderer>();
                    _cellUnitEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));
                }
        }
    }
}