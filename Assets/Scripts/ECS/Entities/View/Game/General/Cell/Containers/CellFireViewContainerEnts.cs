using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Game.General.Entities.Containers;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts.ECS.Entities.Game.General.Base.View.Containers.Cell
{
    internal sealed class CellFireViewContainerEnts : CellEntsAbstractContainer
    {
        private EcsEntity[,] _cellFireEnts;
        internal ref SpriteRendererComponent CellFireEnt_SprRendCom(int[] xy) => ref _cellFireEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();

        internal CellFireViewContainerEnts(GameObject[,] cellParentGOs, EcsWorld gameWorld) : base(cellParentGOs)
        {
            _cellFireEnts = new EcsEntity[CellValues.CELL_COUNT_X, CellValues.CELL_COUNT_Y];

            for (int x = 0; x < Xamount; x++)
                for (int y = 0; y < Yamount; y++)
                {
                    var parentGO = cellParentGOs[x, y].transform.Find("Fire").gameObject;

                    var sr = parentGO.GetComponent<SpriteRenderer>();
                    _cellFireEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));
                }
        }
    }
}
