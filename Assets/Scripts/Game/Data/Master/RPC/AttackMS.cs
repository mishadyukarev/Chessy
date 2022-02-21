using Photon.Pun;

namespace Game.Game
{
    sealed class AttackMS : SystemAbstract, IEcsRunSystem
    {
        internal AttackMS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            var whoseMove = E.WhoseMove.Player;
            var idx_from = E.RpcPoolEs.AttackME.FromIdxC.Idx;
            var idx_to = E.RpcPoolEs.AttackME.ToIdxC.Idx;

            var canAttack = E.UnitEs(idx_from).ForAttack(AttackTypes.Unique).Contains(idx_to)
                || E.UnitEs(idx_from).ForAttack(AttackTypes.Simple).Contains(idx_to);

            if (canAttack && E.UnitPlayerTC(idx_from).Is(whoseMove))
            {
                E.UnitStepC(idx_from).Steps = 0;
                E.UnitConditionTC(idx_from).Condition = ConditionUnitTypes.None;

                if (E.UnitMainE(idx_from).IsMelee)
                    E.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);
                else E.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AttackArcher);


                float powerDam_from = E.UnitDamageAttackC(idx_from).Damage;
                float powerDam_to = E.UnitDamageAttackC(idx_to).Damage;


                if (E.UnitEs(idx_from).ForAttack(AttackTypes.Unique).Contains(idx_to))
                {
                    powerDam_from += powerDam_from * CellUnitDamage_Values.UNIQUE_PERCENT_DAMAGE;
                }

                var dirAttack = E.CellEs(idx_from).Direct(idx_to);

                if (E.SunSideTC.IsAcitveSun)
                {
                    var isSunnedUnit = true;

                    foreach (var dir in E.SunSideTC.RaysSun)
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

                var maxDamage = CellUnitStatHp_Values.MAX_HP;
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


                var player_from = E.UnitPlayerTC(idx_from).Player;

                if (E.UnitMainE(idx_from).IsMelee)
                {
                    if (E.UnitEffectShield(idx_from).HaveAnyProtection)
                    {
                        E.UnitEffectShield(idx_from).Protection--;
                    }

                    else if (E.UnitExtraTWTC(idx_from).Is(ToolWeaponTypes.Shield))
                    {
                        E.UnitEffectShield(idx_from).Protection--;
                        if (!E.UnitEffectShield(idx_from).HaveAnyProtection)
                            E.UnitExtraTWTC(idx_from).ToolWeapon = ToolWeaponTypes.None;
                    }

                    else if (minus_from > 0)
                    {
                        E.UnitAttackE.Attack(minus_from, E.NextPlayer(E.UnitPlayerTC(idx_from).Player).Player, idx_from);
                    }
                }
                else
                {
                    if (E.UnitEffectFrozenArrawC(idx_from).HaveEffect)
                    {
                        E.UnitEffectFrozenArrawC(idx_from).Shoots = 0;

                        E.UnitEffectStunC(idx_to).Stun = 2;
                    }
                }

                if (E.UnitEffectShield(idx_to).HaveAnyProtection)
                {
                    E.UnitEffectShield(idx_to).Protection--;
                }

                else if (E.UnitExtraTWTC(idx_to).Is(ToolWeaponTypes.Shield))
                {
                    E.UnitEffectShield(idx_to).Protection--;
                    if (!E.UnitEffectShield(idx_to).HaveAnyProtection)
                        E.UnitExtraTWTC(idx_to).ToolWeapon = ToolWeaponTypes.None;
                }

                else if (minus_to > 0)
                {
                    var wasUnitT = E.UnitTC(idx_to).Unit;

                    if (E.UnitMainE(idx_to).IsAnimal)
                    {
                        E.UnitAttackE.Attack(minus_to, E.UnitPlayerTC(idx_from).Player, idx_to);
                    }
                    else
                    {
                        E.UnitAttackE.Attack(minus_to, E.NextPlayer(E.UnitPlayerTC(idx_to)).Player, idx_to);
                    }




                    if (!E.UnitHpC(idx_to).IsAlive)
                    {
                        if (wasUnitT == UnitTypes.Camel)
                        {
                            E.ResourcesC(E.UnitPlayerTC(idx_from).Player, ResourceTypes.Food).Resources += ResourcesEconomy_Values.AMOUNT_FOOD_AFTER_KILL_CAMEL;
                        }

                        if (E.UnitTC(idx_from).HaveUnit)
                        {
                            if (E.UnitMainE(idx_from).IsMelee)
                            {
                                E.UnitEs(idx_to).Set(E.UnitEs(idx_from));
                                E.UnitTC(idx_from).Unit = UnitTypes.None;
                            }
                        }
                    }
                }
            }
        }
    }
}