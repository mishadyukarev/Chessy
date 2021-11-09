using Leopotam.Ecs;
using System;
using UnityEditor;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class AbilSyncMasSys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataC, OwnerC> _unitBaseFilt = default;
        private EcsFilter<CellUnitDataC, UniqAbilC> _unitUniqFilt = default;
        private EcsFilter<CellEnvDataC> _envFilt = default;
        private EcsFilter<CellFireDataC> _fireFilt = default;

        public void Run()
        {
            foreach (var idx_0 in _unitUniqFilt)
            {
                ref var unit_0 = ref _unitUniqFilt.Get1(idx_0);

                ref var ownUnit_0 = ref _unitBaseFilt.Get2(idx_0);

                ref var uniq_0 = ref _unitUniqFilt.Get2(idx_0);

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
                                break;

                            case UnitTypes.Pawn:
                                if (env_0.Have(EnvTypes.AdultForest))
                                {
                                    if (fire_0.HaveFire) uniq_0.SetAbility(UniqButtonTypes.First, UniqAbilTypes.NoneFirePawn);
                                    else uniq_0.SetAbility(UniqButtonTypes.First, UniqAbilTypes.FirePawn);
                                }
                                else
                                {
                                    uniq_0.SetAbility(UniqButtonTypes.First, UniqAbilTypes.Seed);
                                }
                                uniq_0.Reset(UniqButtonTypes.Second);
                                break;

                            case UnitTypes.Rook:
                                uniq_0.SetAbility(UniqButtonTypes.First, UniqAbilTypes.FireArcher);
                                uniq_0.Reset(UniqButtonTypes.Second);
                                break;

                            case UnitTypes.Bishop:
                                uniq_0.SetAbility(UniqButtonTypes.First, UniqAbilTypes.FireArcher);
                                uniq_0.Reset(UniqButtonTypes.Second);
                                break;

                            case UnitTypes.Scout:
                                uniq_0.SetAbility(UniqButtonTypes.First, UniqAbilTypes.None);
                                uniq_0.Reset(UniqButtonTypes.Second);
                                break;

                            case UnitTypes.Elfemale:
                                uniq_0.SetAbility(UniqButtonTypes.First, UniqAbilTypes.GrowAdultForest);
                                uniq_0.Reset(UniqButtonTypes.Second);
                                break;

                            default: throw new Exception();
                        }
                    }
                }
                else
                {
                    uniq_0.Reset(UniqButtonTypes.First);
                    uniq_0.Reset(UniqButtonTypes.Second);
                }
            }
        }
    }
}