using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Game.General.Entities.Containers;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts.ECS.Entities.Game.General.Cells.View.Containers.Cell
{
    internal sealed class CellEnvirViewContainerEnts : CellEntsAbstractContainer
    {
        private EcsEntity[,] _cellFertilizerEnts;
        internal ref SpriteRendererComponent CellFertilizerEnt_SprRendCom(int[] xy) => ref _cellFertilizerEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


        private EcsEntity[,] _cellYoungForestEnts;
        internal ref SpriteRendererComponent CellYoungForestEnt_SprRendCom(int[] xy) => ref _cellYoungForestEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


        private EcsEntity[,] _cellAdultForestEnts;
        internal ref SpriteRendererComponent CellAdultForestEnt_SprRendCom(int[] xy) => ref _cellAdultForestEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


        private EcsEntity[,] _cellHillEnts;
        internal ref SpriteRendererComponent CellHillEnt_SprRendCom(int[] xy) => ref _cellHillEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


        private EcsEntity[,] _cellMountainEnts;
        internal ref SpriteRendererComponent CellMountainEnt_SprRendCom(int[] xy) => ref _cellMountainEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();


        internal CellEnvirViewContainerEnts(GameObject[,] cellParentGOs, EcsWorld gameWorld) : base(cellParentGOs)
        {
            _cellFertilizerEnts = new EcsEntity[CellValues.CELL_COUNT_X, CellValues.CELL_COUNT_Y];
            _cellYoungForestEnts = new EcsEntity[CellValues.CELL_COUNT_X, CellValues.CELL_COUNT_Y];
            _cellAdultForestEnts = new EcsEntity[CellValues.CELL_COUNT_X, CellValues.CELL_COUNT_Y];
            _cellHillEnts = new EcsEntity[CellValues.CELL_COUNT_X, CellValues.CELL_COUNT_Y];
            _cellMountainEnts = new EcsEntity[CellValues.CELL_COUNT_X, CellValues.CELL_COUNT_Y];

            for (int x = 0; x < Xamount; x++)
                for (int y = 0; y < Yamount; y++)
                {
                    var parentGO = cellParentGOs[x, y].transform.Find("Environments").gameObject;

                    var sr = parentGO.transform.Find("Fertilizer").GetComponent<SpriteRenderer>();

                    _cellFertilizerEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));



                    sr = parentGO.transform.Find("YoungForest").GetComponent<SpriteRenderer>();

                    _cellYoungForestEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));



                    sr = parentGO.transform.Find("AdultForest").GetComponent<SpriteRenderer>();

                    _cellAdultForestEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));



                    sr = parentGO.transform.Find("Hill").GetComponent<SpriteRenderer>();

                    _cellHillEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));



                    sr = parentGO.transform.Find("Mountain").GetComponent<SpriteRenderer>();

                    _cellMountainEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));
                }


        }
    }
}
