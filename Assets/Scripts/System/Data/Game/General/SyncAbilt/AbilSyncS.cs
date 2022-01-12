using System;
using static Game.Game.EntityCellPool;
using static Game.Game.EntCellUnit;
using static Game.Game.EntityCellEnvPool;
using static Game.Game.EntityCellFirePool;

namespace Game.Game
{
    struct AbilSyncS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                ref var unit_0 = ref Unit<UnitC>(idx_0);
                ref var ownUnit_0 = ref Unit<PlayerC>(idx_0);

                ref var fire_0 = ref Fire<HaveEffectC>(idx_0);


                if (ownUnit_0.Is(WhoseMoveC.CurPlayerI))
                {
                    if (unit_0.Have)
                    {
                        switch (unit_0.Unit)
                        {
                            case UnitTypes.None: throw new Exception();

                            case UnitTypes.King:
                                Unit<UniqueAbilityC>(UniqueButtonTypes.First, idx_0).Ability = UniqueAbilityTypes.CircularAttack;
                                Unit<UniqueAbilityC>(UniqueButtonTypes.Second, idx_0).Ability = UniqueAbilityTypes.BonusNear;
                                Unit<UniqueAbilityC>(UniqueButtonTypes.Third, idx_0).Reset();
                                break;

                            case UnitTypes.Pawn:
                                if (Environment<HaveEnvironmentC>(EnvTypes.AdultForest, idx_0).Have)
                                {
                                    if (fire_0.Have) Unit<UniqueAbilityC>(UniqueButtonTypes.First, idx_0).Ability = UniqueAbilityTypes.PutOutFirePawn;
                                    else Unit<UniqueAbilityC>(UniqueButtonTypes.First, idx_0).Ability = UniqueAbilityTypes.FirePawn;
                                }
                                else
                                {
                                    Unit<UniqueAbilityC>(UniqueButtonTypes.First, idx_0).Ability = UniqueAbilityTypes.Seed;
                                }
                                Unit<UniqueAbilityC>(UniqueButtonTypes.Second, idx_0).Reset();
                                Unit<UniqueAbilityC>(UniqueButtonTypes.Third, idx_0).Reset();
                                break;

                            case UnitTypes.Archer:
                                Unit<UniqueAbilityC>(UniqueButtonTypes.First, idx_0).Ability = UniqueAbilityTypes.FireArcher;
                                Unit<UniqueAbilityC>(UniqueButtonTypes.Second, idx_0).Ability = UniqueAbilityTypes.ChangeCornerArcher;
                                Unit<UniqueAbilityC>(UniqueButtonTypes.Third, idx_0).Reset();
                                break;

                            case UnitTypes.Scout:
                                Unit<UniqueAbilityC>(UniqueButtonTypes.First, idx_0).Reset();
                                Unit<UniqueAbilityC>(UniqueButtonTypes.Second, idx_0).Reset();
                                Unit<UniqueAbilityC>(UniqueButtonTypes.Third, idx_0).Reset();
                                break;

                            case UnitTypes.Elfemale:
                                Unit<UniqueAbilityC>(UniqueButtonTypes.First, idx_0).Ability = UniqueAbilityTypes.GrowAdultForest;
                                Unit<UniqueAbilityC>(UniqueButtonTypes.Second, idx_0).Ability = UniqueAbilityTypes.StunElfemale;
                                Unit<UniqueAbilityC>(UniqueButtonTypes.Third, idx_0).Ability = UniqueAbilityTypes.ChangeDirWind;
                                break;

                            default: throw new Exception();
                        }
                    }
                }
                else
                {
                    Unit<UniqueAbilityC>(UniqueButtonTypes.First, idx_0).Reset();
                    Unit<UniqueAbilityC>(UniqueButtonTypes.Second, idx_0).Reset();
                    Unit<UniqueAbilityC>(UniqueButtonTypes.Third, idx_0).Reset();
                }
            }
        }
    }
}