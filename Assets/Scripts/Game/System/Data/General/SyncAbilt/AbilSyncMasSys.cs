using Leopotam.Ecs;
using System;

namespace Game.Game
{
    public sealed class AbilSyncMasSys : IEcsRunSystem
    {
        private EcsFilter<UnitC, OwnerC> _unitF = default;
        private EcsFilter<UniqAbilC> _uniqUnitF = default;
        private EcsFilter<EnvC> _envF = default;
        private EcsFilter<FireC> _fireF = default;

        public void Run()
        {
            foreach (var idx_0 in _uniqUnitF)
            {
                ref var unit_0 = ref _unitF.Get1(idx_0);

                ref var ownUnit_0 = ref _unitF.Get2(idx_0);

                ref var uniq_0 = ref _uniqUnitF.Get1(idx_0);

                ref var env_0 = ref _envF.Get1(idx_0);
                ref var fire_0 = ref _fireF.Get1(idx_0);


                if (ownUnit_0.Is(WhoseMoveC.CurPlayerI))
                {
                    if (unit_0.HaveUnit)
                    {
                        switch (unit_0.Unit)
                        {
                            case UnitTypes.None: throw new Exception();

                            case UnitTypes.King:
                                uniq_0.SetAbility(UniqButTypes.First, UniqAbilTypes.CircularAttack);
                                uniq_0.SetAbility(UniqButTypes.Second, UniqAbilTypes.BonusNear);
                                uniq_0.Reset(UniqButTypes.Third);
                                break;

                            case UnitTypes.Pawn:
                                if (env_0.Have(EnvTypes.AdultForest))
                                {
                                    if (fire_0.Have) uniq_0.SetAbility(UniqButTypes.First, UniqAbilTypes.PutOutFirePawn);
                                    else uniq_0.SetAbility(UniqButTypes.First, UniqAbilTypes.FirePawn);
                                }
                                else
                                {
                                    uniq_0.SetAbility(UniqButTypes.First, UniqAbilTypes.Seed);
                                }
                                uniq_0.Reset(UniqButTypes.Second);
                                uniq_0.Reset(UniqButTypes.Third);
                                break;

                            case UnitTypes.Archer:
                                uniq_0.SetAbility(UniqButTypes.First, UniqAbilTypes.FireArcher);
                                uniq_0.SetAbility(UniqButTypes.Second, UniqAbilTypes.ChangeCornerArcher);
                                uniq_0.Reset(UniqButTypes.Third);
                                break;

                            case UnitTypes.Scout:
                                uniq_0.SetAbility(UniqButTypes.First, UniqAbilTypes.None);
                                uniq_0.Reset(UniqButTypes.Second);
                                uniq_0.Reset(UniqButTypes.Third);
                                break;

                            case UnitTypes.Elfemale:
                                uniq_0.SetAbility(UniqButTypes.First, UniqAbilTypes.GrowAdultForest);
                                uniq_0.SetAbility(UniqButTypes.Second, UniqAbilTypes.StunElfemale);
                                uniq_0.SetAbility(UniqButTypes.Third, UniqAbilTypes.ChangeDirWind);
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