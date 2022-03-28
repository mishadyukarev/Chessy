using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Unit;
using System;

namespace Chessy.Game.System.Model
{
    sealed class GetDamageUnitsS : SystemModelGameAbs
    {
        internal GetDamageUnitsS(in EntitiesModelGame eMGame) : base(eMGame) { }

        internal void Get(in byte cell_0)
        {
            var powerDamage = 0f;


            ref var unitTC = ref eMGame.UnitTC(cell_0);

            if (eMGame.UnitTC(cell_0).HaveUnit)
            {
                switch (eMGame.UnitLevelTC(cell_0).Level)
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

                if (eMGame.PlayerInfoE(eMGame.UnitPlayerTC(cell_0).Player).WhereKingEffects.Contains(cell_0)) powerDamage *= 1.25f;


                if (eMGame.PlayerInfoE(eMGame.UnitPlayerTC(cell_0).Player).MyHeroTC.Is(UnitTypes.Hell))
                {
                    if (unitTC.Is(UnitTypes.Pawn))
                    {
                        powerDamage *= 1.5f;
                    }
                }


                if (eMGame.UnitMainTWTC(cell_0).HaveToolWeapon)
                {
                    if (eMGame.UnitMainTWLevelTC(cell_0).Is(LevelTypes.Second))
                    {
                        if (eMGame.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.BowCrossbow))
                        {
                            powerDamage += powerDamage * DamageValues.BOW_CROSSBOW_SECOND;
                        }
                        else if (eMGame.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Axe))
                        {
                            powerDamage += powerDamage * DamageValues.AXE_SECOND;
                        }
                    }
                }
                if (eMGame.UnitExtraTWTC(cell_0).Is(ToolWeaponTypes.Sword)) powerDamage += powerDamage * DamageValues.SWORD;

                if (eMGame.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Staff)) powerDamage /= 2;


                eMGame.DamageAttackC(cell_0).Damage = powerDamage;




                if (eMGame.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Protected))
                {
                    powerDamage += powerDamage * DamageValues.PROTECTED;
                }
                else if (eMGame.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Relaxed))
                {
                    powerDamage += powerDamage * DamageValues.RELAXED;
                }

                if (eMGame.BuildingTC(cell_0).HaveBuilding)
                {
                    var p = 0f;

                    switch (eMGame.BuildingTC(cell_0).Building)
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

                if (eMGame.FertilizeC(cell_0).HaveAnyResources) protectionPercent += DamageValues.FERTILIZER;
                if (eMGame.AdultForestC(cell_0).HaveAnyResources) protectionPercent += DamageValues.ADULT_FOREST;
                if (eMGame.HillC(cell_0).HaveAnyResources) protectionPercent += DamageValues.HILL;

                powerDamage += powerDamage * protectionPercent;


                if (eMGame.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Staff)) powerDamage /= 2;

                eMGame.DamageOnCellC(cell_0).Damage = powerDamage;
            }
        } }
}