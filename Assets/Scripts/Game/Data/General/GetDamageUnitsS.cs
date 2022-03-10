using Chessy.Game.Values.Cell.Unit;
using System;

namespace Chessy.Game.System.Model
{
    sealed class GetDamageUnitsS : CellSystem, IEcsRunSystem
    {
        internal GetDamageUnitsS(in byte idx, in EntitiesModel eM) : base(idx, eM)
        {
        }

        public void Run()
        {
            if (E.UnitTC(Idx).HaveUnit)
            {
                var powerDamage = 0f;


                ref var unitTC = ref E.UnitTC(Idx);

                switch (E.UnitLevelTC(Idx).Level)
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

                if (E.PlayerInfoE(E.UnitPlayerTC(Idx).Player).WhereKingEffects.Contains(Idx)) powerDamage *= 1.25f;


                if (E.PlayerInfoE(E.UnitPlayerTC(Idx).Player).AvailableHeroTC.Is(UnitTypes.Hell))
                {
                    if (unitTC.Is(UnitTypes.Pawn))
                    {
                        powerDamage *= 1.5f;
                    }
                }


                if (E.UnitMainTWTC(Idx).HaveToolWeapon)
                {
                    if (E.UnitLevelTC(Idx).Is(LevelTypes.Second))
                    {
                        if (E.UnitMainTWTC(Idx).Is(ToolWeaponTypes.BowCrossbow))
                        {
                            powerDamage += powerDamage * DamageValues.BOW_CROSSBOW_SECOND;
                        }
                        else if (E.UnitMainTWTC(Idx).Is(ToolWeaponTypes.Axe))
                        {
                            powerDamage += powerDamage * DamageValues.AXE_SECOND;
                        }
                    }
                }
                if (E.UnitExtraTWTC(Idx).Is(ToolWeaponTypes.Sword)) powerDamage += powerDamage * DamageValues.SWORD;

                if (E.UnitMainTWTC(Idx).Is(ToolWeaponTypes.Staff)) powerDamage /= 2;


                E.DamageAttackC(Idx).Damage = powerDamage;




                if (E.UnitConditionTC(Idx).Is(ConditionUnitTypes.Protected))
                {
                    powerDamage += powerDamage * DamageValues.PROTECTED;
                }
                else if (E.UnitConditionTC(Idx).Is(ConditionUnitTypes.Relaxed))
                {
                    powerDamage += powerDamage * DamageValues.RELAXED;
                }

                if (E.BuildingTC(Idx).HaveBuilding)
                {
                    var p = 0f;

                    switch (E.BuildingTC(Idx).Building)
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

                if (E.FertilizeC(Idx).HaveAnyResources) protectionPercent += DamageValues.FERTILIZER;
                if (E.AdultForestC(Idx).HaveAnyResources) protectionPercent += DamageValues.ADULT_FOREST;
                if (E.HillC(Idx).HaveAnyResources) protectionPercent += DamageValues.HILL;

                powerDamage += powerDamage * protectionPercent;


                if (E.UnitMainTWTC(Idx).Is(ToolWeaponTypes.Staff)) powerDamage /= 2;

                E.DamageOnCellC(Idx).Damage = powerDamage;
            }
        }
    }
}