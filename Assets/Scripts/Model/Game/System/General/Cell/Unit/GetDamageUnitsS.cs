using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit;
using System;

namespace Chessy.Game.Model.System
{
    sealed partial class GetDataCellsAfterAnyDoingS_M : SystemModel
    {
        internal void GetDamageUnits()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                var powerDamage = 0f;


                ref var unitTC = ref _eMG.UnitTC(cellIdxCurrent);

                if (_eMG.UnitTC(cellIdxCurrent).HaveUnit)
                {
                    switch (_eMG.UnitLevelTC(cellIdxCurrent).LevelT)
                    {
                        case LevelTypes.First:
                            switch (unitTC.UnitT)
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

                    if (_eMG.PlayerInfoE(_eMG.UnitPlayerTC(cellIdxCurrent).PlayerT).WhereKingEffects.Contains(cellIdxCurrent))
                    {
                        powerDamage *= DamageUnitValues.KING_EFFECT_ON_NEAR_UNITS;
                    }


                    if (_eMG.MainToolWeaponTC(cellIdxCurrent).HaveToolWeapon)
                    {
                        if (_eMG.MainTWLevelTC(cellIdxCurrent).Is(LevelTypes.Second))
                        {
                            if (_eMG.MainToolWeaponTC(cellIdxCurrent).Is(ToolWeaponTypes.BowCrossbow))
                            {
                                powerDamage += DamageUnitValues.BOW_CROSSBOW_SECOND_ADDING;
                            }
                        }
                    }
                    if (_eMG.ExtraToolWeaponTC(cellIdxCurrent).Is(ToolWeaponTypes.Sword)) powerDamage += DamageUnitValues.SWORD_ADDING;

                    if (_eMG.MainToolWeaponTC(cellIdxCurrent).Is(ToolWeaponTypes.Staff)) powerDamage -= DamageUnitValues.STAFF_EFFECT_ON_PAWN_TAKING;


                    _eMG.DamageAttackC(cellIdxCurrent).Damage = powerDamage;




                    if (_eMG.UnitConditionTC(cellIdxCurrent).Is(ConditionUnitTypes.Protected))
                    {
                        powerDamage += powerDamage * DamageUnitValues.PROTECTED;
                    }
                    else if (_eMG.UnitConditionTC(cellIdxCurrent).Is(ConditionUnitTypes.Relaxed))
                    {
                        powerDamage += powerDamage * DamageUnitValues.RELAXED;
                    }

                    if (_eMG.BuildingTC(cellIdxCurrent).HaveBuilding)
                    {
                        var p = 0f;

                        switch (_eMG.BuildingTC(cellIdxCurrent).BuildingT)
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
                    if (_eMG.AdultForestC(cellIdxCurrent).HaveAnyResources) protectionPercent += DamageUnitValues.ADULT_FOREST;
                    if (_eMG.HillC(cellIdxCurrent).HaveAnyResources) protectionPercent += DamageUnitValues.HILL;

                    powerDamage += powerDamage * protectionPercent;

                    _eMG.DamageOnCellC(cellIdxCurrent).Damage = powerDamage;
                }
            }
        }
    }
}