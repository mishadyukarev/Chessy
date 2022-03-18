using Chessy.Common;
using System;

namespace Chessy.Game.System.Model
{
    public struct GetAbilityUnitS
    {
        public GetAbilityUnitS(in byte idx_0, in EntitiesModel e)
        {
            e.UnitEs(idx_0).Ability(ButtonTypes.First).Reset();
            e.UnitEs(idx_0).Ability(ButtonTypes.Second).Reset();
            e.UnitEs(idx_0).Ability(ButtonTypes.Third).Reset();
            e.UnitEs(idx_0).Ability(ButtonTypes.Fourth).Reset();
            e.UnitEs(idx_0).Ability(ButtonTypes.Fifth).Reset();

            if (e.UnitPlayerTC(idx_0).Is(e.CurPlayerITC.Player))
            {
                if (e.UnitTC(idx_0).HaveUnit)
                {
                    switch (e.UnitTC(idx_0).Unit)
                    {
                        case UnitTypes.None: throw new Exception();

                        case UnitTypes.King:
                            e.CellEs(idx_0).UnitEs.Ability(ButtonTypes.First).Ability = AbilityTypes.CircularAttack;
                            e.CellEs(idx_0).UnitEs.Ability(ButtonTypes.Fourth).Ability = AbilityTypes.KingPassiveNearBonus;
                            break;

                        case UnitTypes.Pawn:

                            if (e.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                            {
                                e.UnitEs(idx_0).Ability(ButtonTypes.First).Ability = AbilityTypes.FireArcher;
                                e.UnitEs(idx_0).Ability(ButtonTypes.Second).Ability = AbilityTypes.ChangeCornerArcher;
                            }
                            else if (e.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Staff))
                            {

                            }
                            else
                            {
                                if (e.AdultForestC(idx_0).HaveAnyResources)
                                {
                                    if (e.HaveFire(idx_0)) e.CellEs(idx_0).UnitEs.Ability(ButtonTypes.First).Ability = AbilityTypes.PutOutFirePawn;
                                    else e.CellEs(idx_0).UnitEs.Ability(ButtonTypes.First).Ability = AbilityTypes.FirePawn;
                                }
                                else
                                {
                                    e.CellEs(idx_0).UnitEs.Ability(ButtonTypes.First).Ability = AbilityTypes.Seed;
                                }



                                if (e.BuildingTC(idx_0).HaveBuilding)
                                {
                                    e.UnitEs(idx_0).Ability(ButtonTypes.Fourth).Ability = AbilityTypes.DestroyBuilding;
                                }
                                else
                                {
                                    e.UnitEs(idx_0).Ability(ButtonTypes.Second).Ability = AbilityTypes.SetFarm;
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
                            e.UnitEs(idx_0).Ability(ButtonTypes.First).Ability = AbilityTypes.StunElfemale;
                            e.UnitEs(idx_0).Ability(ButtonTypes.Second).Ability = AbilityTypes.GrowAdultForest;

                            break;

                        case UnitTypes.Snowy:
                            e.UnitEs(idx_0).Ability(ButtonTypes.First).Ability = AbilityTypes.IncreaseWindSnowy;
                            e.UnitEs(idx_0).Ability(ButtonTypes.Second).Ability = AbilityTypes.DecreaseWindSnowy;
                            e.UnitEs(idx_0).Ability(ButtonTypes.Third).Ability = AbilityTypes.ChangeDirectionWind;
                            //E.UnitEs(Idx).Ability(ButtonTypes.Third).Ability = AbilityTypes.IceWall;
                            break;

                        case UnitTypes.Undead:
                            e.UnitEs(idx_0).Ability(ButtonTypes.First).Ability = AbilityTypes.Resurrect;
                            e.UnitEs(idx_0).Ability(ButtonTypes.Second).Ability = AbilityTypes.SetTeleport;
                            e.UnitEs(idx_0).Ability(ButtonTypes.Third).Ability = AbilityTypes.InvokeSkeletons;
                            if (e.BuildingTC(idx_0).HaveBuilding) e.UnitEs(idx_0).Ability(ButtonTypes.Fourth).Ability = AbilityTypes.DestroyBuilding;
                            break;

                        case UnitTypes.Hell:
                            break;

                        case UnitTypes.Camel:
                            break;

                        case UnitTypes.Skeleton:
                            break;

                        case UnitTypes.Tree:
                            break;

                        default: throw new Exception();
                    }

                    if (e.BuildingTC(idx_0).Is(BuildingTypes.Teleport))
                    {
                        e.UnitEs(idx_0).Ability(ButtonTypes.Fifth).Ability = AbilityTypes.Teleport;
                    }

                }
            }
        }
    }
}