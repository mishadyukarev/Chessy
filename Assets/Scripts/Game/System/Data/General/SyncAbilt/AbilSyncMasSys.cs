using Leopotam.Ecs;
using System;

namespace Chessy.Game
{
    public sealed class AbilSyncMasSys : IEcsRunSystem
    {
        private EcsFilter<UnitC, OwnerC> _unitBaseFilt = default;
        private EcsFilter<UniqAbilC> _uniqUnitF = default;
        private EcsFilter<EnvC> _envFilt = default;
        private EcsFilter<FireC> _fireFilt = default;

        public void Run()
        {
            foreach (var idx_0 in _uniqUnitF)
            {
                ref var unit_0 = ref _unitBaseFilt.Get1(idx_0);

                ref var ownUnit_0 = ref _unitBaseFilt.Get2(idx_0);

                ref var uniq_0 = ref _uniqUnitF.Get1(idx_0);

                ref var env_0 = ref _envFilt.Get1(idx_0);
                ref var fire_0 = ref _fireFilt.Get1(idx_0);


                if (ownUnit_0.Is(WhoseMoveC.CurPlayerI))
                {
                    if (unit_0.HaveUnit)
                    {
                        switch (unit_0.Unit)
                        {
                            case UnitTypes.None: throw new Exception();

                            case UnitTypes.King:
                                uniq_0.SetAbility(UniqButtonTypes.First, UniqAbilTypes.CircularAttack);
                                uniq_0.SetAbility(UniqButtonTypes.Second, UniqAbilTypes.BonusNear);
                                uniq_0.Reset(UniqButtonTypes.Third);
                                break;

                            case UnitTypes.Pawn:
                                if (env_0.Have(EnvTypes.AdultForest))
                                {
                                    if (fire_0.Have) uniq_0.SetAbility(UniqButtonTypes.First, UniqAbilTypes.PutOutFirePawn);
                                    else uniq_0.SetAbility(UniqButtonTypes.First, UniqAbilTypes.FirePawn);
                                }
                                else
                                {
                                    uniq_0.SetAbility(UniqButtonTypes.First, UniqAbilTypes.Seed);
                                }
                                uniq_0.Reset(UniqButtonTypes.Second);
                                uniq_0.Reset(UniqButtonTypes.Third);
                                break;

                            case UnitTypes.Archer:
                                uniq_0.SetAbility(UniqButtonTypes.First, UniqAbilTypes.FireArcher);
                                uniq_0.SetAbility(UniqButtonTypes.Second, UniqAbilTypes.ChangeCornerArcher);
                                uniq_0.Reset(UniqButtonTypes.Third);
                                break;

                            case UnitTypes.Scout:
                                uniq_0.SetAbility(UniqButtonTypes.First, UniqAbilTypes.None);
                                uniq_0.Reset(UniqButtonTypes.Second);
                                uniq_0.Reset(UniqButtonTypes.Third);
                                break;

                            case UnitTypes.Elfemale:
                                uniq_0.SetAbility(UniqButtonTypes.First, UniqAbilTypes.GrowAdultForest);
                                uniq_0.SetAbility(UniqButtonTypes.Second, UniqAbilTypes.StunElfemale);
                                uniq_0.SetAbility(UniqButtonTypes.Third, UniqAbilTypes.PutOutFireElfemale);
                                break;

                            default: throw new Exception();
                        }
                    }
                }
                else
                {
                    uniq_0.Reset(UniqButtonTypes.First);
                    uniq_0.Reset(UniqButtonTypes.Second);
                    uniq_0.Reset(UniqButtonTypes.Third);
                }
            }
        }
    }
}