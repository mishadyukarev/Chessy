﻿using Leopotam.Ecs;
using UnityEditor;
using UnityEngine;

namespace Scripts.Game
{
    public sealed class FireUpdMasSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellDataC> _cellDataFilt = default;

        private EcsFilter<CellUnitDataCom, LevelUnitC, OwnerCom> _cellUnitMainFilt = default;
        private EcsFilter<CellUnitDataCom, HpUnitC> _cellUnitFilter = default;

        private EcsFilter<CellFireDataC> _cellFireDataFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvDataFilter = default;
        private EcsFilter<CellBuildDataC, OwnerCom> _cellBuildFilt = default;
        private EcsFilter<CellCloudsDataC> _cellCloudsFilt = default;

        public void Run()
        {
            foreach (byte idx_0 in _xyCellFilter)
            {
                var curXy = _xyCellFilter.Get1(idx_0).XyCell;

                ref var unit_0 = ref _cellUnitFilter.Get1(idx_0);

                ref var levUnit_0 = ref _cellUnitMainFilt.Get2(idx_0);
                ref var ownUnit_0 = ref _cellUnitMainFilt.Get3(idx_0);

                ref var hpUnit_0 = ref _cellUnitFilter.Get2(idx_0);

                ref var buil_0 = ref _cellBuildFilt.Get1(idx_0);

                ref var fire_0 = ref _cellFireDataFilter.Get1(idx_0);
                ref var envr_0 = ref _cellEnvDataFilter.Get1(idx_0);

                ref var cloud_0 = ref _cellCloudsFilt.Get1(idx_0);


                if (cloud_0.HaveCloud)
                {
                    fire_0.DisableFire();
                }

                if (fire_0.HaveFire)
                {
                    envr_0.TakeAmountRes(EnvTypes.AdultForest, 2);

                    if (unit_0.HaveUnit)
                    {
                        hpUnit_0.TakeHp(40);

                        if (!hpUnit_0.HaveHp)
                        {
                            if (unit_0.Is(UnitTypes.King))
                            {
                                if (ownUnit_0.Is(PlayerTypes.First))
                                {
                                    EndGameDataUIC.PlayerWinner = PlayerTypes.Second;
                                }
                                else
                                {
                                    EndGameDataUIC.PlayerWinner = PlayerTypes.First;
                                }
                            }

                            WhereUnitsC.Remove(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level, idx_0);
                            unit_0.NoneUnit();
                        }
                    }



                    if (!envr_0.HaveRes(EnvTypes.AdultForest))
                    {
                        if (buil_0.HaveBuild)
                        {
                            buil_0.Reset();
                        }

                        envr_0.Reset(EnvTypes.AdultForest);
                        WhereEnvC.Remove(EnvTypes.AdultForest, idx_0);


                        if (UnityEngine.Random.Range(0, 100) < 70)
                        {
                            ref var envDatCom = ref _cellEnvDataFilter.Get1(idx_0);

                            envDatCom.SetNew(EnvTypes.YoungForest);
                            WhereEnvC.Add(EnvTypes.YoungForest, idx_0);
                        }

                        fire_0.HaveFire = false;


                        var aroundXYList = CellSpaceSupport.TryGetXyAround(_xyCellFilter.Get1(idx_0).XyCell);
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