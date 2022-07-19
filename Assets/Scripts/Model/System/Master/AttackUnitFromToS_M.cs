using Chessy.Model.Values;
using Photon.Pun;
namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        internal void AttackUnitFromTo(in byte idx_from, in byte idx_to)
        {
            _e.SetUnitConditionT(idx_from, ConditionUnitTypes.None);
            _e.UnitMainC(idx_from).CooldownForAttackAnyUnitInSeconds = ValuesChessy.COOLDOWN_AFTER_ATTACK;

            _e.UnitMainC(idx_from).HowManySecondUnitWasHereInThisCondition = 0;


            if (_e.UnitT(idx_to).IsAnimal())
            {
                RpcSs.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.AttackAnimal);
            }
            else
            {
                if (_e.UnitT(idx_from).IsMelee(_e.MainToolWeaponT(idx_from)))
                {
                    RpcSs.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.AttackMelee);
                }
                else
                {
                    RpcSs.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.AttackArcher);
                }
            }

            RpcSs.AnimationCellToGeneral(idx_from, AnimationCellTypes.AttackSword, RpcTarget.All);
            RpcSs.AnimationCellToGeneral(idx_to, AnimationCellTypes.AttackSword, RpcTarget.All);

            RpcSs.AnimationCellToGeneral(idx_from, AnimationCellTypes.JumpAppearanceUnit, RpcTarget.All);
            RpcSs.AnimationCellToGeneral(idx_to, AnimationCellTypes.JumpAppearanceUnit, RpcTarget.All);


            var powerDam_from = _e.DamageSimpleAttack(idx_from);
            if (_e.WhereUnitCanAttackUniqueAttackToEnemyC(idx_from).Can(idx_to))
            {
                powerDam_from *= DamageUnitValues.UNIQUE_ATTACK_PERCENT_DAMAGE;
            }

            var powerDam_to = _e.DamageSimpleAttack(idx_to);


            var dirAttack = _e.DirectionAround(idx_from, idx_to);

            if (_e.SunSideT.IsAcitveSun())
            {
                var isSunnedUnit = true;

                foreach (var dir in _e.SunSideT.RaysSun())
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

            var maxDamage = HpUnitValues.MAX;
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
                if (_e.UnitEffectsC(idx_from).ProtectionRainyMagicShield >= 1)
                {
                    _e.UnitEffectsC(idx_from).ProtectionRainyMagicShield--;
                }

                else if (_e.ExtraToolWeaponT(idx_from).Is(ToolsWeaponsWarriorTypes.Shield))
                {
                    UnitSs.AttackShield(1f, idx_from);
                }

                else if (minus_from > 0)
                {
                    AttackUnitOnCell(minus_from, _e.UnitPlayerT(idx_from).NextPlayer(), idx_from);
                }
            }
            else
            {
                if (_e.UnitEffectsC(idx_from).HaveFrozenArrawArcher)
                {
                    _e.UnitEffectsC(idx_from).HaveFrozenArrawArcher = false;
                    _e.UnitEffectsC(idx_to).StunHowManyUpdatesNeedStay = StunUnitValues.AFTER_FROZEN_ARRAW_PAWN;
                }
            }

            if (_e.UnitEffectsC(idx_to).ProtectionRainyMagicShield >= 1)
            {
                _e.UnitEffectsC(idx_to).ProtectionRainyMagicShield--;
            }

            else if (_e.ExtraToolWeaponT(idx_to).Is(ToolsWeaponsWarriorTypes.Shield))
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

                AttackUnitOnCell(minus_to, killer, idx_to);

                if (!_e.UnitT(idx_to).HaveUnit())
                {
                    if (_e.UnitT(idx_from).HaveUnit())
                    {
                        if (_e.UnitT(idx_from).IsMelee(_e.MainToolWeaponT(idx_from)))
                        {
                            //ShiftUnitOnOtherCellM(idx_from, idx_to);
                        }
                    }

                    if (wasUnitT_to == UnitTypes.Wolf)
                    {
                        _e.ResourcesInInventoryC(_e.UnitPlayerT(idx_from)).Add(ResourceTypes.Food, EconomyValues.AMOUNT_FOOD_AFTER_KILL_CAMEL);
                    }
                }
            }
        }
    }
}