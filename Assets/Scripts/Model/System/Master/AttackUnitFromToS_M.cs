using Chessy.Model.Values;
using Photon.Pun;
namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        internal void AttackUnitFromTo(in byte idx_from, in byte idx_to)
        {
            _unitCs[idx_from].ConditionT = ConditionUnitTypes.None;
            _unitCs[idx_from].CooldownForAttackAnyUnitInSeconds = ValuesChessy.COOLDOWN_AFTER_ATTACK;

            _unitCs[idx_from].HowManySecondUnitWasHereInThisCondition = 0;


            if (_unitCs[idx_to].IsAnimal)
            {
                RpcSs.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.AttackAnimal);
            }
            else
            {
                if (_unitCs[idx_from].UnitT.IsMelee(_mainTWC[idx_from].ToolWeaponT))
                {
                    RpcSs.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.AttackMelee);
                }
                else
                {
                    RpcSs.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.AttackArcher);
                }
            }

            if (_unitVisibleCs[idx_from].IsVisible(_unitCs[idx_from].PlayerT.NextPlayer()))
            {
                RpcSs.AnimationCellToGeneral(idx_from, AnimationCellTypes.AttackSword, RpcTarget.All);
            }
            
            RpcSs.AnimationCellToGeneral(idx_to, AnimationCellTypes.AttackSword, RpcTarget.All);

            RpcSs.AnimationCellToGeneral(idx_from, AnimationCellTypes.JumpAppearanceUnit, RpcTarget.All);
            RpcSs.AnimationCellToGeneral(idx_to, AnimationCellTypes.JumpAppearanceUnit, RpcTarget.All);


            var powerDam_from = _unitCs[idx_from].DamageSimpleAttack;
            if (_whereUniqueAttackCs[idx_from].Can(idx_to))
            {
                powerDam_from *= DamageUnitValues.UNIQUE_ATTACK_PERCENT_DAMAGE;
            }

            var powerDam_to = _unitCs[idx_to].DamageSimpleAttack;


            var dirAttack = _cellAroundCs[idx_from, idx_to].DirectT;

            if (_sunC.IsAcitveSun)
            {
                var isSunnedUnit = true;

                foreach (var dir in _sunC.RaysSun)
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

            if (_unitCs[idx_from].UnitT.IsMelee(_mainTWC[idx_from].ToolWeaponT))
            {
                if (_effectsUnitCs[idx_from].ProtectionRainyMagicShield >= 1)
                {
                    _effectsUnitCs[idx_from].ProtectionRainyMagicShield--;
                }

                else if (_extraTWC[idx_from].ToolWeaponT == ToolsWeaponsWarriorTypes.Shield)
                {
                    UnitSs.AttackShield(1f, idx_from);
                }

                else if (minus_from > 0)
                {
                    AttackUnitOnCell(minus_from, _unitCs[idx_from].PlayerT.NextPlayer(), idx_from);
                }
            }
            else
            {
                if (_effectsUnitCs[idx_from].HaveFrozenArrawArcher)
                {
                    _effectsUnitCs[idx_from].HaveFrozenArrawArcher = false;
                    _effectsUnitCs[idx_to].StunHowManyUpdatesNeedStay = StunUnitValues.AFTER_FROZEN_ARRAW_PAWN;
                }
            }

            if (_effectsUnitCs[idx_to].ProtectionRainyMagicShield >= 1)
            {
                _effectsUnitCs[idx_to].ProtectionRainyMagicShield--;
            }

            else if (_extraTWC[idx_to].ToolWeaponT == ToolsWeaponsWarriorTypes.Shield)
            {
                UnitSs.AttackShield(1f, idx_to);
            }

            else if (minus_to > 0)
            {
                var killer = PlayerTypes.None;

                if (_unitCs[idx_to].IsAnimal)
                {
                    killer = _unitCs[idx_from].PlayerT;
                }
                else
                {
                    killer = _unitCs[idx_to].PlayerT.NextPlayer();
                }


                var wasUnitT_to = _unitCs[idx_to].UnitT;

                AttackUnitOnCell(minus_to, killer, idx_to);

                if (!_unitCs[idx_to].HaveUnit)
                {
                    if (_unitCs[idx_from].HaveUnit)
                    {
                        if (_unitCs[idx_from].UnitT.IsMelee(_mainTWC[idx_from].ToolWeaponT))
                        {
                            //ShiftUnitOnOtherCellM(idx_from, idx_to);
                        }
                    }

                    if (wasUnitT_to == UnitTypes.Wolf)
                    {
                        ResourcesInInventoryC(_unitCs[idx_from].PlayerT).Add(ResourceTypes.Food, EconomyValues.AMOUNT_FOOD_AFTER_KILL_CAMEL);
                    }
                }
            }
        }
    }
}