using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    sealed class GetDamageUnitsS : SystemAbstract, IEcsRunSystem
    {
        internal GetDamageUnitsS(in EntitiesModel ents) : base(ents)
        {

        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.UnitTC(idx_0).HaveUnit)
                {
                    var standDamage = E.UnitInfo(E.UnitPlayerTC(idx_0).Player, E.UnitLevelTC(idx_0).Level, E.UnitTC(idx_0).Unit).DamageStandart;
                    float powerDamage = standDamage;

                    if (E.UnitMainTWTC(idx_0).HaveToolWeapon)
                    {
                        if (E.UnitMainTWLevelTC(idx_0).Is(LevelTypes.Second))
                        {
                            if (E.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                            {
                                powerDamage += standDamage * UnitDamage_Values.BOW_CROSSBOW_SECOND;
                            }
                            else if (E.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Axe))
                            {
                                powerDamage += standDamage * UnitDamage_Values.AXE_SECOND;
                            }
                        }
                    }
                    if (E.UnitExtraTWTC(idx_0).Is(ToolWeaponTypes.Sword)) powerDamage += standDamage * UnitDamage_Values.SWORD;

                    E.UnitDamageAttackC(idx_0).Damage = powerDamage;


                    if (E.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Protected))
                    {
                        powerDamage += standDamage * UnitDamage_Values.PROTECTED;
                    }
                    else if (E.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Relaxed))
                    {
                        powerDamage += standDamage * UnitDamage_Values.RELAXED;
                    }

                    if (E.BuildTC(idx_0).HaveBuilding)
                    {
                        var p = 0f;

                        switch (E.BuildTC(idx_0).Building)
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


                        powerDamage += standDamage * p;
                    }

                    float protectionPercent = 0;

                    if (E.FertilizeC(idx_0).HaveAnyResources) protectionPercent += UnitDamage_Values.FERTILIZER;
                    if (E.AdultForestC(idx_0).HaveAnyResources) protectionPercent += UnitDamage_Values.ADULT_FOREST;
                    if (E.HillC(idx_0).HaveAnyResources) protectionPercent += UnitDamage_Values.HILL;

                    powerDamage += standDamage * protectionPercent;

                    E.UnitDamageOnCellC(idx_0).Damage = powerDamage;
                }

            }
        }
    }
}