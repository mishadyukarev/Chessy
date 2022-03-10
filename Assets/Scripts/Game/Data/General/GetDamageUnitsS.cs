using Chessy.Game.Values.Cell.Unit;
using System;

namespace Chessy.Game.System.Model
{
    sealed class GetDamageUnitsS : SystemAbstract, IEcsRunSystem
    {
        internal GetDamageUnitsS(in EntitiesModel eM) : base(eM) { }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                if (E.UnitTC(idx_0).HaveUnit)
                {
                    var powerDamage = 0f;


                    ref var unitTC = ref E.UnitTC(idx_0);

                    switch (E.UnitLevelTC(idx_0).Level)
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

                                case UnitTypes.Camel:
                                    powerDamage = DamageValues.CAMEL;
                                    break;

                                default: throw new Exception();
                            }
                            break;
                    }

                    if (E.PlayerInfoE(E.UnitPlayerTC(idx_0).Player).WhereKingEffects.Contains(idx_0)) powerDamage *= 1.25f;


                    if (E.PlayerInfoE(E.UnitPlayerTC(idx_0).Player).AvailableHeroTC.Is(UnitTypes.Hell))
                    {
                        if (unitTC.Is(UnitTypes.Pawn))
                        {
                            powerDamage *= 1.5f;
                        }
                    }


                    if (E.UnitMainTWTC(idx_0).HaveToolWeapon)
                    {
                        if (E.UnitLevelTC(idx_0).Is(LevelTypes.Second))
                        {
                            if (E.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                            {
                                powerDamage += powerDamage * DamageValues.BOW_CROSSBOW_SECOND;
                            }
                            else if (E.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Axe))
                            {
                                powerDamage += powerDamage * DamageValues.AXE_SECOND;
                            }
                        }
                    }
                    if (E.UnitExtraTWTC(idx_0).Is(ToolWeaponTypes.Sword)) powerDamage += powerDamage * DamageValues.SWORD;

                    if (E.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Staff)) powerDamage /= 2;


                    E.DamageAttackC(idx_0).Damage = powerDamage;




                    if (E.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Protected))
                    {
                        powerDamage += powerDamage * DamageValues.PROTECTED;
                    }
                    else if (E.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Relaxed))
                    {
                        powerDamage += powerDamage * DamageValues.RELAXED;
                    }

                    if (E.BuildingTC(idx_0).HaveBuilding)
                    {
                        var p = 0f;

                        switch (E.BuildingTC(idx_0).Building)
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

                    if (E.FertilizeC(idx_0).HaveAnyResources) protectionPercent += DamageValues.FERTILIZER;
                    if (E.AdultForestC(idx_0).HaveAnyResources) protectionPercent += DamageValues.ADULT_FOREST;
                    if (E.HillC(idx_0).HaveAnyResources) protectionPercent += DamageValues.HILL;

                    powerDamage += powerDamage * protectionPercent;


                    if (E.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Staff)) powerDamage /= 2;

                    E.DamageOnCellC(idx_0).Damage = powerDamage;
                }
            }
        }
    }
}