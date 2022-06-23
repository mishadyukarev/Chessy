using Chessy.Model.Values;
using Chessy.Model.Values.Cell.Unit;
using System;

namespace Chessy.Model.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        internal void GetDamageUnits()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                var powerDamage = 0f;

                if (_e.UnitT(cellIdxCurrent).HaveUnit())
                {
                    switch (_e.UnitLevelT(cellIdxCurrent))
                    {
                        case LevelTypes.First:
                            switch (_e.UnitT(cellIdxCurrent))
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

                    if (_e.PlayerInfoE(_e.UnitPlayerT(cellIdxCurrent)).WhereKingEffects.Contains(cellIdxCurrent))
                    {
                        powerDamage *= DamageUnitValues.KING_EFFECT_ON_NEAR_UNITS;
                    }


                    if (_e.MainToolWeaponT(cellIdxCurrent).HaveToolWeapon())
                    {
                        if (_e.MainTWLevelT(cellIdxCurrent).Is(LevelTypes.Second))
                        {
                            if (_e.MainToolWeaponT(cellIdxCurrent).Is(ToolWeaponTypes.BowCrossbow))
                            {
                                powerDamage += DamageUnitValues.BOW_CROSSBOW_SECOND_ADDING;
                            }
                        }
                    }
                    if (_e.ExtraToolWeaponT(cellIdxCurrent).Is(ToolWeaponTypes.Sword)) powerDamage += DamageUnitValues.SWORD_ADDING;

                    if (_e.MainToolWeaponT(cellIdxCurrent).Is(ToolWeaponTypes.Staff)) powerDamage -= DamageUnitValues.STAFF_EFFECT_ON_PAWN_TAKING;


                    _e.DamageAttackC(cellIdxCurrent).Damage = powerDamage;




                    if (_e.UnitConditionT(cellIdxCurrent).Is(ConditionUnitTypes.Protected))
                    {
                        powerDamage += powerDamage * DamageUnitValues.PROTECTED;
                    }
                    else if (_e.UnitConditionT(cellIdxCurrent).Is(ConditionUnitTypes.Relaxed))
                    {
                        powerDamage += powerDamage * DamageUnitValues.RELAXED;
                    }

                    if (_e.HaveBuildingOnCell(cellIdxCurrent))
                    {
                        var p = 0f;

                        switch (_e.BuildingOnCellT(cellIdxCurrent))
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

                    //if (e.FertilizeC(cell_0).HaveAnyResources) protectionPercent += DamageValues.FERTILIZER;
                    if (_e.AdultForestC(cellIdxCurrent).HaveAnyResources) protectionPercent += DamageUnitValues.ADULT_FOREST;
                    if (_e.HillC(cellIdxCurrent).HaveAnyResources) protectionPercent += DamageUnitValues.HILL;

                    powerDamage += powerDamage * protectionPercent;

                    _e.DamageOnCellC(cellIdxCurrent).Damage = powerDamage;
                }
            }
        }
    }
}