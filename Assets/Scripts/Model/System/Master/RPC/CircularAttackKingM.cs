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
                s.RpcSs.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.AttackMelee);

                _cooldownAbilityCs[cell_0].Set(abilityT, AbilityCooldownUnitValues.NeedAfterAbility(abilityT));


                foreach (byte idx_1 in IdxsAroundCellC(cell_0).IdxCellsAroundArray)
                {
                    if (unitCs[idx_1].HaveUnit)
                    {
                        if (unitCs[idx_1].PlayerT != unitCs[cell_0].PlayerT)
                        {
                            if (!shiftingUnitCs[idx_1].IsShifting)
                            {
                                if (_extraTWC[idx_1].ToolWeaponT == ToolsWeaponsWarriorTypes.Shield)
                                {
                                    s.unitSs.AttackShield(1f, idx_1);
                                }
                                else if (_effectsUnitCs[idx_1].HaveAnyProtectionRainyMagicShield)
                                {
                                    _effectsUnitCs[idx_1].ProtectionRainyMagicShield--;
                                }

                                else
                                {
                                    s.AttackUnitOnCell(HpUnitValues.MAX / 4, unitCs[cell_0].PlayerT, idx_1);
                                }
                            }
                        }
                    }
                }

                s.RpcSs.AnimationCellToGeneral(_unitWhereViewDataCs[cell_0].ViewIdxCell, AnimationCellTypes.CircularAttackKing, RpcTarget.All);

                unitCs[cell_0].ConditionT = ConditionUnitTypes.None;

                s.RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.AttackMelee);
            }

            else s.RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.Mistake);
        }
    }
}