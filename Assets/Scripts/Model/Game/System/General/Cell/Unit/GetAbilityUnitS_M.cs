using Chessy.Common;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using System;

namespace Chessy.Game.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        internal void GetAbilityUnit()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                _eMG.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.First, AbilityTypes.None);
                _eMG.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Second, AbilityTypes.None);
                _eMG.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Third, AbilityTypes.None);
                _eMG.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Fourth, AbilityTypes.None);
                _eMG.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Fifth, AbilityTypes.None);


                if (_eMG.UnitTC(cellIdxCurrent).HaveUnit)
                {
                    switch (_eMG.UnitTC(cellIdxCurrent).UnitT)
                    {
                        case UnitTypes.None: throw new Exception();

                        case UnitTypes.King:
                            _eMG.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.First, AbilityTypes.CircularAttack);
                            _eMG.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Fourth, AbilityTypes.KingPassiveNearBonus);
                            break;

                        case UnitTypes.Pawn:

                            if (_eMG.MainToolWeaponTC(cellIdxCurrent).Is(ToolWeaponTypes.BowCrossbow))
                            {
                                _eMG.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.First, AbilityTypes.FireArcher);
                                _eMG.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Second, AbilityTypes.ChangeCornerArcher);
                            }
                            else if (_eMG.MainToolWeaponTC(cellIdxCurrent).Is(ToolWeaponTypes.Staff))
                            {

                            }
                            else
                            {
                                _eMG.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.First, AbilityTypes.Seed);

                                if (_eMG.BuildingTC(cellIdxCurrent).HaveBuilding)
                                {
                                    _eMG.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Second, AbilityTypes.DestroyBuilding);
                                }
                                else
                                {
                                    _eMG.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Second, AbilityTypes.SetFarm);
                                }

                                if (_eMG.HaveFire(cellIdxCurrent)) _eMG.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Third, AbilityTypes.PutOutFirePawn);
                                else _eMG.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Third, AbilityTypes.FirePawn);






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
                            _eMG.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.First, AbilityTypes.StunElfemale);
                            _eMG.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Second, AbilityTypes.GrowAdultForest);

                            break;

                        case UnitTypes.Snowy:
                            _eMG.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.First, AbilityTypes.IncreaseWindSnowy);
                            _eMG.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Second, AbilityTypes.DecreaseWindSnowy);
                            _eMG.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Third, AbilityTypes.ChangeDirectionWind);
                            //E.UnitEs(Idx).Ability(ButtonTypes.Third, AbilityTypes.IceWall;
                            break;

                        case UnitTypes.Undead:
                            _eMG.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.First, AbilityTypes.Resurrect);
                            _eMG.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Second, AbilityTypes.SetTeleport);
                            _eMG.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Third, AbilityTypes.InvokeSkeletons);
                            if (_eMG.BuildingTC(cellIdxCurrent).HaveBuilding) _eMG.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Fourth, AbilityTypes.DestroyBuilding);
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

                    if (_eMG.BuildingTC(cellIdxCurrent).Is(BuildingTypes.Teleport))
                    {
                        _eMG.UnitButtonAbilitiesC(cellIdxCurrent).SetAbility(ButtonTypes.Fifth, AbilityTypes.Teleport);
                    }
                }
            }
        }
    }
}