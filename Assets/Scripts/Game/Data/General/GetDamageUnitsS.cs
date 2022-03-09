using Chessy.Game.Values;
using System;

namespace Chessy.Game
{
    sealed class GetDamageUnitsS : SystemAbstract, IEcsRunSystem
    {
        internal GetDamageUnitsS(in EntitiesModel ents) : base(ents)
        {

        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < StartValues.ALL_CELLS_AMOUNT; idx_0++)
            {
                if (E.UnitTC(idx_0).HaveUnit)
                {
                    var powerDamage = 0f;

                    switch (E.UnitLevelTC(idx_0).Level)
                    {
                        case LevelTypes.First:
                            switch (E.UnitTC(idx_0).Unit)
                            {
                                case UnitTypes.King:
                                    powerDamage = UNIT_DAMAGE_VALUES.KING;
                                    break;

                                case UnitTypes.Pawn:
                                    powerDamage = UNIT_DAMAGE_VALUES.PAWN;
                                    break;

                                case UnitTypes.Elfemale:
                                    powerDamage = UNIT_DAMAGE_VALUES.ELFEMALE;
                                    break;

                                case UnitTypes.Snowy:
                                    powerDamage = UNIT_DAMAGE_VALUES.SNOWY;
                                    break;

                                case UnitTypes.Undead:
                                    powerDamage = UNIT_DAMAGE_VALUES.UNDEAD;
                                    break;

                                case UnitTypes.Hell:
                                    powerDamage = UNIT_DAMAGE_VALUES.HELL;
                                    break;

                                case UnitTypes.Skeleton:
                                    powerDamage = UNIT_DAMAGE_VALUES.SKELETON;
                                    break;

                                case UnitTypes.Camel:
                                    powerDamage = UNIT_DAMAGE_VALUES.CAMEL;
                                    break;

                                default: throw new Exception();
                            }
                            break;
                    }


                    if (E.UnitEffectsE(idx_0).HaveKingEffect)
                    {
                        powerDamage *= 1.25f;
                    }


                    if (E.PlayerE(E.UnitPlayerTC(idx_0).Player).AvailableHeroTC.Is(UnitTypes.Hell))
                    {
                        if (E.UnitTC(idx_0).Is(UnitTypes.Pawn))
                        {
                            powerDamage *= 1.5f;
                        }
                    }


                    if (E.UnitMainTWTC(idx_0).HaveToolWeapon)
                    {
                        if (E.UnitMainTWLevelTC(idx_0).Is(LevelTypes.Second))
                        {
                            if (E.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                            {
                                powerDamage += powerDamage * UNIT_DAMAGE_VALUES.BOW_CROSSBOW_SECOND;
                            }
                            else if (E.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Axe))
                            {
                                powerDamage += powerDamage * UNIT_DAMAGE_VALUES.AXE_SECOND;
                            }
                        }
                    }
                    if (E.UnitExtraTWTC(idx_0).Is(ToolWeaponTypes.Sword)) powerDamage += powerDamage * UNIT_DAMAGE_VALUES.SWORD;

                    E.UnitDamageAttackC(idx_0).Damage = powerDamage;


                    if (E.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Protected))
                    {
                        powerDamage += powerDamage * UNIT_DAMAGE_VALUES.PROTECTED;
                    }
                    else if (E.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Relaxed))
                    {
                        powerDamage += powerDamage * UNIT_DAMAGE_VALUES.RELAXED;
                    }

                    if (E.BuildingTC(idx_0).HaveBuilding)
                    {
                        var p = 0f;

                        switch (E.BuildingTC(idx_0).Building)
                        {
                            case BuildingTypes.City:
                                p = UNIT_DAMAGE_VALUES.CITY;
                                break;

                            case BuildingTypes.Farm:
                                p = UNIT_DAMAGE_VALUES.FARM;
                                break;

                            case BuildingTypes.Woodcutter:
                                p = UNIT_DAMAGE_VALUES.WOODCUTTER;
                                break;

                            default:
                                break;
                        }


                        powerDamage += powerDamage * p;
                    }

                    float protectionPercent = 0;

                    if (E.FertilizeC(idx_0).HaveAnyResources) protectionPercent += UNIT_DAMAGE_VALUES.FERTILIZER;
                    if (E.AdultForestC(idx_0).HaveAnyResources) protectionPercent += UNIT_DAMAGE_VALUES.ADULT_FOREST;
                    if (E.HillC(idx_0).HaveAnyResources) protectionPercent += UNIT_DAMAGE_VALUES.HILL;

                    powerDamage += powerDamage * protectionPercent;


                    if (E.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Staff))
                    {
                        powerDamage /= 2;
                    }

                    E.UnitDamageOnCellC(idx_0).Damage = powerDamage;
                }
            }
        }
    }
}