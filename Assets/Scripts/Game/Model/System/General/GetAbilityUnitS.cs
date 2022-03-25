using Chessy.Common;
using Chessy.Game.Entity.Model;
using System;

namespace Chessy.Game.System.Model
{
    public struct GetAbilityUnitS
    {
        public GetAbilityUnitS(in byte cell_0, in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            e.UnitEs(cell_0).Ability(ButtonTypes.First).Reset();
            e.UnitEs(cell_0).Ability(ButtonTypes.Second).Reset();
            e.UnitEs(cell_0).Ability(ButtonTypes.Third).Reset();
            e.UnitEs(cell_0).Ability(ButtonTypes.Fourth).Reset();
            e.UnitEs(cell_0).Ability(ButtonTypes.Fifth).Reset();

            if (e.UnitPlayerTC(cell_0).Is(e.CurPlayerITC.Player))
            {
                if (e.UnitTC(cell_0).HaveUnit)
                {
                    switch (e.UnitTC(cell_0).Unit)
                    {
                        case UnitTypes.None: throw new Exception();

                        case UnitTypes.King:
                            e.CellEs(cell_0).UnitEs.Ability(ButtonTypes.First).Ability = AbilityTypes.CircularAttack;
                            e.CellEs(cell_0).UnitEs.Ability(ButtonTypes.Fourth).Ability = AbilityTypes.KingPassiveNearBonus;
                            break;

                        case UnitTypes.Pawn:

                            if (e.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.BowCrossbow))
                            {
                                e.UnitEs(cell_0).Ability(ButtonTypes.First).Ability = AbilityTypes.FireArcher;
                                e.UnitEs(cell_0).Ability(ButtonTypes.Second).Ability = AbilityTypes.ChangeCornerArcher;
                            }
                            else if (e.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Staff))
                            {

                            }
                            else
                            {
                                if (e.AdultForestC(cell_0).HaveAnyResources)
                                {
                                    if (e.HaveFire(cell_0)) e.CellEs(cell_0).UnitEs.Ability(ButtonTypes.First).Ability = AbilityTypes.PutOutFirePawn;
                                    else e.CellEs(cell_0).UnitEs.Ability(ButtonTypes.First).Ability = AbilityTypes.FirePawn;
                                }
                                else
                                {
                                    e.CellEs(cell_0).UnitEs.Ability(ButtonTypes.First).Ability = AbilityTypes.Seed;
                                }



                                if (e.BuildingTC(cell_0).HaveBuilding)
                                {
                                    e.UnitEs(cell_0).Ability(ButtonTypes.Fourth).Ability = AbilityTypes.DestroyBuilding;
                                }
                                else
                                {
                                    e.UnitEs(cell_0).Ability(ButtonTypes.Second).Ability = AbilityTypes.SetFarm;
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
                            e.UnitEs(cell_0).Ability(ButtonTypes.First).Ability = AbilityTypes.StunElfemale;
                            e.UnitEs(cell_0).Ability(ButtonTypes.Second).Ability = AbilityTypes.GrowAdultForest;

                            break;

                        case UnitTypes.Snowy:
                            e.UnitEs(cell_0).Ability(ButtonTypes.First).Ability = AbilityTypes.IncreaseWindSnowy;
                            e.UnitEs(cell_0).Ability(ButtonTypes.Second).Ability = AbilityTypes.DecreaseWindSnowy;
                            e.UnitEs(cell_0).Ability(ButtonTypes.Third).Ability = AbilityTypes.ChangeDirectionWind;
                            //E.UnitEs(Idx).Ability(ButtonTypes.Third).Ability = AbilityTypes.IceWall;
                            break;

                        case UnitTypes.Undead:
                            e.UnitEs(cell_0).Ability(ButtonTypes.First).Ability = AbilityTypes.Resurrect;
                            e.UnitEs(cell_0).Ability(ButtonTypes.Second).Ability = AbilityTypes.SetTeleport;
                            e.UnitEs(cell_0).Ability(ButtonTypes.Third).Ability = AbilityTypes.InvokeSkeletons;
                            if (e.BuildingTC(cell_0).HaveBuilding) e.UnitEs(cell_0).Ability(ButtonTypes.Fourth).Ability = AbilityTypes.DestroyBuilding;
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

                    if (e.BuildingTC(cell_0).Is(BuildingTypes.Teleport))
                    {
                        e.UnitEs(cell_0).Ability(ButtonTypes.Fifth).Ability = AbilityTypes.Teleport;
                    }

                }
            }
        }
    }
}