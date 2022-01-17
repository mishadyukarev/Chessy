using System;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellFireEs;

namespace Game.Game
{
    struct AbilSyncS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                ref var unit_0 = ref Unit<UnitTC>(idx_0);
                ref var ownUnit_0 = ref Unit<PlayerTC>(idx_0);

                ref var fire_0 = ref Fire<HaveEffectC>(idx_0);


                if (ownUnit_0.Is(WhoseMoveE.CurPlayerI))
                {
                    if (unit_0.Have)
                    {
                        switch (unit_0.Unit)
                        {
                            case UnitTypes.None: throw new Exception();

                            case UnitTypes.King:
                                UnitUniqueButton<UniqueAbilityC>(ButtonTypes.First, idx_0).Ability = UniqueAbilityTypes.CircularAttack;
                                UnitUniqueButton<UniqueAbilityC>(ButtonTypes.Second, idx_0).Ability = UniqueAbilityTypes.BonusNear;
                                UnitUniqueButton<UniqueAbilityC>(ButtonTypes.Third, idx_0).Reset();
                                break;

                            case UnitTypes.Pawn:
                                if (Environment<HaveEnvironmentC>(EnvTypes.AdultForest, idx_0).Have)
                                {
                                    if (fire_0.Have) UnitUniqueButton<UniqueAbilityC>(ButtonTypes.First, idx_0).Ability = UniqueAbilityTypes.PutOutFirePawn;
                                    else UnitUniqueButton<UniqueAbilityC>(ButtonTypes.First, idx_0).Ability = UniqueAbilityTypes.FirePawn;
                                }
                                else
                                {
                                    UnitUniqueButton<UniqueAbilityC>(ButtonTypes.First, idx_0).Ability = UniqueAbilityTypes.Seed;
                                }
                                UnitUniqueButton<UniqueAbilityC>(ButtonTypes.Second, idx_0).Reset();
                                UnitUniqueButton<UniqueAbilityC>(ButtonTypes.Third, idx_0).Reset();
                                break;

                            case UnitTypes.Archer:
                                UnitUniqueButton<UniqueAbilityC>(ButtonTypes.First, idx_0).Ability = UniqueAbilityTypes.FireArcher;
                                UnitUniqueButton<UniqueAbilityC>(ButtonTypes.Second, idx_0).Ability = UniqueAbilityTypes.ChangeCornerArcher;
                                UnitUniqueButton<UniqueAbilityC>(ButtonTypes.Third, idx_0).Reset();
                                break;

                            case UnitTypes.Scout:
                                UnitUniqueButton<UniqueAbilityC>(ButtonTypes.First, idx_0).Reset();
                                UnitUniqueButton<UniqueAbilityC>(ButtonTypes.Second, idx_0).Reset();
                                UnitUniqueButton<UniqueAbilityC>(ButtonTypes.Third, idx_0).Reset();
                                break;

                            case UnitTypes.Elfemale:
                                UnitUniqueButton<UniqueAbilityC>(ButtonTypes.First, idx_0).Ability = UniqueAbilityTypes.GrowAdultForest;
                                UnitUniqueButton<UniqueAbilityC>(ButtonTypes.Second, idx_0).Ability = UniqueAbilityTypes.StunElfemale;
                                UnitUniqueButton<UniqueAbilityC>(ButtonTypes.Third, idx_0).Ability = UniqueAbilityTypes.ChangeDirWind;
                                break;

                            default: throw new Exception();
                        }
                    }
                }
                else
                {
                    UnitUniqueButton<UniqueAbilityC>(ButtonTypes.First, idx_0).Reset();
                    UnitUniqueButton<UniqueAbilityC>(ButtonTypes.Second, idx_0).Reset();
                    UnitUniqueButton<UniqueAbilityC>(ButtonTypes.Third, idx_0).Reset();
                }
            }
        }
    }
}