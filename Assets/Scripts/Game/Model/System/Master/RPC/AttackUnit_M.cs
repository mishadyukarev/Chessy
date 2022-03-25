using Chessy.Game.Entity.Model;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;

namespace Chessy.Game.System.Model.Master
{
    sealed class AttackUnit_M : SystemModelGameAbs
    {
        readonly AttackUnitS _attackUnitS;
        readonly AttackShieldS _attackShieldS;
        readonly ShiftUnitS _shiftUnitS;

        internal AttackUnit_M(in AttackUnitS attackUnitS, in AttackShieldS attackShieldS, in ShiftUnitS shiftUnitS, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _attackUnitS = attackUnitS;
            _attackShieldS = attackShieldS;
            _shiftUnitS = shiftUnitS;
        }

        public void Attack(in byte idx_from, in byte idx_to)
        {
            var whoseMove = eMGame.WhoseMove.Player;

            var canAttack = eMGame.UnitEs(idx_from).UniqueAttack.Contains(idx_to)
                || eMGame.UnitEs(idx_from).SimpleAttack.Contains(idx_to);

            if (canAttack && eMGame.UnitPlayerTC(idx_from).Is(whoseMove))
            {
                eMGame.UnitStepC(idx_from).Steps = 0;
                eMGame.UnitConditionTC(idx_from).Condition = ConditionUnitTypes.None;

                if (eMGame.UnitTC(idx_from).IsMelee(eMGame.UnitMainTWTC(idx_from).ToolWeapon))
                    eMGame.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);
                else eMGame.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AttackArcher);


                float powerDam_from = eMGame.DamageAttackC(idx_from).Damage; 
                if (eMGame.UnitEs(idx_from).UniqueAttack.Contains(idx_to))
                {
                    powerDam_from *= DamageValues.UNIQUE_PERCENT_DAMAGE;
                }

                float powerDam_to = eMGame.DamageOnCellC(idx_to).Damage;


                var dirAttack = eMGame.CellEs(idx_from).Direct(idx_to);

                if (eMGame.WeatherE.SunC.IsAcitveSun)
                {
                    var isSunnedUnit = true;

                    foreach (var dir in eMGame.WeatherE.SunC.RaysSun)
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

                if (eMGame.UnitTC(idx_from).IsMelee(eMGame.UnitMainTWTC(idx_from).ToolWeapon))
                {
                    if (eMGame.UnitEffectShield(idx_from).HaveAnyProtection)
                    {
                        eMGame.UnitEffectShield(idx_from).Protection--;
                    }

                    else if (eMGame.UnitExtraTWTC(idx_from).Is(ToolWeaponTypes.Shield))
                    {
                        _attackShieldS.Attack(1f, idx_from);
                    }

                    else if (minus_from > 0)
                    {
                        _attackUnitS.Attack(minus_from, eMGame.NextPlayer(eMGame.UnitPlayerTC(idx_from).Player).Player, idx_from);
                    }
                }
                else
                {
                    if (eMGame.UnitEffectFrozenArrawC(idx_from).HaveShoots)
                    {
                        eMGame.UnitEffectFrozenArrawC(idx_from).Shoots = 0;

                        eMGame.UnitEffectStunC(idx_to).Stun = 2;
                    }
                }

                if (eMGame.UnitEffectShield(idx_to).HaveAnyProtection)
                {
                    eMGame.UnitEffectShield(idx_to).Protection--;
                }

                else if (eMGame.UnitExtraTWTC(idx_to).Is(ToolWeaponTypes.Shield))
                {
                    _attackShieldS.Attack(1f, idx_to);
                }

                else if (minus_to > 0)
                {
                    var killer = PlayerTypes.None;

                    if (eMGame.UnitTC(idx_to).IsAnimal)
                    {
                        killer = eMGame.UnitPlayerTC(idx_from).Player;
                    }
                    else
                    {
                        killer = eMGame.NextPlayer(eMGame.UnitPlayerTC(idx_to)).Player;
                    }


                    var wasUnitT_to = eMGame.UnitTC(idx_to).Unit;

                    _attackUnitS.Attack(minus_to, killer, idx_to);

                    if (!eMGame.UnitTC(idx_to).HaveUnit)
                    {
                        if (eMGame.UnitTC(idx_from).HaveUnit)
                        {
                            if (eMGame.UnitTC(idx_from).IsMelee(eMGame.UnitMainTWTC(idx_from).ToolWeapon))
                            {
                                _shiftUnitS.Shift(idx_from, idx_to);
                            }
                        }

                        if (wasUnitT_to == UnitTypes.Wolf)
                        {
                            eMGame.ResourcesC(eMGame.UnitPlayerTC(idx_from).Player, ResourceTypes.Food).Resources += EconomyValues.AMOUNT_FOOD_AFTER_KILL_CAMEL;
                        }
                    }
                }
            }
        }
    }
}