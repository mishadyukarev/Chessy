﻿using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class RelaxUpdMasSys : IEcsRunSystem
    {
        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private EcsFilter<HpC> _statUnitF = default;
        private EcsFilter<ConditionUnitC, UnitEffectsC> _effUnitF = default;
        private EcsFilter<ToolWeaponC> _twUnitF = default;

        private EcsFilter<EnvC, EnvResC> _cellEnvFilt = default;
        private EcsFilter<BuildC, OwnerC> _cellBuildFilt = default;
        private EcsFilter<TrailC> _cellTrailFilt = default;
        public void Run()
        {
            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                for (var unit = UnitTypes.First; unit < UnitTypes.End; unit++)
                {
                    for (var levUnit = (LevelUnitTypes)1; levUnit < (LevelUnitTypes)typeof(LevelUnitTypes).GetEnumNames().Length; levUnit++)
                    {
                        foreach (var idx_0 in WhereUnitsC.IdxsUnits(player, unit, levUnit))
                        {
                            ref var unit_0 = ref _unitF.Get1(idx_0);
                            ref var levUnit_0 = ref _unitF.Get2(idx_0);
                            ref var ownUnit_0 = ref _unitF.Get3(idx_0);
                            ref var hpUnit_0 = ref _statUnitF.Get1(idx_0);
                            ref var condUnit_0 = ref _effUnitF.Get1(idx_0);
                            ref var effUnit_0 = ref _effUnitF.Get2(idx_0);
                            ref var twUnit_0 = ref _twUnitF.Get1(idx_0);

                            ref var env_0 = ref _cellEnvFilt.Get1(idx_0);
                            ref var envRes_0 = ref _cellEnvFilt.Get2(idx_0);

                            ref var buil_0 = ref _cellBuildFilt.Get1(idx_0);
                            ref var ownBuil_0 = ref _cellBuildFilt.Get2(idx_0);

                            ref var trail_0 = ref _cellTrailFilt.Get1(idx_0);


                            if (condUnit_0.Is(CondUnitTypes.Relaxed))
                            {
                                if (hpUnit_0.HaveMaxHp)
                                {
                                    if (unit_0.Is(UnitTypes.Pawn))
                                    {
                                        if (env_0.Have(EnvTypes.AdultForest))
                                        {
                                            var extract = ExtractC.ExtractOnePawnWood(levUnit_0.Level);

                                            if (extract > envRes_0.AmountRes(EnvTypes.AdultForest))
                                            {
                                                extract = envRes_0.MaxAmountRes(EnvTypes.AdultForest);
                                            }

                                            InvResC.AddAmountRes(ownUnit_0.Owner, ResTypes.Wood, extract);
                                            envRes_0.TakeAmountRes(EnvTypes.AdultForest, extract);

                                            if (envRes_0.HaveRes(EnvTypes.AdultForest))
                                            {
                                                if (buil_0.Is(BuildTypes.Camp))
                                                {
                                                    WhereBuildsC.Remove(ownUnit_0.Owner, BuildTypes.Camp, idx_0);
                                                    buil_0.Remove();

                                                    buil_0.SetNew(BuildTypes.Woodcutter);
                                                    ownBuil_0.SetOwner(ownUnit_0.Owner);
                                                    WhereBuildsC.Add(ownUnit_0.Owner, buil_0.Build, idx_0);
                                                }
                                                else if (!buil_0.Have)
                                                {
                                                    buil_0.SetNew(BuildTypes.Woodcutter);
                                                    ownBuil_0.SetOwner(ownUnit_0.Owner);
                                                    WhereBuildsC.Add(ownUnit_0.Owner, buil_0.Build, idx_0);
                                                }
                                                else if (buil_0.Is(BuildTypes.Woodcutter))
                                                {

                                                }
                                                else
                                                {
                                                    condUnit_0.Set(CondUnitTypes.Protected);
                                                }
                                            }
                                            else
                                            {
                                                if (buil_0.Have)
                                                {
                                                    WhereBuildsC.Remove(ownBuil_0.Owner, buil_0.Build, idx_0);
                                                    buil_0.Remove();
                                                }

                                                trail_0.ResetAll();
                                                env_0.Remove(EnvTypes.AdultForest);
                                                WhereEnvC.Remove(EnvTypes.AdultForest, idx_0);

                                                if (UnityEngine.Random.Range(0, 100) < 50)
                                                {
                                                    ref var envDatCom = ref _cellEnvFilt.Get1(idx_0);

                                                    envDatCom.Set(EnvTypes.YoungForest);
                                                    WhereEnvC.Add(EnvTypes.YoungForest, idx_0);
                                                }
                                            }
                                        }

                                        else if (twUnit_0.Is(ToolWeaponTypes.Pick))
                                        {
                                            if (env_0.Have(EnvTypes.Hill))
                                            {
                                                if (buil_0.Have)
                                                {
                                                    condUnit_0.Set(CondUnitTypes.Protected);
                                                }
                                                else
                                                {
                                                    if (envRes_0.HaveMaxRes(EnvTypes.Hill))
                                                    {
                                                        condUnit_0.Set(CondUnitTypes.Protected);
                                                    }
                                                    else
                                                    {
                                                        envRes_0.SetMaxAmountRes(EnvTypes.Hill);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                condUnit_0.Set(CondUnitTypes.Protected);
                                            }
                                        }

                                        else
                                        {
                                            condUnit_0.Set(CondUnitTypes.Protected);
                                        }
                                    }

                                    //else if (unit_0.Is(UnitTypes.Scout))
                                    //{
                                    //    if (env_0.Have(EnvTypes.AdultForest))
                                    //    {
                                    //        trail_0.SetAllTrail();
                                    //    }
                                    //}

                                    else if (unit_0.Is(UnitTypes.Elfemale))
                                    {
                                        if (env_0.Have(EnvTypes.AdultForest))
                                        {
                                            if (!envRes_0.HaveMaxRes(EnvTypes.AdultForest))
                                            {
                                                var adding = 3;

                                                if (adding + envRes_0.AmountRes(EnvTypes.AdultForest)
                                                    > envRes_0.MaxAmountRes(EnvTypes.AdultForest))
                                                {
                                                    envRes_0.SetMaxAmountRes(EnvTypes.AdultForest);
                                                }
                                                else
                                                {
                                                    envRes_0.AddAmountRes(EnvTypes.AdultForest, adding);
                                                }
                                            }
                                            else
                                            {
                                                condUnit_0.Set(CondUnitTypes.Protected);
                                            }
                                        }
                                        else
                                        {
                                            condUnit_0.Set(CondUnitTypes.Protected);
                                        }
                                    }

                                    else
                                    {
                                        condUnit_0.Set(CondUnitTypes.Protected);
                                    }
                                }

                                else
                                {
                                    hpUnit_0.SetMaxHp();
                                    if (hpUnit_0.HaveMaxHp)
                                    {
                                        hpUnit_0.SetMaxHp();
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