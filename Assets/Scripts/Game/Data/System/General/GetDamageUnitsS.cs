using Chessy.Game.Values.Cell.Unit;
using System;

namespace Chessy.Game.System.Model
{
    public struct GetDamageUnitsS
    {
        public GetDamageUnitsS(in byte idx_0, in EntitiesModel e)
        {
            var powerDamage = 0f;


            ref var unitTC = ref e.UnitTC(idx_0);

            if (e.UnitTC(idx_0).HaveUnit)
            {
                switch (e.UnitLevelTC(idx_0).Level)
                {
                    case LevelTypes.First:
                        switch (unitTC.Unit)
                        {
                            case UnitTypes.King:
                                powerDamage = DamageValues.KING;
                                break;

                            case UnitTypes.Pawn:
                                powerDamage = DamageValues.PAWN;
                                break;

                            case UnitTypes.Elfemale:
                                powerDamage = DamageValues.ELFEMALE;
                                break;

                            case UnitTypes.Snowy:
                                powerDamage = DamageValues.SNOWY;
                                break;

                            case UnitTypes.Undead:
                                powerDamage = DamageValues.UNDEAD;
                                break;

                            case UnitTypes.Hell:
                                powerDamage = DamageValues.HELL;
                                break;

                            case UnitTypes.Skeleton:
                                powerDamage = DamageValues.SKELETON;
                                break;

                            case UnitTypes.Tree:
                                powerDamage = DamageValues.TREE;
                                break;

                            case UnitTypes.Wolf:
                                powerDamage = DamageValues.CAMEL;
                                break;

                            default: throw new Exception();
                        }
                        break;
                }

                if (e.PlayerInfoE(e.UnitPlayerTC(idx_0).Player).WhereKingEffects.Contains(idx_0)) powerDamage *= 1.25f;


                if (e.PlayerInfoE(e.UnitPlayerTC(idx_0).Player).AvailableHeroTC.Is(UnitTypes.Hell))
                {
                    if (unitTC.Is(UnitTypes.Pawn))
                    {
                        powerDamage *= 1.5f;
                    }
                }


                if (e.UnitMainTWTC(idx_0).HaveToolWeapon)
                {
                    if (e.UnitMainTWLevelTC(idx_0).Is(LevelTypes.Second))
                    {
                        if (e.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                        {
                            powerDamage += powerDamage * DamageValues.BOW_CROSSBOW_SECOND;
                        }
                        else if (e.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Axe))
                        {
                            powerDamage += powerDamage * DamageValues.AXE_SECOND;
                        }
                    }
                }
                if (e.UnitExtraTWTC(idx_0).Is(ToolWeaponTypes.Sword)) powerDamage += powerDamage * DamageValues.SWORD;

                if (e.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Staff)) powerDamage /= 2;


                e.DamageAttackC(idx_0).Damage = powerDamage;




                if (e.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Protected))
                {
                    powerDamage += powerDamage * DamageValues.PROTECTED;
                }
                else if (e.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Relaxed))
                {
                    powerDamage += powerDamage * DamageValues.RELAXED;
                }

                if (e.BuildingTC(idx_0).HaveBuilding)
                {
                    var p = 0f;

                    switch (e.BuildingTC(idx_0).Building)
                    {
                        case BuildingTypes.City:
                            p = DamageValues.CITY;
                            break;

                        case BuildingTypes.Farm:
                            p = DamageValues.FARM;
                            break;

                        case BuildingTypes.Woodcutter:
                            p = DamageValues.WOODCUTTER;
                            break;

                        default:
                            break;
                    }


                    powerDamage += powerDamage * p;
                }

                float protectionPercent = 0;

                if (e.FertilizeC(idx_0).HaveAnyResources) protectionPercent += DamageValues.FERTILIZER;
                if (e.AdultForestC(idx_0).HaveAnyResources) protectionPercent += DamageValues.ADULT_FOREST;
                if (e.HillC(idx_0).HaveAnyResources) protectionPercent += DamageValues.HILL;

                powerDamage += powerDamage * protectionPercent;


                if (e.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Staff)) powerDamage /= 2;

                e.DamageOnCellC(idx_0).Damage = powerDamage;
            }
        } }
}