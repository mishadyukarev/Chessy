using Chessy.Model.Entity;
using Chessy.Model.Values;
using System;

namespace Chessy.Model.System
{
    sealed class GetButtonAbilitiesUnitsS_M : SystemModelAbstract
    {
        internal GetButtonAbilitiesUnitsS_M(in SystemsModel sM, EntitiesModel eM) : base(sM, eM)
        {
        }

        internal void GetAbilityUnit()
        {
            for (byte currentCellIdx_0 = 0; currentCellIdx_0 < IndexCellsValues.CELLS; currentCellIdx_0++)
            {
                if (cellCs[currentCellIdx_0].IsBorder) continue;

                var unitButtonC_0 = UnitButtonsC(currentCellIdx_0);

                for (var buttonT = (ButtonTypes)1; buttonT < ButtonTypes.End; buttonT++)
                {
                    unitButtonC_0.SetAbility(buttonT, AbilityTypes.None);
                }


                if (unitCs[currentCellIdx_0].HaveUnit)
                {
                    switch (unitCs[currentCellIdx_0].UnitT)
                    {
                        case UnitTypes.None: throw new Exception();

                        case UnitTypes.King:
                            unitButtonC_0.SetAbility(ButtonTypes.First, AbilityTypes.CircularAttack);
                            unitButtonC_0.SetAbility(ButtonTypes.Fourth, AbilityTypes.KingPassiveNearBonus);
                            break;

                        case UnitTypes.Pawn:

                            if (mainTWC[currentCellIdx_0].ToolWeaponT == ToolsWeaponsWarriorTypes.BowCrossbow)
                            {
                                unitButtonC_0.SetAbility(ButtonTypes.First, AbilityTypes.FireArcher);
                                unitButtonC_0.SetAbility(ButtonTypes.Second, AbilityTypes.ChangeCornerArcher);
                            }
                            else if (mainTWC[currentCellIdx_0].ToolWeaponT == ToolsWeaponsWarriorTypes.Staff)
                            {

                            }
                            else
                            {
                                unitButtonC_0.SetAbility(ButtonTypes.First, AbilityTypes.Seed);

                                if (buildingCs[currentCellIdx_0].HaveBuilding)
                                {
                                    unitButtonC_0.SetAbility(ButtonTypes.Second, AbilityTypes.DestroyBuilding);
                                }
                                else
                                {
                                    unitButtonC_0.SetAbility(ButtonTypes.Second, AbilityTypes.SetFarm);
                                }

                                if (fireCs[currentCellIdx_0].HaveFire) unitButtonC_0.SetAbility(ButtonTypes.Third, AbilityTypes.PutOutFirePawn);
                                else unitButtonC_0.SetAbility(ButtonTypes.Third, AbilityTypes.FirePawn);






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
                            unitButtonC_0.SetAbility(ButtonTypes.First, AbilityTypes.StunElfemale);
                            unitButtonC_0.SetAbility(ButtonTypes.Second, AbilityTypes.GrowAdultForest);

                            break;

                        case UnitTypes.Snowy:
                            unitButtonC_0.SetAbility(ButtonTypes.First, AbilityTypes.IncreaseWindSnowy);
                            unitButtonC_0.SetAbility(ButtonTypes.Second, AbilityTypes.DecreaseWindSnowy);
                            unitButtonC_0.SetAbility(ButtonTypes.Third, AbilityTypes.ChangeDirectionWind);
                            //E.UnitEs(Idx).Ability(ButtonTypes.Third, AbilityTypes.IceWall;
                            break;

                        case UnitTypes.Undead:
                            unitButtonC_0.SetAbility(ButtonTypes.First, AbilityTypes.Resurrect);
                            unitButtonC_0.SetAbility(ButtonTypes.Second, AbilityTypes.SetTeleport);
                            unitButtonC_0.SetAbility(ButtonTypes.Third, AbilityTypes.InvokeSkeletons);
                            if (buildingCs[currentCellIdx_0].HaveBuilding) unitButtonC_0.SetAbility(ButtonTypes.Fourth, AbilityTypes.DestroyBuilding);
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

                    //if (BuildingC(currentCellIdx_0).BuildingT == BuildingTypes.Teleport)
                    //{
                    //    unitButtonC_0.SetAbility(ButtonTypes.Fifth, AbilityTypes.Teleport);
                    //}
                }
            }
        }
    }
}