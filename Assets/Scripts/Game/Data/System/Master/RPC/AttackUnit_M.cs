using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;

namespace Chessy.Game.System.Model.Master
{
    public struct AttackUnit_M
    {
        public void Attack(in byte idx_from, in byte idx_to, in SystemsModel sMM, in EntitiesModel e)
        {
            var whoseMove = e.WhoseMove.Player;

            var canAttack = e.UnitEs(idx_from).ForAttack(AttackTypes.Unique).Contains(idx_to)
                || e.UnitEs(idx_from).ForAttack(AttackTypes.Simple).Contains(idx_to);

            if (canAttack && e.UnitPlayerTC(idx_from).Is(whoseMove))
            {
                e.UnitStepC(idx_from).Steps = 0;
                e.UnitConditionTC(idx_from).Condition = ConditionUnitTypes.None;

                if (e.IsMelee(idx_from))
                    e.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);
                else e.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AttackArcher);


                float powerDam_from = e.DamageAttackC(idx_from).Damage; 
                if (e.UnitEs(idx_from).ForAttack(AttackTypes.Unique).Contains(idx_to))
                {
                    powerDam_from *= DamageValues.UNIQUE_PERCENT_DAMAGE;
                }

                float powerDam_to = e.DamageOnCellC(idx_to).Damage;


                var dirAttack = e.CellEs(idx_from).Direct(idx_to);

                if (e.WeatherE.SunC.IsAcitveSun)
                {
                    var isSunnedUnit = true;

                    foreach (var dir in e.WeatherE.SunC.RaysSun)
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

                if (e.IsMelee(idx_from))
                {
                    if (e.UnitEffectShield(idx_from).HaveAnyProtection)
                    {
                        e.UnitEffectShield(idx_from).Protection--;
                    }

                    else if (e.UnitExtraTWTC(idx_from).Is(ToolWeaponTypes.Shield))
                    {
                        sMM.AttackShieldS.Attack(1f, idx_from, e);
                    }

                    else if (minus_from > 0)
                    {
                        sMM.AttackUnitS.AttackUnit(minus_from, e.NextPlayer(e.UnitPlayerTC(idx_from).Player).Player, idx_from, sMM.KillUnitS, e);
                    }
                }
                else
                {
                    if (e.UnitEffectFrozenArrawC(idx_from).HaveEffect)
                    {
                        e.UnitEffectFrozenArrawC(idx_from).Shoots = 0;

                        e.UnitEffectStunC(idx_to).Stun = 2;
                    }
                }

                if (e.UnitEffectShield(idx_to).HaveAnyProtection)
                {
                    e.UnitEffectShield(idx_to).Protection--;
                }

                else if (e.UnitExtraTWTC(idx_to).Is(ToolWeaponTypes.Shield))
                {
                    sMM.AttackShieldS.Attack(1f, idx_to, e);
                }

                else if (minus_to > 0)
                {
                    var killer = PlayerTypes.None;

                    if (e.IsAnimal(e.UnitTC(idx_to).Unit))
                    {
                        killer = e.UnitPlayerTC(idx_from).Player;
                    }
                    else
                    {
                        killer = e.NextPlayer(e.UnitPlayerTC(idx_to)).Player;
                    }


                    var wasUnitT_to = e.UnitTC(idx_to).Unit;

                    sMM.AttackUnitS.AttackUnit(minus_to, killer, idx_to, sMM.KillUnitS, e);

                    if (!e.UnitTC(idx_to).HaveUnit)
                    {
                        if (e.UnitTC(idx_from).HaveUnit)
                        {
                            if (e.IsMelee(idx_from))
                            {
                                new ShiftUnitS(idx_from, idx_to, e);
                            }
                        }

                        if (wasUnitT_to == UnitTypes.Camel)
                        {
                            e.ResourcesC(e.UnitPlayerTC(idx_from).Player, ResourceTypes.Food).Resources += EconomyValues.AMOUNT_FOOD_AFTER_KILL_CAMEL;
                        }
                    }
                }
            }
        }
    }
}