using System;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellFireEs;

namespace Game.Game
{
    struct AbilitySyncS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                ref var unit_0 = ref Unit(idx_0);
                ref var ownUnit_0 = ref CellUnitElseEs.Owner(idx_0);

                ref var fire_0 = ref Fire<HaveEffectC>(idx_0);


                if (ownUnit_0.Is(WhoseMoveE.CurPlayerI))
                {
                    if (unit_0.Have)
                    {
                        switch (unit_0.Unit)
                        {
                            case UnitTypes.None: throw new Exception();

                            case UnitTypes.King:
                                CellUnitUniqueButtonsEs.Ability(ButtonTypes.First, idx_0).Ability = UniqueAbilityTypes.CircularAttack;
                                CellUnitUniqueButtonsEs.Ability(ButtonTypes.Second, idx_0).Ability = UniqueAbilityTypes.BonusNear;
                                CellUnitUniqueButtonsEs.Ability(ButtonTypes.Third, idx_0).Reset();
                                break;

                            case UnitTypes.Pawn:
                                if (Resources(EnvironmentTypes.AdultForest, idx_0).Have)
                                {
                                    if (fire_0.Have) CellUnitUniqueButtonsEs.Ability(ButtonTypes.First, idx_0).Ability = UniqueAbilityTypes.PutOutFirePawn;
                                    else CellUnitUniqueButtonsEs.Ability(ButtonTypes.First, idx_0).Ability = UniqueAbilityTypes.FirePawn;
                                }
                                else
                                {
                                    CellUnitUniqueButtonsEs.Ability(ButtonTypes.First, idx_0).Ability = UniqueAbilityTypes.Seed;
                                }
                                CellUnitUniqueButtonsEs.Ability(ButtonTypes.Second, idx_0).Reset();
                                CellUnitUniqueButtonsEs.Ability(ButtonTypes.Third, idx_0).Reset();
                                break;

                            case UnitTypes.Archer:
                                CellUnitUniqueButtonsEs.Ability(ButtonTypes.First, idx_0).Ability = UniqueAbilityTypes.FireArcher;
                                CellUnitUniqueButtonsEs.Ability(ButtonTypes.Second, idx_0).Ability = UniqueAbilityTypes.ChangeCornerArcher;
                                CellUnitUniqueButtonsEs.Ability(ButtonTypes.Third, idx_0).Reset();
                                break;

                            case UnitTypes.Scout:
                                CellUnitUniqueButtonsEs.Ability(ButtonTypes.First, idx_0).Reset();
                                CellUnitUniqueButtonsEs.Ability(ButtonTypes.Second, idx_0).Reset();
                                CellUnitUniqueButtonsEs.Ability(ButtonTypes.Third, idx_0).Reset();
                                break;

                            case UnitTypes.Elfemale:
                                CellUnitUniqueButtonsEs.Ability(ButtonTypes.First, idx_0).Ability = UniqueAbilityTypes.StunElfemale;
                                CellUnitUniqueButtonsEs.Ability(ButtonTypes.Second, idx_0).Ability = UniqueAbilityTypes.GrowAdultForest;
                                CellUnitUniqueButtonsEs.Ability(ButtonTypes.Third, idx_0).Ability = UniqueAbilityTypes.ChangeDirectionWind;
                                break;

                            case UnitTypes.Snowy:
                                CellUnitUniqueButtonsEs.Ability(ButtonTypes.First, idx_0).Ability = UniqueAbilityTypes.FreezeDirectEnemy;
                                CellUnitUniqueButtonsEs.Ability(ButtonTypes.Second, idx_0).Ability = UniqueAbilityTypes.IceWall;
                                CellUnitUniqueButtonsEs.Ability(ButtonTypes.Third, idx_0).Reset();
                                break;

                            case UnitTypes.Camel:
                                CellUnitUniqueButtonsEs.Ability(ButtonTypes.First, idx_0).Reset();
                                CellUnitUniqueButtonsEs.Ability(ButtonTypes.Second, idx_0).Reset();
                                CellUnitUniqueButtonsEs.Ability(ButtonTypes.Third, idx_0).Reset();
                                break;

                            default: throw new Exception();
                        }
                    }
                }
                else
                {
                    CellUnitUniqueButtonsEs.Ability(ButtonTypes.First, idx_0).Reset();
                    CellUnitUniqueButtonsEs.Ability(ButtonTypes.Second, idx_0).Reset();
                    CellUnitUniqueButtonsEs.Ability(ButtonTypes.Third, idx_0).Reset();
                }
            }
        }
    }
}