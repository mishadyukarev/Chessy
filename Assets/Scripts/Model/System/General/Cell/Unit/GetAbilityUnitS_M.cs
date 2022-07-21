using Chessy.Model.Component;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using System;

namespace Chessy.Model.System
{
    sealed class GetAbilityUnitS_M : SystemModelAbstract
    {
        internal GetAbilityUnitS_M(in SystemsModel sM, EntitiesModel eM) : base(sM, eM)
        {
        }

        internal void GetAbilityUnit()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_cellCs[cellIdxCurrent].IsBorder) continue;

                _buttonsAbilitiesUnitCs[cellIdxCurrent].SetAbility(ButtonTypes.First, AbilityTypes.None);
                _buttonsAbilitiesUnitCs[cellIdxCurrent].SetAbility(ButtonTypes.Second, AbilityTypes.None);
                _buttonsAbilitiesUnitCs[cellIdxCurrent].SetAbility(ButtonTypes.Third, AbilityTypes.None);
                _buttonsAbilitiesUnitCs[cellIdxCurrent].SetAbility(ButtonTypes.Fourth, AbilityTypes.None);
                _buttonsAbilitiesUnitCs[cellIdxCurrent].SetAbility(ButtonTypes.Fifth, AbilityTypes.None);


                if (_unitCs[cellIdxCurrent].HaveUnit)
                {
                    switch (_unitCs[cellIdxCurrent].UnitT)
                    {
                        case UnitTypes.None: throw new Exception();

                        case UnitTypes.King:
                            _buttonsAbilitiesUnitCs[cellIdxCurrent].SetAbility(ButtonTypes.First, AbilityTypes.CircularAttack);
                            _buttonsAbilitiesUnitCs[cellIdxCurrent].SetAbility(ButtonTypes.Fourth, AbilityTypes.KingPassiveNearBonus);
                            break;

                        case UnitTypes.Pawn:

                            if (_mainTWC[cellIdxCurrent].ToolWeaponT == ToolsWeaponsWarriorTypes.BowCrossbow)
                            {
                                _buttonsAbilitiesUnitCs[cellIdxCurrent].SetAbility(ButtonTypes.First, AbilityTypes.FireArcher);
                                _buttonsAbilitiesUnitCs[cellIdxCurrent].SetAbility(ButtonTypes.Second, AbilityTypes.ChangeCornerArcher);
                            }
                            else if (_mainTWC[cellIdxCurrent].ToolWeaponT == ToolsWeaponsWarriorTypes.Staff)
                            {

                            }
                            else
                            {
                                _buttonsAbilitiesUnitCs[cellIdxCurrent].SetAbility(ButtonTypes.First, AbilityTypes.Seed);

                                if (_buildingCs[cellIdxCurrent].HaveBuilding)
                                {
                                    _buttonsAbilitiesUnitCs[cellIdxCurrent].SetAbility(ButtonTypes.Second, AbilityTypes.DestroyBuilding);
                                }
                                else
                                {
                                    _buttonsAbilitiesUnitCs[cellIdxCurrent].SetAbility(ButtonTypes.Second, AbilityTypes.SetFarm);
                                }

                                if (_fireCs[cellIdxCurrent].HaveFire) _buttonsAbilitiesUnitCs[cellIdxCurrent].SetAbility(ButtonTypes.Third, AbilityTypes.PutOutFirePawn);
                                else _buttonsAbilitiesUnitCs[cellIdxCurrent].SetAbility(ButtonTypes.Third, AbilityTypes.FirePawn);






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
                            _buttonsAbilitiesUnitCs[cellIdxCurrent].SetAbility(ButtonTypes.First, AbilityTypes.StunElfemale);
                            _buttonsAbilitiesUnitCs[cellIdxCurrent].SetAbility(ButtonTypes.Second, AbilityTypes.GrowAdultForest);

                            break;

                        case UnitTypes.Snowy:
                            _buttonsAbilitiesUnitCs[cellIdxCurrent].SetAbility(ButtonTypes.First, AbilityTypes.IncreaseWindSnowy);
                            _buttonsAbilitiesUnitCs[cellIdxCurrent].SetAbility(ButtonTypes.Second, AbilityTypes.DecreaseWindSnowy);
                            _buttonsAbilitiesUnitCs[cellIdxCurrent].SetAbility(ButtonTypes.Third, AbilityTypes.ChangeDirectionWind);
                            //E.UnitEs(Idx).Ability(ButtonTypes.Third, AbilityTypes.IceWall;
                            break;

                        case UnitTypes.Undead:
                            _buttonsAbilitiesUnitCs[cellIdxCurrent].SetAbility(ButtonTypes.First, AbilityTypes.Resurrect);
                            _buttonsAbilitiesUnitCs[cellIdxCurrent].SetAbility(ButtonTypes.Second, AbilityTypes.SetTeleport);
                            _buttonsAbilitiesUnitCs[cellIdxCurrent].SetAbility(ButtonTypes.Third, AbilityTypes.InvokeSkeletons);
                            if (_buildingCs[cellIdxCurrent].HaveBuilding) _buttonsAbilitiesUnitCs[cellIdxCurrent].SetAbility(ButtonTypes.Fourth, AbilityTypes.DestroyBuilding);
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

                    if (_buildingCs[cellIdxCurrent].BuildingT == BuildingTypes.Teleport)
                    {
                        _buttonsAbilitiesUnitCs[cellIdxCurrent].SetAbility(ButtonTypes.Fifth, AbilityTypes.Teleport);
                    }
                }
            }
        }
    }
}