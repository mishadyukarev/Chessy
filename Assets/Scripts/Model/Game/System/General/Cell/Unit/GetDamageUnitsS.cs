using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit;
using System;

namespace Chessy.Game.Model.System
{
    sealed class GetDamageUnitsS : SystemModel
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

                if (eMG.PlayerInfoE(eMG.UnitPlayerTC(cell_0).PlayerT).WhereKingEffects.Contains(cell_0))
                {
                    powerDamage *= DamageUnitValues.KING_EFFECT_ON_NEAR_UNITS;
                }


                if (eMG.MainToolWeaponTC(cell_0).HaveToolWeapon)
                {
                    if (eMG.MainTWLevelTC(cell_0).Is(LevelTypes.Second))
                    {
                        if (eMG.MainToolWeaponTC(cell_0).Is(ToolWeaponTypes.BowCrossbow))
                        {
                            powerDamage += DamageUnitValues.BOW_CROSSBOW_SECOND_ADDING;
                        }
                    }
                }
                if (eMG.ExtraToolWeaponTC(cell_0).Is(ToolWeaponTypes.Sword)) powerDamage += DamageUnitValues.SWORD_ADDING;

                if (eMG.MainToolWeaponTC(cell_0).Is(ToolWeaponTypes.Staff)) powerDamage -= DamageUnitValues.STAFF_EFFECT_ON_PAWN_TAKING;


                eMG.DamageAttackC(cell_0).Damage = powerDamage;




                if (eMG.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Protected))
                {
                    powerDamage += powerDamage * DamageUnitValues.PROTECTED;
                }
                else if (eMG.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Relaxed))
                {
                    powerDamage += powerDamage * DamageUnitValues.RELAXED;
                }

                if (eMG.BuildingTC(cell_0).HaveBuilding)
                {
                    var p = 0f;

                    switch (eMG.BuildingTC(cell_0).BuildingT)
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
                if (eMG.AdultForestC(cell_0).HaveAnyResources) protectionPercent += DamageUnitValues.ADULT_FOREST;
                if (eMG.HillC(cell_0).HaveAnyResources) protectionPercent += DamageUnitValues.HILL;

                powerDamage += powerDamage * protectionPercent;

                eMG.DamageOnCellC(cell_0).Damage = powerDamage;
            }
        }
    }
}