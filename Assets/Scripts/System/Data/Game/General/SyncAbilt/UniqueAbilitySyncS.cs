using System;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellFireE;

namespace Game.Game
{
    struct UniqueAbilitySyncS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                ref var unit_0 = ref Else(idx_0).UnitC;
                ref var ownUnit_0 = ref CellUnitEs.Else(idx_0).OwnerC;

                ref var fire_0 = ref CellFireEs.Fire(idx_0).Fire;


                if (ownUnit_0.Is(Entities.WhoseMoveE.CurPlayerI))
                {
                    if (unit_0.Have)
                    {
                        switch (unit_0.Unit)
                        {
                            case UnitTypes.None: throw new Exception();

                            case UnitTypes.King:
                                CellUnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Ability = UniqueAbilityTypes.CircularAttack;
                                CellUnitEs.UniqueButton(ButtonTypes.Second, idx_0).AbilityC.Ability = UniqueAbilityTypes.BonusNear;
                                CellUnitEs.UniqueButton(ButtonTypes.Third, idx_0).AbilityC.Reset();
                                break;

                            case UnitTypes.Pawn:
                                if (Environment(EnvironmentTypes.AdultForest, idx_0).Resources.Have)
                                {
                                    if (fire_0.Have) CellUnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Ability = UniqueAbilityTypes.PutOutFirePawn;
                                    else CellUnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Ability = UniqueAbilityTypes.FirePawn;
                                }
                                else
                                {
                                    CellUnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Ability = UniqueAbilityTypes.Seed;
                                }
                                CellUnitEs.UniqueButton(ButtonTypes.Second, idx_0).AbilityC.Reset();
                                CellUnitEs.UniqueButton(ButtonTypes.Third, idx_0).AbilityC.Reset();
                                break;

                            case UnitTypes.Archer:
                                CellUnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Ability = UniqueAbilityTypes.FireArcher;
                                CellUnitEs.UniqueButton(ButtonTypes.Second, idx_0).AbilityC.Ability = UniqueAbilityTypes.ChangeCornerArcher;
                                CellUnitEs.UniqueButton(ButtonTypes.Third, idx_0).AbilityC.Reset();
                                break;

                            case UnitTypes.Scout:
                                CellUnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Reset();
                                CellUnitEs.UniqueButton(ButtonTypes.Second, idx_0).AbilityC.Reset();
                                CellUnitEs.UniqueButton(ButtonTypes.Third, idx_0).AbilityC.Reset();
                                break;

                            case UnitTypes.Elfemale:
                                CellUnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Ability = UniqueAbilityTypes.StunElfemale;
                                CellUnitEs.UniqueButton(ButtonTypes.Second, idx_0).AbilityC.Ability = UniqueAbilityTypes.GrowAdultForest;
                                CellUnitEs.UniqueButton(ButtonTypes.Third, idx_0).AbilityC.Ability = UniqueAbilityTypes.ChangeDirectionWind;
                                break;

                            case UnitTypes.Snowy:
                                CellUnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Ability = UniqueAbilityTypes.FreezeDirectEnemy;
                                CellUnitEs.UniqueButton(ButtonTypes.Second, idx_0).AbilityC.Ability = UniqueAbilityTypes.IceWall;
                                CellUnitEs.UniqueButton(ButtonTypes.Third, idx_0).AbilityC.Reset();
                                break;

                            case UnitTypes.Camel:
                                CellUnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Reset();
                                CellUnitEs.UniqueButton(ButtonTypes.Second, idx_0).AbilityC.Reset();
                                CellUnitEs.UniqueButton(ButtonTypes.Third, idx_0).AbilityC.Reset();
                                break;

                            default: throw new Exception();
                        }
                    }
                }
                else
                {
                    CellUnitEs.UniqueButton(ButtonTypes.First, idx_0).AbilityC.Reset();
                    CellUnitEs.UniqueButton(ButtonTypes.Second, idx_0).AbilityC.Reset();
                    CellUnitEs.UniqueButton(ButtonTypes.Third, idx_0).AbilityC.Reset();
                }
            }
        }
    }
}