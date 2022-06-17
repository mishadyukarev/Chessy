using Chessy.Game.Extensions;
using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Effect;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame : IUpdate
    {
        int _snowyArrow;

        internal void AttackUnitFromTo(in byte idx_from, in byte idx_to)
        {
            _eMG.StepUnitC(idx_from).Steps = 0;
            _eMG.UnitConditionTC(idx_from).Condition = ConditionUnitTypes.None;

            if (_eMG.UnitTC(idx_from).IsMelee(_eMG.MainToolWeaponTC(idx_from).ToolWeaponT))
                _eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);
            else _eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AttackArcher);


            _eMG.RpcPoolEs.AnimationCell_ToGeneral(idx_from, AnimationCellTypes.JumpAppearanceUnit, RpcTarget.All);
            _eMG.RpcPoolEs.AnimationCell_ToGeneral(idx_to, AnimationCellTypes.JumpAppearanceUnit, RpcTarget.All);


            var powerDam_from = _eMG.DamageAttackC(idx_from).Damage;
            if (_eMG.AttackUniqueCellsC(idx_from).Contains(idx_to))
            {
                powerDam_from *= DamageUnitValues.UNIQUE_ATTACK_PERCENT_DAMAGE;
            }

            var powerDam_to = _eMG.DamageOnCellC(idx_to).Damage;


            var dirAttack = _eMG.AroundCellsE(idx_from).Direct(idx_to);

            if (_eMG.WeatherE.SunSideTC.IsAcitveSun)
            {
                var isSunnedUnit = true;

                foreach (var dir in _eMG.WeatherE.SunSideTC.RaysSun)
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

            if (_eMG.UnitTC(idx_from).IsMelee(_eMG.MainToolWeaponTC(idx_from).ToolWeaponT))
            {
                if (_eMG.ShieldUnitEffectC(idx_from).HaveAnyProtection)
                {
                    _eMG.ShieldUnitEffectC(idx_from).Protection--;
                }

                else if (_eMG.ExtraToolWeaponTC(idx_from).Is(ToolWeaponTypes.Shield))
                {
                    UnitSs.AttackShieldS.Attack(1f, idx_from);
                }

                else if (minus_from > 0)
                {
                    UnitSs.AttackUnitS.Attack(minus_from, _eMG.UnitPlayerTC(idx_from).PlayerT.NextPlayer(), idx_from);
                }
            }
            else
            {
                if (_eMG.FrozenArrawEffectC(idx_from).HaveShoots)
                {
                    _eMG.FrozenArrawEffectC(idx_from).Shoots--;

                    _eMG.StunUnitC(idx_to).Stun = StunValues.AFTER_FROZEN_ARRAW_PAWN;
                }
                else if (_eMG.UnitT(idx_from) == UnitTypes.Snowy)
                {
                    if (_snowyArrow <= 0)
                    {
                        _eMG.FrozenArrawEffectC(idx_from).Shoots--;

                        _eMG.StunUnitC(idx_to).Stun = 1;

                        _snowyArrow = Values.Values.RAINY_COOLDOWN_FROZEN_ARRAW;
                    }
                    else
                    {
                        _snowyArrow--;
                    }
                }
            }

            if (_eMG.ShieldUnitEffectC(idx_to).HaveAnyProtection)
            {
                _eMG.ShieldUnitEffectC(idx_to).Protection--;
            }

            else if (_eMG.ExtraToolWeaponTC(idx_to).Is(ToolWeaponTypes.Shield))
            {
                UnitSs.AttackShieldS.Attack(1f, idx_to);
            }

            else if (minus_to > 0)
            {
                var killer = PlayerTypes.None;

                if (_eMG.UnitTC(idx_to).IsAnimal)
                {
                    killer = _eMG.UnitPlayerTC(idx_from).PlayerT;
                }
                else
                {
                    killer = _eMG.UnitPlayerTC(idx_to).PlayerT.NextPlayer();
                }


                var wasUnitT_to = _eMG.UnitTC(idx_to).UnitT;

                UnitSs.AttackUnitS.Attack(minus_to, killer, idx_to);

                if (!_eMG.UnitTC(idx_to).HaveUnit)
                {
                    if (_eMG.UnitTC(idx_from).HaveUnit)
                    {
                        if (_eMG.UnitTC(idx_from).IsMelee(_eMG.MainToolWeaponTC(idx_from).ToolWeaponT))
                        {
                            ShiftUnitOnOtherCellM(idx_from, idx_to);
                        }
                    }

                    if (wasUnitT_to == UnitTypes.Wolf)
                    {
                        _eMG.ResourcesC(_eMG.UnitPlayerTC(idx_from).PlayerT, ResourceTypes.Food).Resources += EconomyValues.AMOUNT_FOOD_AFTER_KILL_CAMEL;
                    }
                }
            }
        }
    }
}