using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class RelaxUpdMS : IEcsRunSystem
    {
        public void Run()
        {
            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                for (var unit = UnitTypes.First; unit < UnitTypes.End; unit++)
                {
                    for (var levUnit = LevelTypes.First; levUnit < LevelTypes.End; levUnit++)
                    {
                        foreach (var idx_0 in WhereUnitsC.Idxs(unit, levUnit, player))
                        {
                            ref var unit_0 = ref EntityPool.Unit<UnitC>(idx_0);
                            ref var levUnit_0 = ref EntityPool.Unit<LevelC>(idx_0);
                            ref var ownUnit_0 = ref EntityPool.Unit<OwnerC>(idx_0);
                            ref var hpUnit_0 = ref EntityPool.Unit<HpC>(idx_0);
                            ref var condUnit_0 = ref EntityPool.Unit<ConditionC>(idx_0);
                            ref var effUnit_0 = ref EntityPool.Unit<UnitEffectsC>(idx_0);

                            ref var twUnit_0 = ref EntityPool.ToolWeapon<ToolWeaponC>(idx_0);

                            ref var env_0 = ref EntityPool.Environment<EnvC>(idx_0);
                            ref var envRes_0 = ref EntityPool.Environment<EnvResC>(idx_0);

                            ref var buil_0 = ref EntityPool.Build<BuildC>(idx_0);
                            ref var ownBuil_0 = ref EntityPool.Build<OwnerC>(idx_0);

                            ref var trail_0 = ref EntityPool.Trail<TrailC>(idx_0);


                            if (condUnit_0.Is(CondUnitTypes.Relaxed))
                            {
                                if (hpUnit_0.HaveMaxHp)
                                {
                                    if (unit_0.Is(UnitTypes.Pawn))
                                    {
                                        if (env_0.Have(EnvTypes.AdultForest))
                                        {
                                            var extract = ExtractorC.ExtractOnePawnWood(levUnit_0.Level);

                                            if (extract > envRes_0.Amount(EnvTypes.AdultForest))
                                            {
                                                extract = envRes_0.Max(EnvTypes.AdultForest);
                                            }

                                            InvResC.Add(ResTypes.Wood, ownUnit_0.Owner, extract);
                                            envRes_0.Take(EnvTypes.AdultForest, extract);

                                            if (envRes_0.Have(EnvTypes.AdultForest))
                                            {
                                                if (buil_0.Is(BuildTypes.Camp))
                                                {
                                                    buil_0.Remove();

                                                    
                                                    ownBuil_0.SetOwner(ownUnit_0.Owner);
                                                    buil_0.SetNew(BuildTypes.Woodcutter, ownBuil_0.Owner);
                                                }
                                                else if (!buil_0.Have)
                                                {
                                                    ownBuil_0.SetOwner(ownUnit_0.Owner);
                                                    buil_0.SetNew(BuildTypes.Woodcutter, ownUnit_0.Owner);
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
                                                buil_0.Remove();

                                                trail_0.ResetAll();
                                                env_0.Remove(EnvTypes.AdultForest);

                                                if (UnityEngine.Random.Range(0, 100) < 50)
                                                {
                                                    env_0.SetNew(EnvTypes.YoungForest);
                                                }
                                            }
                                        }

                                        else if (twUnit_0.Is(TWTypes.Pick))
                                        {
                                            if (env_0.Have(EnvTypes.Hill))
                                            {
                                                if (buil_0.Have)
                                                {
                                                    condUnit_0.Set(CondUnitTypes.Protected);
                                                }
                                                else
                                                {
                                                    if (envRes_0.HaveMax(EnvTypes.Hill))
                                                    {
                                                        condUnit_0.Set(CondUnitTypes.Protected);
                                                    }
                                                    else
                                                    {
                                                        envRes_0.SetMax(EnvTypes.Hill);
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

                                    else if (unit_0.Is(UnitTypes.Elfemale))
                                    {
                                        if (env_0.Have(EnvTypes.AdultForest))
                                        {
                                            if (!envRes_0.HaveMax(EnvTypes.AdultForest))
                                            {
                                                var adding = 30;

                                                envRes_0.Add(EnvTypes.AdultForest, adding);
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
                                    hpUnit_0.SetMax();
                                    if (hpUnit_0.HaveMaxHp)
                                    {
                                        hpUnit_0.SetMax();
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