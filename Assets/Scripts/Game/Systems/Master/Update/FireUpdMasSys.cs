using Leopotam.Ecs;
using UnityEditor;
using UnityEngine;

namespace Scripts.Game
{
    internal sealed class FireUpdMasSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellDataC> _cellDataFilt = default;

        private EcsFilter<CellUnitDataCom, LevelUnitC, OwnerCom> _cellUnitMainFilt = default;
        private EcsFilter<CellUnitDataCom, HpUnitC> _cellUnitFilter = default;

        private EcsFilter<CellFireDataComponent> _cellFireDataFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvDataFilter = default;
        private EcsFilter<CellBuildDataC, OwnerCom> _cellBuildFilt = default;
        private EcsFilter<CellCloudsDataC> _cellCloudsFilt = default;

        public void Run()
        {
            foreach (byte idx_0 in _xyCellFilter)
            {
                var curXy = _xyCellFilter.GetXyCell(idx_0);

                ref var unitC_0 = ref _cellUnitFilter.Get1(idx_0);

                ref var levUnitC_0 = ref _cellUnitMainFilt.Get2(idx_0);
                ref var ownUnitC_0 = ref _cellUnitMainFilt.Get3(idx_0);

                ref var curHpUnitC = ref _cellUnitFilter.Get2(idx_0);

                ref var curBuilCom = ref _cellBuildFilt.Get1(idx_0);

                ref var curFireC = ref _cellFireDataFilter.Get1(idx_0);
                ref var curEnvrC = ref _cellEnvDataFilter.Get1(idx_0);

                ref var curCloudC = ref _cellCloudsFilt.Get1(idx_0);


                if (curCloudC.HaveCloud)
                {
                    curFireC.DisableFire();
                }

                if (curFireC.HaveFire)
                {
                    curEnvrC.TakeAmountRes(EnvTypes.AdultForest, 2);

                    if (unitC_0.HaveUnit)
                    {
                        curHpUnitC.TakeHp(40);

                        if (!curHpUnitC.HaveHp)
                        {
                            if (unitC_0.Is(UnitTypes.King))
                            {
                                if (ownUnitC_0.Is(PlayerTypes.First))
                                {
                                    EndGameDataUIC.PlayerWinner = PlayerTypes.Second;
                                }
                                else
                                {
                                    EndGameDataUIC.PlayerWinner = PlayerTypes.First;
                                }

                            }

                            unitC_0.NoneUnit();
                        }
                    }



                    if (!curEnvrC.HaveRes(EnvTypes.AdultForest))
                    {
                        if (curBuilCom.HaveBuild)
                        {
                            curBuilCom.NoneBuild();
                        }

                        curEnvrC.Reset(EnvTypes.AdultForest);
                        WhereEnvC.Remove(EnvTypes.AdultForest, idx_0);


                        if (UnityEngine.Random.Range(0, 100) < 70)
                        {
                            ref var envDatCom = ref _cellEnvDataFilter.Get1(idx_0);

                            envDatCom.SetNew(EnvTypes.YoungForest);
                            WhereEnvC.Add(EnvTypes.YoungForest, idx_0);
                        }

                        curFireC.HaveFire = false;


                        var aroundXYList = CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(idx_0));
                        foreach (var xy1 in aroundXYList)
                        {
                            var curIdxCell1 = _xyCellFilter.GetIdxCell(xy1);

                            if (_cellDataFilt.Get1(curIdxCell1).IsActiveCell)
                            {
                                if (_cellEnvDataFilter.Get1(curIdxCell1).Have(EnvTypes.AdultForest))
                                {
                                    _cellFireDataFilter.Get1(curIdxCell1).HaveFire = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}