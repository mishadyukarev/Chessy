using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts.ECS.Game.General.Entities.Containers
{
    internal sealed class CellSupVisBlocksEntsContainer : CellEntsAbstractContainer
    {
        private EcsEntity[,] _cellProtectRelaxEnts;
        internal ref SpriteRendererComponent CellProtectRelaxEnt_SpriteRendererCom(int[] xy) => ref _cellProtectRelaxEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


        private EcsEntity[,] _cellMaxStepsEnts;
        internal ref SpriteRendererComponent CellMaxStepsEnt_SpriteRendererCom(int[] xy) => ref _cellMaxStepsEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


        internal CellSupVisBlocksEntsContainer(GameObject[,] cellParentGOs, EcsWorld gameWorld) : base(cellParentGOs)
        {
            _cellProtectRelaxEnts = new EcsEntity[CellValues.CELL_COUNT_X, CellValues.CELL_COUNT_Y];
            _cellMaxStepsEnts = new EcsEntity[CellValues.CELL_COUNT_X, CellValues.CELL_COUNT_Y];

            for (int x = 0; x < Xamount; x++)
                for (int y = 0; y < Yamount; y++)
                {
                    var sr = cellParentGOs[x, y].transform.Find("ProtectRelax").GetComponent<SpriteRenderer>();
                    _cellProtectRelaxEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));



                    sr = cellParentGOs[x, y].transform.Find("MaxSteps").GetComponent<SpriteRenderer>();
                    _cellMaxStepsEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));
                }
        }
    }
}
