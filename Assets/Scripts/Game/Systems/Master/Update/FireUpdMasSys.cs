using Leopotam.Ecs;
using UnityEditor;
using UnityEngine;

namespace Scripts.Game
{
    internal sealed class FireUpdMasSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellDataC> _cellDataFilt = default;
        private EcsFilter<CellUnitDataCom, HpComponent, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellFireDataComponent> _cellFireDataFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvDataFilter = default;
        private EcsFilter<CellBuildDataCom, OwnerCom> _cellBuildFilt = default;
        private EcsFilter<CellCloudsDataC> _cellCloudsFilt = default;

        public void Run()
        {
            foreach (byte curIdxCell in _xyCellFilter)
            {
                var curXy = _xyCellFilter.GetXyCell(curIdxCell);

                ref var curUnitCom = ref _cellUnitFilter.Get1(curIdxCell);
                ref var curHpUnitC = ref _cellUnitFilter.Get2(curIdxCell);
                ref var curOwnUnitCom = ref _cellUnitFilter.Get3(curIdxCell);

                ref var curBuilCom = ref _cellBuildFilt.Get1(curIdxCell);

                ref var curFireC = ref _cellFireDataFilter.Get1(curIdxCell);
                ref var curEnvrC = ref _cellEnvDataFilter.Get1(curIdxCell);

                ref var curCloudC = ref _cellCloudsFilt.Get1(curIdxCell);


                if (curCloudC.HaveCloud)
                {
                    curFireC.DisableFire();
                }

                if (curFireC.HaveFire)
                {
                    curEnvrC.TakeAmountRes(EnvirTypes.AdultForest, 2);

                    if (curUnitCom.HaveUnit)
                    {
                        curHpUnitC.TakeAmountHealth(40);

                        if (!curHpUnitC.HaveAmountHealth)
                        {
                            if (curUnitCom.Is(UnitTypes.King))
                            {
                                if (curOwnUnitCom.Is(PlayerTypes.First))
                                {
                                    EndGameDataUIC.PlayerWinner = PlayerTypes.Second;
                                }
                                else
                                {
                                    EndGameDataUIC.PlayerWinner = PlayerTypes.First;
                                }

                            }

                            curUnitCom.DefUnitType();
                        }
                    }



                    if (!curEnvrC.HaveRes(EnvirTypes.AdultForest))
                    {
                        if (curBuilCom.HaveBuild)
                        {
                            curBuilCom.BuildType = default;
                        }

                        curEnvrC.Reset(EnvirTypes.AdultForest);
                        WhereEnvironmentC.Remove(EnvirTypes.AdultForest, curIdxCell);


                        if (UnityEngine.Random.Range(0, 100) < 70)
                        {
                            ref var envDatCom = ref _cellEnvDataFilter.Get1(curIdxCell);

                            envDatCom.SetNew(EnvirTypes.YoungForest);
                            WhereEnvironmentC.Add(EnvirTypes.YoungForest, curIdxCell);
                        }

                        curFireC.HaveFire = false;


                        var aroundXYList = CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(curIdxCell));
                        foreach (var xy1 in aroundXYList)
                        {
                            var curIdxCell1 = _xyCellFilter.GetIdxCell(xy1);

                            if (_cellDataFilt.Get1(curIdxCell1).IsActiveCell)
                            {
                                if (_cellEnvDataFilter.Get1(curIdxCell1).Have(EnvirTypes.AdultForest))
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