using System;

namespace Game.Game
{
    sealed class GetDamageUnitsS : SystemAbstract, IEcsRunSystem
    {
        internal GetDamageUnitsS(in EntitiesModel ents) : base(ents)
        {

        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Start_Values.ALL_CELLS_AMOUNT; idx_0++)
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
                                    powerDamage = UnitDamage_Values.KING;
                                    break;

                                case UnitTypes.Pawn:
                                    powerDamage = UnitDamage_Values.PAWN;
                                    break;

                                case UnitTypes.Scout:
                                    powerDamage = UnitDamage_Values.SCOUT;
                                    break;

                                case UnitTypes.Elfemale:
                                    powerDamage = UnitDamage_Values.ELFEMALE;
                                    break;

                                case UnitTypes.Snowy:
                                    powerDamage = UnitDamage_Values.SNOWY;
                                    break;

                                case UnitTypes.Undead:
                                    powerDamage = UnitDamage_Values.UNDEAD;
                                    break;

                                case UnitTypes.Hell:
                                    powerDamage = UnitDamage_Values.HELL;
                                    break;

                                case UnitTypes.Skeleton:
                                    powerDamage = UnitDamage_Values.SKELETON;
                                    break;

                                case UnitTypes.Camel:
                                    powerDamage = UnitDamage_Values.CAMEL;
                                    break;

                                default: throw new Exception();
                            }
                            break;
                    }

                    if (E.UnitInfo(E.UnitMainE(idx_0)).HaveCenterUpgrade)
                    {
                        if (E.UnitTC(idx_0).Is(UnitTypes.King))
                        {
                            powerDamage += UnitDamage_Values.CENTER_KING_BONUS;
                        }
                        else if (E.UnitTC(idx_0).Is(UnitTypes.Pawn))
                        {
                            powerDamage += UnitDamage_Values.CENTER_PAWN_BONUS;
                        }
                    }


                    if (E.UnitMainTWTC(idx_0).HaveToolWeapon)
                    {
                        if (E.UnitMainTWLevelTC(idx_0).Is(LevelTypes.Second))
                        {
                            if (E.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                            {
                                powerDamage += powerDamage * UnitDamage_Values.BOW_CROSSBOW_SECOND;
                            }
                            else if (E.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Axe))
                            {
                                powerDamage += powerDamage * UnitDamage_Values.AXE_SECOND;
                            }
                        }
                    }
                    if (E.UnitExtraTWTC(idx_0).Is(ToolWeaponTypes.Sword)) powerDamage += powerDamage * UnitDamage_Values.SWORD;

                    E.UnitDamageAttackC(idx_0).Damage = powerDamage;


                    if (E.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Protected))
                    {
                        powerDamage += powerDamage * UnitDamage_Values.PROTECTED;
                    }
                    else if (E.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Relaxed))
                    {
                        powerDamage += powerDamage * UnitDamage_Values.RELAXED;
                    }

                    if (E.BuildingTC(idx_0).HaveBuilding)
                    {
                        var p = 0f;

                        switch (E.BuildingTC(idx_0).Building)
                        {
                            case BuildingTypes.City:
                                p = UnitDamage_Values.CITY;
                                break;

                            case BuildingTypes.House:
                                p = UnitDamage_Values.HOUSE;
                                break;

                            case BuildingTypes.Market:
                                p = UnitDamage_Values.MARKET;
                                break;

                            case BuildingTypes.Smelter:
                                p = UnitDamage_Values.SMELTER;
                                break;

                            case BuildingTypes.Farm:
                                p = UnitDamage_Values.FARM;
                                break;

                            case BuildingTypes.Woodcutter:
                                p = UnitDamage_Values.WOODCUTTER;
                                break;

                            default:
                                break;
                        }


                        powerDamage += powerDamage * p;
                    }

                    float protectionPercent = 0;

                    if (E.FertilizeC(idx_0).HaveAnyResources) protectionPercent += UnitDamage_Values.FERTILIZER;
                    if (E.AdultForestC(idx_0).HaveAnyResources) protectionPercent += UnitDamage_Values.ADULT_FOREST;
                    if (E.HillC(idx_0).HaveAnyResources) protectionPercent += UnitDamage_Values.HILL;

                    powerDamage += powerDamage * protectionPercent;

                    E.UnitDamageOnCellC(idx_0).Damage = powerDamage;
                }
            }
        }
    }
}