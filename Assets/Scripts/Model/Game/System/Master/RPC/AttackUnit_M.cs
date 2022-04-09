using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Extensions;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;
using Chessy.Game.Values.Cell.Unit.Effect;

namespace Chessy.Game.Model.System.Master
{
    sealed class AttackUnit_M : SystemModel
    {
        int _snowyArrow;

        internal AttackUnit_M(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG)
        {
        }

        internal void Attack(in byte idx_from, in byte idx_to, in Player sender)
        {
            var whoseMove = PhotonNetwork.OfflineMode ? eMG.WhoseMovePlayerT : sender.GetPlayer();

            var canAttack = eMG.AttackUniqueCellsC(idx_from).Contains(idx_to)
                || eMG.AttackSimpleCellsC(idx_from).Contains(idx_to);

            if (canAttack && eMG.UnitPlayerTC(idx_from).Is(whoseMove))
            {
                eMG.StepUnitC(idx_from).Steps = 0;
                eMG.UnitConditionTC(idx_from).Condition = ConditionUnitTypes.None;

                if (eMG.UnitTC(idx_from).IsMelee(eMG.MainToolWeaponTC(idx_from).ToolWeaponT))
                    eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);
                else eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AttackArcher);


                var powerDam_from = eMG.DamageAttackC(idx_from).Damage;
                if (eMG.AttackUniqueCellsC(idx_from).Contains(idx_to))
                {
                    powerDam_from *= DamageUnitValues.UNIQUE_ATTACK_PERCENT_DAMAGE;
                }

                var powerDam_to = eMG.DamageOnCellC(idx_to).Damage;


                var dirAttack = eMG.AroundCellsE(idx_from).Direct(idx_to);

                if (eMG.WeatherE.SunSideTC.IsAcitveSun)
                {
                    var isSunnedUnit = true;

                    foreach (var dir in eMG.WeatherE.SunSideTC.RaysSun)
                    {
                        if (dirAttack == dir) isSunnedUnit = false;
                    }

                    if (isSunnedUnit)
                    {
                        powerDam_from *= DamageUnitValues.SUN_EFFECT;
                    }
                }






                double min_limit = 0;
                double max_limit = 0;
                double minus_to = 0;
                double minus_from = 0;

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

                if (eMG.UnitTC(idx_from).IsMelee(eMG.MainToolWeaponTC(idx_from).ToolWeaponT))
                {
                    if (eMG.ShieldUnitEffectC(idx_from).HaveAnyProtection)
                    {
                        eMG.ShieldUnitEffectC(idx_from).Protection--;
                    }

                    else if (eMG.ExtraToolWeaponTC(idx_from).Is(ToolWeaponTypes.Shield))
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
                    if (eMG.FrozenArrawEffectC(idx_from).HaveShoots)
                    {
                        eMG.FrozenArrawEffectC(idx_from).Shoots--;

                        eMG.StunUnitC(idx_to).Stun = StunValues.AFTER_FROZEN_ARRAW_PAWN;
                    }
                    else if(eMG.UnitT(idx_from) == UnitTypes.Snowy)
                    {
                        if (_snowyArrow <= 0)
                        {
                            eMG.FrozenArrawEffectC(idx_from).Shoots--;

                            eMG.StunUnitC(idx_to).Stun = 1;

                            _snowyArrow = Values.Values.RAINY_COOLDOWN_FROZEN_ARRAW;
                        }
                        else
                        {
                            _snowyArrow--;
                        }
                    }
                }

                if (eMG.ShieldUnitEffectC(idx_to).HaveAnyProtection)
                {
                    eMG.ShieldUnitEffectC(idx_to).Protection--;
                }

                else if (eMG.ExtraToolWeaponTC(idx_to).Is(ToolWeaponTypes.Shield))
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
                            if (eMG.UnitTC(idx_from).IsMelee(eMG.MainToolWeaponTC(idx_from).ToolWeaponT))
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