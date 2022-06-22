using Chessy.Game.Extensions;
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
            _e.StepUnitC(idx_from).Steps = 0;
            _e.SetUnitConditionT(idx_from, ConditionUnitTypes.None);

            if (_e.UnitT(idx_from).IsMelee(_e.MainToolWeaponT(idx_from)))
                ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.AttackMelee);
            else ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.AttackArcher);


            AnimationCellToGeneral(idx_from, AnimationCellTypes.JumpAppearanceUnit, RpcTarget.All);
            AnimationCellToGeneral(idx_to, AnimationCellTypes.JumpAppearanceUnit, RpcTarget.All);


            var powerDam_from = _e.DamageAttackC(idx_from).Damage;
            if (_e.AttackUniqueCellsC(idx_from).Contains(idx_to))
            {
                powerDam_from *= DamageUnitValues.UNIQUE_ATTACK_PERCENT_DAMAGE;
            }

            var powerDam_to = _e.DamageOnCellC(idx_to).Damage;


            var dirAttack = _e.AroundCellsE(idx_from).Direct(idx_to);

            if (_e.WeatherE.SunSideT.IsAcitveSun())
            {
                var isSunnedUnit = true;

                foreach (var dir in _e.WeatherE.SunSideT.RaysSun())
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

            if (_e.UnitT(idx_from).IsMelee(_e.MainToolWeaponT(idx_from)))
            {
                if (_e.ShieldUnitEffectC(idx_from).HaveAnyProtection())
                {
                    _e.ShieldUnitEffectC(idx_from).Protection--;
                }

                else if (_e.ExtraToolWeaponT(idx_from).Is(ToolWeaponTypes.Shield))
                {
                    UnitSs.AttackShield(1f, idx_from);
                }

                else if (minus_from > 0)
                {
                    UnitSs.Attack(minus_from, _e.UnitPlayerT(idx_from).NextPlayer(), idx_from);
                }
            }
            else
            {
                if (_e.FrozenArrawEffectC(idx_from).HaveShoots)
                {
                    _e.FrozenArrawEffectC(idx_from).Shoots--;

                    _e.StunUnitC(idx_to).Stun = StunValues.AFTER_FROZEN_ARRAW_PAWN;
                }
                else if (_e.UnitT(idx_from) == UnitTypes.Snowy)
                {
                    if (_snowyArrow <= 0)
                    {
                        _e.FrozenArrawEffectC(idx_from).Shoots--;

                        _e.StunUnitC(idx_to).Stun = 1;

                        _snowyArrow = Values.Values.RAINY_COOLDOWN_FROZEN_ARRAW;
                    }
                    else
                    {
                        _snowyArrow--;
                    }
                }
            }

            if (_e.ShieldUnitEffectC(idx_to).HaveAnyProtection())
            {
                _e.ShieldUnitEffectC(idx_to).Protection--;
            }

            else if (_e.ExtraToolWeaponT(idx_to).Is(ToolWeaponTypes.Shield))
            {
                UnitSs.AttackShield(1f, idx_to);
            }

            else if (minus_to > 0)
            {
                var killer = PlayerTypes.None;

                if (_e.UnitT(idx_to).IsAnimal())
                {
                    killer = _e.UnitPlayerT(idx_from);
                }
                else
                {
                    killer = _e.UnitPlayerT(idx_to).NextPlayer();
                }


                var wasUnitT_to = _e.UnitT(idx_to);

                UnitSs.Attack(minus_to, killer, idx_to);

                if (!_e.UnitT(idx_to).HaveUnit())
                {
                    if (_e.UnitT(idx_from).HaveUnit())
                    {
                        if (_e.UnitT(idx_from).IsMelee(_e.MainToolWeaponT(idx_from)))
                        {
                            ShiftUnitOnOtherCellM(idx_from, idx_to);
                        }
                    }

                    if (wasUnitT_to == UnitTypes.Wolf)
                    {
                        _e.ResourcesC(_e.UnitPlayerT(idx_from), ResourceTypes.Food).Resources += EconomyValues.AMOUNT_FOOD_AFTER_KILL_CAMEL;
                    }
                }
            }
        }
    }
}