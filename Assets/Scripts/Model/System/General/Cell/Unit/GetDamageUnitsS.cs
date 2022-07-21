using Chessy.Model.Values;
using System;

namespace Chessy.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModelAbstract
    {
        internal void GetDamageUnits()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                var powerDamage = 0f;

                var curUnitT_0 = _unitCs[cellIdxCurrent].UnitT;

                if (curUnitT_0.HaveUnit())
                {
                    switch (_unitCs[cellIdxCurrent].LevelT)
                    {
                        case LevelTypes.First:
                            switch (curUnitT_0)
                            {
                                case UnitTypes.King:
                                    powerDamage = DamageUnitValues.KING_DAMAGE;
                                    break;

                                case UnitTypes.Pawn:
                                    powerDamage = DamageUnitValues.PAWN_DAMAGE;
                                    break;

                                case UnitTypes.Elfemale:
                                    powerDamage = DamageUnitValues.ELFEMALE;
                                    break;

                                case UnitTypes.Snowy:
                                    powerDamage = DamageUnitValues.SNOWY;
                                    break;

                                case UnitTypes.Undead:
                                    powerDamage = DamageUnitValues.UNDEAD;
                                    break;

                                case UnitTypes.Hell:
                                    powerDamage = DamageUnitValues.HELL;
                                    break;

                                case UnitTypes.Skeleton:
                                    powerDamage = DamageUnitValues.SKELETON;
                                    break;

                                case UnitTypes.Tree:
                                    powerDamage = DamageUnitValues.TREE;
                                    break;

                                case UnitTypes.Wolf:
                                    powerDamage = DamageUnitValues.CAMEL;
                                    break;

                                default: throw new Exception();
                            }
                            break;
                    }



                    if (_hasUnitKingEffectHereCs[cellIdxCurrent].Has(_unitCs[cellIdxCurrent].PlayerT))//Separate player effect
                    {
                        powerDamage *= DamageUnitValues.KING_EFFECT_ON_NEAR_UNITS;
                    }


                    if (_mainTWC[cellIdxCurrent].ToolWeaponT.HaveToolWeapon())
                    {
                        if (_mainTWC[cellIdxCurrent].LevelT == LevelTypes.Second)
                        {
                            if (_mainTWC[cellIdxCurrent].ToolWeaponT == ToolsWeaponsWarriorTypes.BowCrossbow)
                            {
                                powerDamage += DamageUnitValues.BOW_CROSSBOW_SECOND_ADDING;
                            }
                        }
                    }
                    if (_extraTWC[cellIdxCurrent].ToolWeaponT == ToolsWeaponsWarriorTypes.Sword) powerDamage += DamageUnitValues.SWORD_ADDING;

                    if (_mainTWC[cellIdxCurrent].ToolWeaponT == ToolsWeaponsWarriorTypes.Staff) powerDamage -= DamageUnitValues.STAFF_EFFECT_ON_PAWN_TAKING;


                    _unitCs[cellIdxCurrent].DamageSimpleAttack = powerDamage;




                    if (_unitCs[cellIdxCurrent].ConditionT == ConditionUnitTypes.Protected)
                    {
                        powerDamage += powerDamage * DamageUnitValues.PROTECTED;
                    }
                    else if (_unitCs[cellIdxCurrent].ConditionT == ConditionUnitTypes.Relaxed)
                    {
                        powerDamage += powerDamage * DamageUnitValues.RELAXED;
                    }

                    if (_buildingCs[cellIdxCurrent].HaveBuilding)
                    {
                        var p = 0f;

                        switch (_buildingCs[cellIdxCurrent].BuildingT)
                        {
                            case BuildingTypes.Farm:
                                p = DamageUnitValues.FARM;
                                break;

                            case BuildingTypes.Woodcutter:
                                p = DamageUnitValues.WOODCUTTER;
                                break;

                            default:
                                break;
                        }


                        powerDamage += powerDamage * p;
                    }

                    float protectionPercent = 0;

                    //if (e.FertilizeC(cell_0].HaveEnvironment(EnvironmentTypes.AdultForest)) protectionPercent += DamageValues.FERTILIZER;
                    if (_environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.AdultForest)) protectionPercent += DamageUnitValues.ADULT_FOREST;
                    if (_environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.Hill)) protectionPercent += DamageUnitValues.HILL;

                    powerDamage += powerDamage * protectionPercent;

                    _unitCs[cellIdxCurrent].DamageOnCell = powerDamage;
                }
            }
        }
    }
}