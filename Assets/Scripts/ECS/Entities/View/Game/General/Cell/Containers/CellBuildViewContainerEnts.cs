using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Game.General.Entities.Containers;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts.ECS.Entities.Game.General.Base.Containers.Cell
{
    internal sealed class CellBuildViewContainerEnts : CellEntsAbstractContainer
    {
        private EcsEntity[,] _cellBuildingEnts;
        internal ref SpriteRendererComponent CellBuildEnt_SpriteRendererCom(int[] xy) => ref _cellBuildingEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


        private EcsEntity[,] _cellBackBuildingEnts;
        internal ref SpriteRendererComponent CellBackBuildingEnt_SpriteRendererCom(int[] xy) => ref _cellBackBuildingEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();

        internal CellBuildViewContainerEnts(GameObject[,] cellParentGOs, EcsWorld gameWorld) : base(cellParentGOs)
        {
            _cellBuildingEnts = new EcsEntity[CellValues.CELL_COUNT_X, CellValues.CELL_COUNT_Y];
            _cellBackBuildingEnts = new EcsEntity[CellValues.CELL_COUNT_X, CellValues.CELL_COUNT_Y];

            for (int x = 0; x < Xamount; x++)
                for (int y = 0; y < Yamount; y++)
                {
                    var parentGO = cellParentGOs[x, y].transform.Find("Building").gameObject;


                    var sr = parentGO.GetComponent<SpriteRenderer>();

                    _cellBuildingEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));


                    sr = parentGO.transform.Find("BackBuilding").GetComponent<SpriteRenderer>();

                    _cellBackBuildingEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));
                }
        }
    }
}
