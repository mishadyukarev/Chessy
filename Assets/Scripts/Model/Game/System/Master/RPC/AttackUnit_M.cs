using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Entity.Model;
using Chessy.Game.Extensions;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;

namespace Chessy.Game.Model.System.Master
{
    sealed class AttackUnit_M : SystemModelGameAbs
    {
        internal AttackUnit_M(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG)
        {
        }

        internal void Attack(in byte idx_from, in byte idx_to)
        {
            var whoseMove = eMG.WhoseMove.PlayerT;

            var canAttack = eMG.UnitEs(idx_from).UniqueAttack.Contains(idx_to)
                || eMG.UnitEs(idx_from).SimpleAttack.Contains(idx_to);

            if (canAttack && eMG.UnitPlayerTC(idx_from).Is(whoseMove))
            {
                eMG.UnitStepC(idx_from).Steps = 0;
                eMG.UnitConditionTC(idx_from).Condition = ConditionUnitTypes.None;

                if (eMG.UnitTC(idx_from).IsMelee(eMG.UnitMainTWTC(idx_from).ToolWeaponT))
                    eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);
                else eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AttackArcher);


                float powerDam_from = eMG.DamageAttackC(idx_from).Damage;
                if (eMG.UnitEs(idx_from).UniqueAttack.Contains(idx_to))
                {
                    powerDam_from *= DamageValues.UNIQUE_PERCENT_DAMAGE;
                }

                float powerDam_to = eMG.DamageOnCellC(idx_to).Damage;


                var dirAttack = eMG.CellEs(idx_from).AroundCellsEs.Direct(idx_to);

                if (eMG.WeatherE.SunSideTC.IsAcitveSun)
                {
                    var isSunnedUnit = true;

                    foreach (var dir in eMG.WeatherE.SunSideTC.RaysSun)
                    {
                        if (dirAttack == dir) isSunnedUnit = false;
                    }

                    if (isSunnedUnit)
                    {
                        powerDam_from *= 0.9f;
                    }
                }






                float min_limit = 0;
                float max_limit = 0;
                float minus_to = 0;
                float minus_from = 0;

                var maxDamage = HpValues.MAX;
                var minDamage = 0;

                //if (!e.UnitE(idx_to).IsMelee) powerDam_to /= 2;

                if (powerDam_to > powerDam_from)
                {
                    max_limit = powerDam_to * 2;
                    min_limit = powerDam_to / 3;

                    if (min_limit >= powerDam_from)
                    {
                        minus_from = maxDamage;
                        powerDam_to = minDamage;
                    }
                    else
                    {
                        minus_to = maxDamage * powerDam_from / max_limit;

                        max_limit = powerDam_from * 2;
                        minus_from = maxDamage * powerDam_to / max_limit;
                    }
                }
                else
                {
                    max_limit = powerDam_from * 2;
                    min_limit = powerDam_from / 3;

                    if (min_limit >= powerDam_to)
                    {
                        minus_to = maxDamage;
                        minus_from = minDamage;
                    }
                    else
                    {
                        minus_from = maxDamage * powerDam_to / max_limit;

                        max_limit = powerDam_to * 2f;
                        minus_to = maxDamage * powerDam_from / max_limit;
                    }
                }

                if (eMG.UnitTC(idx_from).IsMelee(eMG.UnitMainTWTC(idx_from).ToolWeaponT))
                {
                    if (eMG.UnitEffectShield(idx_from).HaveAnyProtection)
                    {
                        eMG.UnitEffectShield(idx_from).Protection--;
                    }

                    else if (eMG.UnitExtraTWTC(idx_from).Is(ToolWeaponTypes.Shield))
                    {
                        sMG.UnitSs.AttackShieldS.Attack(1f, idx_from);
                    }

                    else if (minus_from > 0)
                    {
                        sMG.UnitSs.AttackUnitS.Attack(minus_from, eMG.UnitPlayerTC(idx_from).PlayerT.NextPlayer(), idx_from);
                    }
                }
                else
                {
                    if (eMG.UnitEffectFrozenArrawC(idx_from).HaveShoots)
                    {
                        eMG.UnitEffectFrozenArrawC(idx_from).Shoots = 0;

                        eMG.UnitEffectStunC(idx_to).Stun = 2;
                    }
                }

                if (eMG.UnitEffectShield(idx_to).HaveAnyProtection)
                {
                    eMG.UnitEffectShield(idx_to).Protection--;
                }

                else if (eMG.UnitExtraTWTC(idx_to).Is(ToolWeaponTypes.Shield))
                {
                    sMG.UnitSs.AttackShieldS.Attack(1f, idx_to);
                }

                else if (minus_to > 0)
                {
                    var killer = PlayerTypes.None;

                    if (eMG.UnitTC(idx_to).IsAnimal)
                    {
                        killer = eMG.UnitPlayerTC(idx_from).PlayerT;
                    }
                    else
                    {
                        killer = eMG.UnitPlayerTC(idx_to).PlayerT.NextPlayer();
                    }


                    var wasUnitT_to = eMG.UnitTC(idx_to).UnitT;

                    sMG.UnitSs.AttackUnitS.Attack(minus_to, killer, idx_to);

                    if (!eMG.UnitTC(idx_to).HaveUnit)
                    {
                        if (eMG.UnitTC(idx_from).HaveUnit)
                        {
                            if (eMG.UnitTC(idx_from).IsMelee(eMG.UnitMainTWTC(idx_from).ToolWeaponT))
                            {
                                sMG.UnitSs.ShiftUnitS.Shift(idx_from, idx_to);
                            }
                        }

                        if (wasUnitT_to == UnitTypes.Wolf)
                        {
                            eMG.ResourcesC(eMG.UnitPlayerTC(idx_from).PlayerT, ResourceTypes.Food).Resources += EconomyValues.AMOUNT_FOOD_AFTER_KILL_CAMEL;
                        }
                    }
                }
            }
        }
    }
}