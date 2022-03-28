using Chessy.Common;
using Chessy.Game.Entity.Model;
using System;

namespace Chessy.Game.System.Model
{
    sealed class GetAbilityUnitS : SystemModelGameAbs
    {
        internal GetAbilityUnitS(in EntitiesModelGame eMGame) : base(eMGame) { }

        internal void Get(in byte cell_0)
        {
            eMGame.UnitEs(cell_0).Ability(ButtonTypes.First).Reset();
            eMGame.UnitEs(cell_0).Ability(ButtonTypes.Second).Reset();
            eMGame.UnitEs(cell_0).Ability(ButtonTypes.Third).Reset();
            eMGame.UnitEs(cell_0).Ability(ButtonTypes.Fourth).Reset();
            eMGame.UnitEs(cell_0).Ability(ButtonTypes.Fifth).Reset();

            if (eMGame.UnitPlayerTC(cell_0).Is(eMGame.CurPlayerITC.Player))
            {
                if (eMGame.UnitTC(cell_0).HaveUnit)
                {
                    switch (eMGame.UnitTC(cell_0).Unit)
                    {
                        case UnitTypes.None: throw new Exception();

                        case UnitTypes.King:
                            eMGame.CellEs(cell_0).UnitEs.Ability(ButtonTypes.First).Ability = AbilityTypes.CircularAttack;
                            eMGame.CellEs(cell_0).UnitEs.Ability(ButtonTypes.Fourth).Ability = AbilityTypes.KingPassiveNearBonus;
                            break;

                        case UnitTypes.Pawn:

                            if (eMGame.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.BowCrossbow))
                            {
                                eMGame.UnitEs(cell_0).Ability(ButtonTypes.First).Ability = AbilityTypes.FireArcher;
                                eMGame.UnitEs(cell_0).Ability(ButtonTypes.Second).Ability = AbilityTypes.ChangeCornerArcher;
                            }
                            else if (eMGame.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Staff))
                            {

                            }
                            else
                            {
                                if (eMGame.AdultForestC(cell_0).HaveAnyResources)
                                {
                                    if (eMGame.HaveFire(cell_0)) eMGame.CellEs(cell_0).UnitEs.Ability(ButtonTypes.First).Ability = AbilityTypes.PutOutFirePawn;
                                    else eMGame.CellEs(cell_0).UnitEs.Ability(ButtonTypes.First).Ability = AbilityTypes.FirePawn;
                                }
                                else
                                {
                                    eMGame.CellEs(cell_0).UnitEs.Ability(ButtonTypes.First).Ability = AbilityTypes.Seed;
                                }



                                if (eMGame.BuildingTC(cell_0).HaveBuilding)
                                {
                                    eMGame.UnitEs(cell_0).Ability(ButtonTypes.Fourth).Ability = AbilityTypes.DestroyBuilding;
                                }
                                else
                                {
                                    eMGame.UnitEs(cell_0).Ability(ButtonTypes.Second).Ability = AbilityTypes.SetFarm;
                                }

                                //else
                                //{
                                //    if (E.BuildingsInfo(E.UnitPlayerTC(Idx).Player, LevelTypes.First, BuildingTypes.City).IdxC.HaveAny)
                                //    {
                                //        E.UnitEs(Idx).Ability(ButtonTypes.Fourth).Reset();
                                //    }
                                //    else
                                //    {
                                //        E.UnitEs(Idx).Ability(ButtonTypes.Fourth).Ability = AbilityTypes.SetCity;
                                //    }
                                //}
                            }

                            break;

                        case UnitTypes.Elfemale:
                            eMGame.UnitEs(cell_0).Ability(ButtonTypes.First).Ability = AbilityTypes.StunElfemale;
                            eMGame.UnitEs(cell_0).Ability(ButtonTypes.Second).Ability = AbilityTypes.GrowAdultForest;

                            break;

                        case UnitTypes.Snowy:
                            eMGame.UnitEs(cell_0).Ability(ButtonTypes.First).Ability = AbilityTypes.IncreaseWindSnowy;
                            eMGame.UnitEs(cell_0).Ability(ButtonTypes.Second).Ability = AbilityTypes.DecreaseWindSnowy;
                            eMGame.UnitEs(cell_0).Ability(ButtonTypes.Third).Ability = AbilityTypes.ChangeDirectionWind;
                            //E.UnitEs(Idx).Ability(ButtonTypes.Third).Ability = AbilityTypes.IceWall;
                            break;

                        case UnitTypes.Undead:
                            eMGame.UnitEs(cell_0).Ability(ButtonTypes.First).Ability = AbilityTypes.Resurrect;
                            eMGame.UnitEs(cell_0).Ability(ButtonTypes.Second).Ability = AbilityTypes.SetTeleport;
                            eMGame.UnitEs(cell_0).Ability(ButtonTypes.Third).Ability = AbilityTypes.InvokeSkeletons;
                            if (eMGame.BuildingTC(cell_0).HaveBuilding) eMGame.UnitEs(cell_0).Ability(ButtonTypes.Fourth).Ability = AbilityTypes.DestroyBuilding;
                            break;

                        case UnitTypes.Hell:
                            break;

                        case UnitTypes.Wolf:
                            break;

                        case UnitTypes.Skeleton:
                            break;

                        case UnitTypes.Tree:
                            break;

                        default: throw new Exception();
                    }

                    if (eMGame.BuildingTC(cell_0).Is(BuildingTypes.Teleport))
                    {
                        eMGame.UnitEs(cell_0).Ability(ButtonTypes.Fifth).Ability = AbilityTypes.Teleport;
                    }

                }
            }
        }
    }
}