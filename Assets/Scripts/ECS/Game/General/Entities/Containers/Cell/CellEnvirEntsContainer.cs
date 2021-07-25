using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts.ECS.Game.General.Entities.Containers
{
    internal sealed class CellEnvirEntsContainer : CellEntsAbstractContainer
    {
        private EcsEntity[,] _cellFertilizerEnts;
        internal ref SpriteRendererComponent CellFertilizerEnt_SprRendCom(int[] xy) => ref _cellFertilizerEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();
        internal ref AmountResourcesComponent CellFertilizerEnt_AmountResourcesCom(int[] xy) => ref _cellFertilizerEnts[xy[X], xy[Y]].Get<AmountResourcesComponent>();
        internal ref HaveEnvironmentComponent CellFertilizerEnt_HaveEnvCom(int[] xy) => ref _cellFertilizerEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>();


        private EcsEntity[,] _cellYoungForestEnts;
        internal ref SpriteRendererComponent CellYoungForestEnt_SprRendCom(int[] xy) => ref _cellYoungForestEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();
        internal ref HaveEnvironmentComponent CellYoungForestEnt_HaveEnvCom(int[] xy) => ref _cellYoungForestEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>();


        private EcsEntity[,] _cellAdultForestEnts;
        internal ref SpriteRendererComponent CellAdultForestEnt_SprRendCom(int[] xy) => ref _cellAdultForestEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();
        internal ref AmountResourcesComponent CellAdultForestEnt_AmountResourcesCom(int[] xy) => ref _cellAdultForestEnts[xy[X], xy[Y]].Get<AmountResourcesComponent>();
        internal ref HaveEnvironmentComponent CellAdultForestEnt_HaveEnvCom(int[] xy) => ref _cellAdultForestEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>();


        private EcsEntity[,] _cellHillEnts;
        internal ref SpriteRendererComponent CellHillEnt_SprRendCom(int[] xy) => ref _cellHillEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();
        internal ref AmountResourcesComponent CellHillEnt_AmountResourcesCom(int[] xy) => ref _cellHillEnts[xy[X], xy[Y]].Get<AmountResourcesComponent>();
        internal ref HaveEnvironmentComponent CellHillEnt_HaveEnvCom(int[] xy) => ref _cellHillEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>();


        private EcsEntity[,] _cellMountainEnts;
        internal ref SpriteRendererComponent CellMountainEnt_SprRendCom(int[] xy) => ref _cellMountainEnts[xy[X], xy[Y]].Get<SpriteRendererComponent>();
        internal ref HaveEnvironmentComponent CellMountainEnt_HaveEnvCom(int[] xy) => ref _cellMountainEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>();


        internal CellEnvirEntsContainer(GameObject[,] cellParentGOs, EcsWorld gameWorld) : base(cellParentGOs)
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
                        .Replace(new SpriteRendererComponent(sr))
                        .Replace(new AmountResourcesComponent());



                    sr = parentGO.transform.Find("YoungForest").GetComponent<SpriteRenderer>();

                    _cellYoungForestEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr));



                    sr = parentGO.transform.Find("AdultForest").GetComponent<SpriteRenderer>();

                    _cellAdultForestEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr))
                        .Replace(new AmountResourcesComponent());



                    sr = parentGO.transform.Find("Hill").GetComponent<SpriteRenderer>();

                    _cellHillEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr))
                        .Replace(new AmountResourcesComponent());



                    sr = parentGO.transform.Find("Mountain").GetComponent<SpriteRenderer>();

                    _cellMountainEnts[x, y] = gameWorld.NewEntity()
                        .Replace(new SpriteRendererComponent(sr))
                        .Replace(new AmountResourcesComponent());
                }


        }
    }
}
