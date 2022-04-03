using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit;
using System;

namespace Chessy.Game.Model.System
{
    sealed class GetDamageUnitsS : SystemModelGameAbs
    {
        internal GetDamageUnitsS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Get(in byte cell_0)
        {
            var powerDamage = 0f;


            ref var unitTC = ref eMG.UnitTC(cell_0);

            if (eMG.UnitTC(cell_0).HaveUnit)
            {
                switch (eMG.UnitLevelTC(cell_0).LevelT)
                {
                    case LevelTypes.First:
                        switch (unitTC.UnitT)
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

                if (eMG.PlayerInfoE(eMG.UnitPlayerTC(cell_0).PlayerT).WhereKingEffects.Contains(cell_0)) powerDamage *= 1.25f;


                if (eMG.PlayerInfoE(eMG.UnitPlayerTC(cell_0).PlayerT).GodInfoE.UnitTC.Is(UnitTypes.Hell))
                {
                    if (unitTC.Is(UnitTypes.Pawn))
                    {
                        powerDamage *= 1.5f;
                    }
                }


                if (eMG.UnitMainTWTC(cell_0).HaveToolWeapon)
                {
                    if (eMG.UnitMainTWLevelTC(cell_0).Is(LevelTypes.Second))
                    {
                        if (eMG.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.BowCrossbow))
                        {
                            powerDamage += powerDamage * DamageValues.BOW_CROSSBOW_SECOND;
                        }
                        else if (eMG.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Axe))
                        {
                            powerDamage += powerDamage * DamageValues.AXE_SECOND;
                        }
                    }
                }
                if (eMG.UnitExtraTWTC(cell_0).Is(ToolWeaponTypes.Sword)) powerDamage += powerDamage * DamageValues.SWORD;

                if (eMG.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Staff)) powerDamage /= 2;


                eMG.DamageAttackC(cell_0).Damage = powerDamage;




                if (eMG.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Protected))
                {
                    powerDamage += powerDamage * DamageValues.PROTECTED;
                }
                else if (eMG.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Relaxed))
                {
                    powerDamage += powerDamage * DamageValues.RELAXED;
                }

                if (eMG.BuildingTC(cell_0).HaveBuilding)
                {
                    var p = 0f;

                    switch (eMG.BuildingTC(cell_0).BuildingT)
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

                //if (e.FertilizeC(cell_0).HaveAnyResources) protectionPercent += DamageValues.FERTILIZER;
                if (eMG.AdultForestC(cell_0).HaveAnyResources) protectionPercent += DamageValues.ADULT_FOREST;
                if (eMG.HillC(cell_0).HaveAnyResources) protectionPercent += DamageValues.HILL;

                powerDamage += powerDamage * protectionPercent;


                if (eMG.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Staff)) powerDamage /= 2;

                eMG.DamageOnCellC(cell_0).Damage = powerDamage;
            }
        }
    }
}