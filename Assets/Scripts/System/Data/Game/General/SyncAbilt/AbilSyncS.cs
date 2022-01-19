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
                                CellUnitUniqueButtonEs.UniqueAbility<UniqueAbilityC>(ButtonTypes.First, idx_0).Ability = UniqueAbilityTypes.CircularAttack;
                                CellUnitUniqueButtonEs.UniqueAbility<UniqueAbilityC>(ButtonTypes.Second, idx_0).Ability = UniqueAbilityTypes.BonusNear;
                                CellUnitUniqueButtonEs.UniqueAbility<UniqueAbilityC>(ButtonTypes.Third, idx_0).Reset();
                                break;

                            case UnitTypes.Pawn:
                                if (Environment<HaveEnvironmentC>(EnvironmentTypes.AdultForest, idx_0).Have)
                                {
                                    if (fire_0.Have) CellUnitUniqueButtonEs.UniqueAbility<UniqueAbilityC>(ButtonTypes.First, idx_0).Ability = UniqueAbilityTypes.PutOutFirePawn;
                                    else CellUnitUniqueButtonEs.UniqueAbility<UniqueAbilityC>(ButtonTypes.First, idx_0).Ability = UniqueAbilityTypes.FirePawn;
                                }
                                else
                                {
                                    CellUnitUniqueButtonEs.UniqueAbility<UniqueAbilityC>(ButtonTypes.First, idx_0).Ability = UniqueAbilityTypes.Seed;
                                }
                                CellUnitUniqueButtonEs.UniqueAbility<UniqueAbilityC>(ButtonTypes.Second, idx_0).Reset();
                                CellUnitUniqueButtonEs.UniqueAbility<UniqueAbilityC>(ButtonTypes.Third, idx_0).Reset();
                                break;

                            case UnitTypes.Archer:
                                CellUnitUniqueButtonEs.UniqueAbility<UniqueAbilityC>(ButtonTypes.First, idx_0).Ability = UniqueAbilityTypes.FireArcher;
                                CellUnitUniqueButtonEs.UniqueAbility<UniqueAbilityC>(ButtonTypes.Second, idx_0).Ability = UniqueAbilityTypes.ChangeCornerArcher;
                                CellUnitUniqueButtonEs.UniqueAbility<UniqueAbilityC>(ButtonTypes.Third, idx_0).Reset();
                                break;

                            case UnitTypes.Scout:
                                CellUnitUniqueButtonEs.UniqueAbility<UniqueAbilityC>(ButtonTypes.First, idx_0).Reset();
                                CellUnitUniqueButtonEs.UniqueAbility<UniqueAbilityC>(ButtonTypes.Second, idx_0).Reset();
                                CellUnitUniqueButtonEs.UniqueAbility<UniqueAbilityC>(ButtonTypes.Third, idx_0).Reset();
                                break;

                            case UnitTypes.Elfemale:
                                CellUnitUniqueButtonEs.UniqueAbility<UniqueAbilityC>(ButtonTypes.First, idx_0).Ability = UniqueAbilityTypes.GrowAdultForest;
                                CellUnitUniqueButtonEs.UniqueAbility<UniqueAbilityC>(ButtonTypes.Second, idx_0).Ability = UniqueAbilityTypes.StunElfemale;
                                CellUnitUniqueButtonEs.UniqueAbility<UniqueAbilityC>(ButtonTypes.Third, idx_0).Ability = UniqueAbilityTypes.ChangeDirectionWind;
                                break;

                            default: throw new Exception();
                        }
                    }
                }
                else
                {
                    CellUnitUniqueButtonEs.UniqueAbility<UniqueAbilityC>(ButtonTypes.First, idx_0).Reset();
                    CellUnitUniqueButtonEs.UniqueAbility<UniqueAbilityC>(ButtonTypes.Second, idx_0).Reset();
                    CellUnitUniqueButtonEs.UniqueAbility<UniqueAbilityC>(ButtonTypes.Third, idx_0).Reset();
                }
            }
        }
    }
}