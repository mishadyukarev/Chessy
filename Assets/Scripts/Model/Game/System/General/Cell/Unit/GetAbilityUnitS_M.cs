using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using System;

namespace Chessy.Game.Model.System
{
    sealed class GetAbilityUnitS_M : SystemModelGameAbs
    {
        internal GetAbilityUnitS_M(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Get(in byte cell_0)
        {
            eMG.UnitButtonAbilitiesC(cell_0).SetAbility(ButtonTypes.First, AbilityTypes.None);
            eMG.UnitButtonAbilitiesC(cell_0).SetAbility(ButtonTypes.Second, AbilityTypes.None);
            eMG.UnitButtonAbilitiesC(cell_0).SetAbility(ButtonTypes.Third, AbilityTypes.None);
            eMG.UnitButtonAbilitiesC(cell_0).SetAbility(ButtonTypes.Fourth, AbilityTypes.None);
            eMG.UnitButtonAbilitiesC(cell_0).SetAbility(ButtonTypes.Fifth, AbilityTypes.None);

            if (eMG.UnitPlayerTC(cell_0).Is(eMG.CurPlayerITC.PlayerT))
            {
                if (eMG.UnitTC(cell_0).HaveUnit)
                {
                    switch (eMG.UnitTC(cell_0).UnitT)
                    {
                        case UnitTypes.None: throw new Exception();

                        case UnitTypes.King:
                            eMG.UnitButtonAbilitiesC(cell_0).SetAbility(ButtonTypes.First, AbilityTypes.CircularAttack);
                            eMG.UnitButtonAbilitiesC(cell_0).SetAbility(ButtonTypes.Fourth, AbilityTypes.KingPassiveNearBonus);
                            break;

                        case UnitTypes.Pawn:

                            if (eMG.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.BowCrossbow))
                            {
                                eMG.UnitButtonAbilitiesC(cell_0).SetAbility(ButtonTypes.First, AbilityTypes.FireArcher);
                                eMG.UnitButtonAbilitiesC(cell_0).SetAbility(ButtonTypes.Second, AbilityTypes.ChangeCornerArcher);
                            }
                            else if (eMG.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Staff))
                            {

                            }
                            else
                            {
                                if (eMG.AdultForestC(cell_0).HaveAnyResources)
                                {
                                    if (eMG.HaveFire(cell_0)) eMG.UnitButtonAbilitiesC(cell_0).SetAbility(ButtonTypes.First, AbilityTypes.PutOutFirePawn);
                                    else eMG.UnitButtonAbilitiesC(cell_0).SetAbility(ButtonTypes.First, AbilityTypes.FirePawn);
                                }
                                else
                                {
                                    eMG.UnitButtonAbilitiesC(cell_0).SetAbility(ButtonTypes.First, AbilityTypes.Seed);
                                }



                                if (eMG.BuildingTC(cell_0).HaveBuilding)
                                {
                                    eMG.UnitButtonAbilitiesC(cell_0).SetAbility(ButtonTypes.Second, AbilityTypes.DestroyBuilding);
                                }
                                else
                                {
                                    eMG.UnitButtonAbilitiesC(cell_0).SetAbility(ButtonTypes.Second, AbilityTypes.SetFarm);
                                }

                                //else
                                //{
                                //    if (E.BuildingsInfo(E.UnitPlayerTC(Idx).Player, LevelTypes.First, BuildingTypes.City).IdxC.HaveAny)
                                //    {
                                //        E.UnitEs(Idx).Ability(ButtonTypes.Fourth).Reset();
                                //    }
                                //    else
                                //    {
                                //        E.UnitEs(Idx).Ability(ButtonTypes.Fourth, AbilityTypes.SetCity;
                                //    }
                                //}
                            }

                            break;

                        case UnitTypes.Elfemale:
                            eMG.UnitButtonAbilitiesC(cell_0).SetAbility(ButtonTypes.First, AbilityTypes.StunElfemale);
                            eMG.UnitButtonAbilitiesC(cell_0).SetAbility(ButtonTypes.Second, AbilityTypes.GrowAdultForest);

                            break;

                        case UnitTypes.Snowy:
                            eMG.UnitButtonAbilitiesC(cell_0).SetAbility(ButtonTypes.First, AbilityTypes.IncreaseWindSnowy);
                            eMG.UnitButtonAbilitiesC(cell_0).SetAbility(ButtonTypes.Second, AbilityTypes.DecreaseWindSnowy);
                            eMG.UnitButtonAbilitiesC(cell_0).SetAbility(ButtonTypes.Third, AbilityTypes.ChangeDirectionWind);
                            //E.UnitEs(Idx).Ability(ButtonTypes.Third, AbilityTypes.IceWall;
                            break;

                        case UnitTypes.Undead:
                            eMG.UnitButtonAbilitiesC(cell_0).SetAbility(ButtonTypes.First, AbilityTypes.Resurrect);
                            eMG.UnitButtonAbilitiesC(cell_0).SetAbility(ButtonTypes.Second, AbilityTypes.SetTeleport);
                            eMG.UnitButtonAbilitiesC(cell_0).SetAbility(ButtonTypes.Third, AbilityTypes.InvokeSkeletons);
                            if (eMG.BuildingTC(cell_0).HaveBuilding) eMG.UnitButtonAbilitiesC(cell_0).SetAbility(ButtonTypes.Fourth, AbilityTypes.DestroyBuilding);
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
                        eMG.UnitButtonAbilitiesC(cell_0).SetAbility(ButtonTypes.Fifth, AbilityTypes.Teleport);
                    }

                }
            }
        }
    }
}