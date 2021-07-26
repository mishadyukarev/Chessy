using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts.ECS.Game.General.Entities.Containers
{
    internal sealed class CellSupVisEntsContainer : CellEntsAbstractContainer
    {
        private EcsEntity[,] _cellSupportVisionEnts;
        internal ref SpriteRendererComponent CellSupVisEnt_SpriteRenderer(int[] xy) => ref _cellSupportVisionEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


        internal CellSupVisEntsContainer(GameObject[,] cellParentGOs, EcsWorld gameWorld) : base(cellParentGOs)
        {
            _cellSupportVisionEnts = new EcsEntity[CellValues.CELL_COUNT_X, CellValues.CELL_COUNT_Y];

            for (int x = 0; x < Xamount; x++)
                for (int y = 0; y < Yamount; y++)
                {
                    var parentGO = cellParentGOs[x, y].transform.Find("SupportVision").gameObject;

                    var sr = parentGO.GetComponent<SpriteRenderer>();
                    _cellSupportVisionEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));
                }
        }
    }
}
