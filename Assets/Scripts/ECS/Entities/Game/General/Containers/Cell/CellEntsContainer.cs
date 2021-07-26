using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts.ECS.Game.General.Entities.Containers
{
    internal sealed class CellEntsContainer : CellEntsAbstractContainer
    {
        private EcsEntity[,] _cellEnts;
        internal ref CellGOComponent CellEnt_CellGOCom(int[] xy) => ref _cellEnts[xy[X], xy[Y]].Get<CellGOComponent>();

        internal CellEntsContainer(GameObject[,] cellParentGOs, EcsWorld gameWorld) : base(cellParentGOs)
        {
            _cellEnts = new EcsEntity[CellValues.CELL_COUNT_X, CellValues.CELL_COUNT_Y];

            for (int x = 0; x < Xamount; x++)
                for (int y = 0; y < Yamount; y++)
                {
                    var parentGO = cellParentGOs[x, y].transform.Find("Cell").gameObject;

                    _cellEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new CellGOComponent(parentGO));
                }
        }
    }
}
