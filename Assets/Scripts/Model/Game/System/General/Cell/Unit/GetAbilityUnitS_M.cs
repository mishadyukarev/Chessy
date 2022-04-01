using Chessy.Common;
using Chessy.Game.Entity.Model;
using System;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;

namespace Chessy.Game.Model.System
{
    sealed class GetAbilityUnitS_M : SystemModelGameAbs
    {
        internal GetAbilityUnitS_M(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Get(in byte cell_0)
        {
            eMG.UnitEs(cell_0).Ability(ButtonTypes.First).Reset();
            eMG.UnitEs(cell_0).Ability(ButtonTypes.Second).Reset();
            eMG.UnitEs(cell_0).Ability(ButtonTypes.Third).Reset();
            eMG.UnitEs(cell_0).Ability(ButtonTypes.Fourth).Reset();
            eMG.UnitEs(cell_0).Ability(ButtonTypes.Fifth).Reset();

            if (eMG.UnitPlayerTC(cell_0).Is(eMG.CurPlayerITC.PlayerT))
            {
                if (eMG.UnitTC(cell_0).HaveUnit)
                {
                    switch (eMG.UnitTC(cell_0).UnitT)
                    {
                        case UnitTypes.None: throw new Exception();

                        case UnitTypes.King:
                            eMG.CellEs(cell_0).UnitEs.Ability(ButtonTypes.First).Ability = AbilityTypes.CircularAttack;
                            eMG.CellEs(cell_0).UnitEs.Ability(ButtonTypes.Fourth).Ability = AbilityTypes.KingPassiveNearBonus;
                            break;

                        case UnitTypes.Pawn:

                            if (eMG.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.BowCrossbow))
                            {
                                eMG.UnitEs(cell_0).Ability(ButtonTypes.First).Ability = AbilityTypes.FireArcher;
                                eMG.UnitEs(cell_0).Ability(ButtonTypes.Second).Ability = AbilityTypes.ChangeCornerArcher;
                            }
                            else if (eMG.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Staff))
                            {

                            }
                            else
                            {
                                if (eMG.AdultForestC(cell_0).HaveAnyResources)
                                {
                                    if (eMG.HaveFire(cell_0)) eMG.CellEs(cell_0).UnitEs.Ability(ButtonTypes.First).Ability = AbilityTypes.PutOutFirePawn;
                                    else eMG.CellEs(cell_0).UnitEs.Ability(ButtonTypes.First).Ability = AbilityTypes.FirePawn;
                                }
                                else
                                {
                                    eMG.CellEs(cell_0).UnitEs.Ability(ButtonTypes.First).Ability = AbilityTypes.Seed;
                                }



                                if (eMG.BuildingTC(cell_0).HaveBuilding)
                                {
                                    eMG.UnitEs(cell_0).Ability(ButtonTypes.Second).Ability = AbilityTypes.DestroyBuilding;
                                }
                                else
                                {
                                    eMG.UnitEs(cell_0).Ability(ButtonTypes.Second).Ability = AbilityTypes.SetFarm;
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
                            eMG.UnitEs(cell_0).Ability(ButtonTypes.First).Ability = AbilityTypes.StunElfemale;
                            eMG.UnitEs(cell_0).Ability(ButtonTypes.Second).Ability = AbilityTypes.GrowAdultForest;

                            break;

                        case UnitTypes.Snowy:
                            eMG.UnitEs(cell_0).Ability(ButtonTypes.First).Ability = AbilityTypes.IncreaseWindSnowy;
                            eMG.UnitEs(cell_0).Ability(ButtonTypes.Second).Ability = AbilityTypes.DecreaseWindSnowy;
                            eMG.UnitEs(cell_0).Ability(ButtonTypes.Third).Ability = AbilityTypes.ChangeDirectionWind;
                            //E.UnitEs(Idx).Ability(ButtonTypes.Third).Ability = AbilityTypes.IceWall;
                            break;

                        case UnitTypes.Undead:
                            eMG.UnitEs(cell_0).Ability(ButtonTypes.First).Ability = AbilityTypes.Resurrect;
                            eMG.UnitEs(cell_0).Ability(ButtonTypes.Second).Ability = AbilityTypes.SetTeleport;
                            eMG.UnitEs(cell_0).Ability(ButtonTypes.Third).Ability = AbilityTypes.InvokeSkeletons;
                            if (eMG.BuildingTC(cell_0).HaveBuilding) eMG.UnitEs(cell_0).Ability(ButtonTypes.Fourth).Ability = AbilityTypes.DestroyBuilding;
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

                    if (eMG.BuildingTC(cell_0).Is(BuildingTypes.Teleport))
                    {
                        eMG.UnitEs(cell_0).Ability(ButtonTypes.Fifth).Ability = AbilityTypes.Teleport;
                    }

                }
            }
        }
    }
}