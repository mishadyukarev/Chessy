using Leopotam.Ecs;

namespace Scripts.Game
{
    public sealed class RelaxUpdMasSys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, LevelUnitC, OwnerCom> _cellUnitMainFilt = default;
        private EcsFilter<CellUnitDataCom, HpUnitC> _cellUnitStatFilt = default;
        private EcsFilter<CellUnitDataCom, ConditionUnitC, UnitEffectsC> _cellUnitOthFilt = default;
        private EcsFilter<CellUnitDataCom, ToolWeaponC> _cellUnitTWFilt = default;

        private EcsFilter<CellEnvDataC, CellEnvResC> _cellEnvFilt = default;
        private EcsFilter<CellBuildDataC, OwnerCom> _cellBuildFilt = default;
        private EcsFilter<CellTrailDataC> _cellTrailFilt = default;
        public void Run()
        {
            for (var player = Support.MinPlayerType; player < Support.MaxPlayerType; player++)
            {
                for (var unit = Support.MinUnitType; unit < Support.MaxUnitType; unit++)
                {
                    for (var levUnit = (LevelUnitTypes)1; levUnit < (LevelUnitTypes)typeof(LevelUnitTypes).GetEnumNames().Length; levUnit++)
                    {
                        foreach (var idx_0 in WhereUnitsC.IdxsUnits(player, unit, levUnit))
                        {
                            ref var unit_0 = ref _cellUnitMainFilt.Get1(idx_0);
                            ref var levUnit_0 = ref _cellUnitMainFilt.Get2(idx_0);
                            ref var ownUnit_0 = ref _cellUnitMainFilt.Get3(idx_0);
                            ref var hpUnit_0 = ref _cellUnitStatFilt.Get2(idx_0);
                            ref var condUnit_0 = ref _cellUnitOthFilt.Get2(idx_0);
                            ref var effUnit_0 = ref _cellUnitOthFilt.Get3(idx_0);
                            ref var twUnit_0 = ref _cellUnitTWFilt.Get2(idx_0);

                            ref var env_0 = ref _cellEnvFilt.Get1(idx_0);
                            ref var envRes_0 = ref _cellEnvFilt.Get2(idx_0);

                            ref var buil_0 = ref _cellBuildFilt.Get1(idx_0);
                            ref var ownBuil_0 = ref _cellBuildFilt.Get2(idx_0);

                            ref var trail_0 = ref _cellTrailFilt.Get1(idx_0);


                            if (condUnit_0.Is(CondUnitTypes.Relaxed))
                            {
                                if (hpUnit_0.HaveMaxHpUnit(unit_0.Unit, effUnit_0.Have(UnitStatTypes.Hp), UnitsUpgC.UpgPercent(ownUnit_0.Owner, unit_0.Unit, UnitStatTypes.Hp)))
                                {
                                    if (unit_0.Is(UnitTypes.Pawn))
                                    {
                                        var adultForest = EnvTypes.AdultForest;

                                        if (env_0.Have(adultForest))
                                        {
                                            InventResC.AddAmountRes(ownUnit_0.Owner, ResTypes.Wood);
                                            envRes_0.TakeAmountRes(adultForest);

                                            if (envRes_0.HaveRes(adultForest))
                                            {
                                                if (buil_0.Is(BuildTypes.Camp))
                                                {
                                                    WhereBuildsC.Remove(ownUnit_0.Owner, BuildTypes.Camp, idx_0);
                                                    buil_0.Reset();

                                                    buil_0.SetBuild(BuildTypes.Woodcutter);
                                                    ownBuil_0.SetOwner(ownUnit_0.Owner);
                                                    WhereBuildsC.Add(ownUnit_0.Owner, buil_0.BuildType, idx_0);
                                                }
                                                else if (!buil_0.HaveBuild)
                                                {
                                                    buil_0.SetBuild(BuildTypes.Woodcutter);
                                                    ownBuil_0.SetOwner(ownUnit_0.Owner);
                                                    WhereBuildsC.Add(ownUnit_0.Owner, buil_0.BuildType, idx_0);
                                                }
                                                else if (buil_0.Is(BuildTypes.Woodcutter))
                                                {

                                                }
                                                else
                                                {
                                                    condUnit_0.CondUnitType = CondUnitTypes.Protected;
                                                }
                                            }
                                            else
                                            {
                                                if (buil_0.HaveBuild)
                                                {
                                                    WhereBuildsC.Remove(ownBuil_0.Owner, buil_0.BuildType, idx_0);
                                                    buil_0.Reset();
                                                }

                                                trail_0.ResetAll();
                                                env_0.Reset(adultForest);
                                                WhereEnvC.Remove(adultForest, idx_0);
                                            }
                                        }

                                        else if (twUnit_0.Is(ToolWeaponTypes.Pick))
                                        {
                                            if (env_0.Have(EnvTypes.Hill))
                                            {
                                                if (buil_0.HaveBuild)
                                                {
                                                    condUnit_0.CondUnitType = CondUnitTypes.Protected;
                                                }
                                                else
                                                {
                                                    if (envRes_0.HaveMaxRes(EnvTypes.Hill))
                                                    {
                                                        condUnit_0.CondUnitType = CondUnitTypes.Protected;
                                                    }
                                                    else
                                                    {
                                                        envRes_0.SetMaxAmountRes(EnvTypes.Hill);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                condUnit_0.CondUnitType = CondUnitTypes.Protected;
                                            }
                                        }

                                        else
                                        {
                                            condUnit_0.CondUnitType = CondUnitTypes.Protected;
                                        }
                                    }

                                    else
                                    {
                                        condUnit_0.CondUnitType = CondUnitTypes.Protected;
                                    }
                                }

                                else
                                {
                                    hpUnit_0.AddHealHp(unit_0.Unit, effUnit_0.Have(UnitStatTypes.Hp), UnitsUpgC.UpgPercent(ownUnit_0.Owner, unit_0.Unit, UnitStatTypes.Hp));
                                    if (hpUnit_0.HaveMaxHpUnit(unit_0.Unit, effUnit_0.Have(UnitStatTypes.Hp), UnitsUpgC.UpgPercent(ownUnit_0.Owner, unit_0.Unit, UnitStatTypes.Hp)))
                                    {
                                        hpUnit_0.SetMaxHp(unit_0.Unit, effUnit_0.Have(UnitStatTypes.Hp), UnitsUpgC.UpgPercent(ownUnit_0.Owner, unit_0.Unit, UnitStatTypes.Hp));
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}