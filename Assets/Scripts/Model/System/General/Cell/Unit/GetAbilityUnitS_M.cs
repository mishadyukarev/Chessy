using Chessy.Common;
using Chessy.Model.Values;
using System;

namespace Chessy.Model
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        internal void GetAbilityUnit()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _e.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.First, AbilityTypes.None);
                _e.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Second, AbilityTypes.None);
                _e.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Third, AbilityTypes.None);
                _e.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Fourth, AbilityTypes.None);
                _e.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Fifth, AbilityTypes.None);


                if (_e.UnitT(cellIdxCurrent).HaveUnit())
                {
                    switch (_e.UnitT(cellIdxCurrent))
                    {
                        case UnitTypes.None: throw new Exception();

                        case UnitTypes.King:
                            _e.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.First, AbilityTypes.CircularAttack);
                            _e.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Fourth, AbilityTypes.KingPassiveNearBonus);
                            break;

                        case UnitTypes.Pawn:

                            if (_e.MainToolWeaponT(cellIdxCurrent).Is(ToolWeaponTypes.BowCrossbow))
                            {
                                _e.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.First, AbilityTypes.FireArcher);
                                _e.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Second, AbilityTypes.ChangeCornerArcher);
                            }
                            else if (_e.MainToolWeaponT(cellIdxCurrent).Is(ToolWeaponTypes.Staff))
                            {

                            }
                            else
                            {
                                _e.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.First, AbilityTypes.Seed);

                                if (_e.HaveBuildingOnCell(cellIdxCurrent))
                                {
                                    _e.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Second, AbilityTypes.DestroyBuilding);
                                }
                                else
                                {
                                    _e.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Second, AbilityTypes.SetFarm);
                                }

                                if (_e.HaveFire(cellIdxCurrent)) _e.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Third, AbilityTypes.PutOutFirePawn);
                                else _e.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Third, AbilityTypes.FirePawn);






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
                            _e.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.First, AbilityTypes.StunElfemale);
                            _e.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Second, AbilityTypes.GrowAdultForest);

                            break;

                        case UnitTypes.Snowy:
                            _e.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.First, AbilityTypes.IncreaseWindSnowy);
                            _e.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Second, AbilityTypes.DecreaseWindSnowy);
                            _e.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Third, AbilityTypes.ChangeDirectionWind);
                            //E.UnitEs(Idx).Ability(ButtonTypes.Third, AbilityTypes.IceWall;
                            break;

                        case UnitTypes.Undead:
                            _e.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.First, AbilityTypes.Resurrect);
                            _e.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Second, AbilityTypes.SetTeleport);
                            _e.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Third, AbilityTypes.InvokeSkeletons);
                            if (_e.HaveBuildingOnCell(cellIdxCurrent)) _e.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Fourth, AbilityTypes.DestroyBuilding);
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

                    if (_e.IsBuildingOnCell(cellIdxCurrent, BuildingTypes.Teleport))
                    {
                        _e.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Fifth, AbilityTypes.Teleport);
                    }
                }
            }
        }
    }
}