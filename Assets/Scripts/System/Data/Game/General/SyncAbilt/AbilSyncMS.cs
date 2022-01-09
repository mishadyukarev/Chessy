﻿using System;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    sealed class AbilSyncMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                ref var unit_0 = ref Unit<UnitC>(idx_0);
                ref var ownUnit_0 = ref Unit<OwnerC>(idx_0);
                ref var uniq_0 = ref Unit<UniqAbilC>(idx_0);

                ref var env_0 = ref Environment<EnvironmentC>(idx_0);
                ref var fire_0 = ref Fire<HaveEffectC>(idx_0);


                if (ownUnit_0.Is(WhoseMoveC.CurPlayerI))
                {
                    if (unit_0.Have)
                    {
                        switch (unit_0.Unit)
                        {
                            case UnitTypes.None: throw new Exception();

                            case UnitTypes.King:
                                uniq_0.SetAbility(UniqButTypes.First, UniqueAbilTypes.CircularAttack);
                                uniq_0.SetAbility(UniqButTypes.Second, UniqueAbilTypes.BonusNear);
                                uniq_0.Reset(UniqButTypes.Third);
                                break;

                            case UnitTypes.Pawn:
                                if (env_0.Have(EnvTypes.AdultForest))
                                {
                                    if (fire_0.Have) uniq_0.SetAbility(UniqButTypes.First, UniqueAbilTypes.PutOutFirePawn);
                                    else uniq_0.SetAbility(UniqButTypes.First, UniqueAbilTypes.FirePawn);
                                }
                                else
                                {
                                    uniq_0.SetAbility(UniqButTypes.First, UniqueAbilTypes.Seed);
                                }
                                uniq_0.Reset(UniqButTypes.Second);
                                uniq_0.Reset(UniqButTypes.Third);
                                break;

                            case UnitTypes.Archer:
                                uniq_0.SetAbility(UniqButTypes.First, UniqueAbilTypes.FireArcher);
                                uniq_0.SetAbility(UniqButTypes.Second, UniqueAbilTypes.ChangeCornerArcher);
                                uniq_0.Reset(UniqButTypes.Third);
                                break;

                            case UnitTypes.Scout:
                                uniq_0.SetAbility(UniqButTypes.First, UniqueAbilTypes.None);
                                uniq_0.Reset(UniqButTypes.Second);
                                uniq_0.Reset(UniqButTypes.Third);
                                break;

                            case UnitTypes.Elfemale:
                                uniq_0.SetAbility(UniqButTypes.First, UniqueAbilTypes.GrowAdultForest);
                                uniq_0.SetAbility(UniqButTypes.Second, UniqueAbilTypes.StunElfemale);
                                uniq_0.SetAbility(UniqButTypes.Third, UniqueAbilTypes.ChangeDirWind);
                                break;

                            default: throw new Exception();
                        }
                    }
                }
                else
                {
                    uniq_0.Reset(UniqButTypes.First);
                    uniq_0.Reset(UniqButTypes.Second);
                    uniq_0.Reset(UniqButTypes.Third);
                }
            }
        }
    }
}