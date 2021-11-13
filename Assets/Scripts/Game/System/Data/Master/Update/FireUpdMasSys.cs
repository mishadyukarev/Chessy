using Leopotam.Ecs;
using UnityEditor;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class FireUpdMasSys : IEcsRunSystem
    {
        private EcsFilter<XyC> _xyCellFilter = default;
        private EcsFilter<CellC> _cellDataFilt = default;

        private EcsFilter<UnitC, LevelUnitC, OwnerC> _cellUnitMainFilt = default;
        private EcsFilter<UnitC, HpC> _cellUnitFilter = default;

        private EcsFilter<FireC> _cellFireDataFilter = default;
        private EcsFilter<EnvC, EnvResC> _cellEnvDataFilter = default;
        private EcsFilter<BuildC, OwnerC> _cellBuildFilt = default;
        private EcsFilter<CloudC> _cellCloudsFilt = default;

        public void Run()
        {
            foreach (byte idx_0 in _xyCellFilter)
            {
                var curXy = _xyCellFilter.Get1(idx_0).Xy;

                ref var unit_0 = ref _cellUnitFilter.Get1(idx_0);

                ref var levUnit_0 = ref _cellUnitMainFilt.Get2(idx_0);
                ref var ownUnit_0 = ref _cellUnitMainFilt.Get3(idx_0);

                ref var hpUnit_0 = ref _cellUnitFilter.Get2(idx_0);

                ref var buil_0 = ref _cellBuildFilt.Get1(idx_0);

                ref var fire_0 = ref _cellFireDataFilter.Get1(idx_0);
                ref var env_0 = ref _cellEnvDataFilter.Get1(idx_0);
                ref var envRes_0 = ref _cellEnvDataFilter.Get2(idx_0);

                ref var cloud_0 = ref _cellCloudsFilt.Get1(idx_0);


                if (cloud_0.Have)
                {
                    fire_0.Disable();
                }

                if (fire_0.Have)
                {
                    envRes_0.TakeAmountRes(EnvTypes.AdultForest, 2);

                    if (unit_0.HaveUnit)
                    {
                        hpUnit_0.TakeHp(40);

                        if (!hpUnit_0.HaveHp)
                        {
                            if (unit_0.Is(UnitTypes.King))
                            {
                                PlyerWinnerC.PlayerWinner = WhoseMoveC.NextPlayerFrom(ownUnit_0.Owner);
                            }
                            else if (unit_0.Is(new[] { UnitTypes.Scout, UnitTypes.Elfemale }))
                            {
                                ScoutHeroCooldownC.SetStandCooldown(ownUnit_0.Owner, unit_0.Unit);
                                InvUnitsC.AddUnit(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level);
                            }

                            WhereUnitsC.Remove(ownUnit_0.Owner, unit_0.Unit, levUnit_0.Level, idx_0);
                            unit_0.Reset();
                        }
                    }



                    if (!envRes_0.HaveRes(EnvTypes.AdultForest))
                    {
                        if (buil_0.HaveBuild)
                        {
                            buil_0.Reset();
                        }

                        env_0.Remove(EnvTypes.AdultForest);
                        WhereEnvC.Remove(EnvTypes.AdultForest, idx_0);


                        if (UnityEngine.Random.Range(0, 100) < 50)
                        {
                            ref var envDatCom = ref _cellEnvDataFilter.Get1(idx_0);

                            envDatCom.Set(EnvTypes.YoungForest);
                            WhereEnvC.Add(EnvTypes.YoungForest, idx_0);
                        }


                        fire_0.Disable();


                        var aroundXYList = CellSpaceSupport.GetXyAround(_xyCellFilter.Get1(idx_0).Xy);
                        foreach (var xy1 in aroundXYList)
                        {
                            var curIdxCell1 = _xyCellFilter.GetIdxCell(xy1);

                            if (_cellDataFilt.Get1(curIdxCell1).IsActiveCell)
                            {
                                if (_cellEnvDataFilter.Get1(curIdxCell1).Have(EnvTypes.AdultForest))
                                {
                                    _cellFireDataFilter.Get1(curIdxCell1).Enable();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}