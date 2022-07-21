using Chessy.Model.Enum;
using Chessy.Model.Values;
using Photon.Pun;
using Photon.Realtime;
namespace Chessy.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModelAbstract
    {
        internal void CircularAttackKingM(in byte cell_0, in AbilityTypes abilityT, in Player sender)
        {
            if (!_cooldownAbilityCs[cell_0].HaveCooldown(abilityT))
            {
                _s.RpcSs.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.AttackMelee);

                _cooldownAbilityCs[cell_0].Set(abilityT, AbilityCooldownUnitValues.NeedAfterAbility(abilityT));


                foreach (byte idx_1 in _e.IdxsCellsAround(cell_0))
                {
                    if (_e.UnitT(idx_1).HaveUnit())
                    {
                        if (_unitCs[idx_1].PlayerT != _unitCs[cell_0].PlayerT)
                        {
                            if (!_shiftingUnitCs[idx_1].IsShifting)
                            {
                                if (_extraTWC[idx_1].ToolWeaponT == ToolsWeaponsWarriorTypes.Shield)
                                {
                                    _s.UnitSs.AttackShield(1f, idx_1);
                                }
                                else if (_effectsUnitCs[idx_1].HaveAnyProtectionRainyMagicShield)
                                {
                                    _effectsUnitCs[idx_1].ProtectionRainyMagicShield--;
                                }

                                else
                                {
                                    _s.AttackUnitOnCell(HpUnitValues.MAX / 4, _unitCs[cell_0].PlayerT, idx_1);
                                }
                            }
                        }
                    }
                }

                _s.RpcSs.AnimationCellToGeneral(_unitWhereViewDataCs[cell_0].ViewIdxCell, AnimationCellTypes.CircularAttackKing, RpcTarget.All);

                _unitCs[cell_0].ConditionT = ConditionUnitTypes.None;

                _s.RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.AttackMelee);
            }

            else _s.RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.Mistake);
        }
    }
}