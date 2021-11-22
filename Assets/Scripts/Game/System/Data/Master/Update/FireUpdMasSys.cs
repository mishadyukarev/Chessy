using Leopotam.Ecs;
using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public sealed class FireUpdMasSys : IEcsRunSystem
    {
        private readonly EcsFilter<XyC> _xyF = default;
        private readonly EcsFilter<CellC> _cellDataFilt = default;

        private readonly EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private readonly EcsFilter<HpC> _statUnitF = default;

        private readonly EcsFilter<FireC> _cellFireDataFilter = default;
        private readonly EcsFilter<EnvC, EnvResC> _cellEnvDataFilter = default;
        private readonly EcsFilter<BuildC, OwnerC> _cellBuildFilt = default;
        private readonly EcsFilter<CloudC> _cellCloudsFilt = default;

        public void Run()
        {
            foreach (byte idx_0 in _xyF)
            {
                var curXy = _xyF.Get1(idx_0).Xy;

                ref var unit_0 = ref _unitF.Get1(idx_0);
                ref var levUnit_0 = ref _unitF.Get2(idx_0);
                ref var ownUnit_0 = ref _unitF.Get3(idx_0);

                ref var hpUnit_0 = ref _statUnitF.Get1(idx_0);

                ref var buil_0 = ref _cellBuildFilt.Get1(idx_0);
                ref var ownBuil_0 = ref _cellBuildFilt.Get2(idx_0);

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
                                InvUnitsC.AddUnit(unit_0.Unit, levUnit_0.Level, ownUnit_0.Owner);
                            }

                            unit_0.Remove(unit_0.Unit, levUnit_0.Level, ownUnit_0.Owner);
                        }
                    }



                    if (!envRes_0.HaveRes(EnvTypes.AdultForest))
                    {
                        if (buil_0.Have)
                        {
                            buil_0.Remove(ownBuil_0.Owner);
                        }

                        env_0.Remove(EnvTypes.AdultForest);


                        if (UnityEngine.Random.Range(0, 100) < 50)
                        {
                            ref var envDatCom = ref _cellEnvDataFilter.Get1(idx_0);

                            envDatCom.SetNew(EnvTypes.YoungForest);
                        }


                        fire_0.Disable();


                        var aroundXYList = CellSpace.GetXyAround(_xyF.Get1(idx_0).Xy);
                        foreach (var xy1 in aroundXYList)
                        {
                            var curIdxCell1 = _xyF.GetIdxCell(xy1);

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