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
            if (!_e.UnitCooldownAbilitiesC(cell_0).HaveCooldown(abilityT))
            {
                _s.RpcSs.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.AttackMelee);

                _e.UnitCooldownAbilitiesC(cell_0).Set(abilityT, AbilityCooldownUnitValues.NeedAfterAbility(abilityT));


                foreach (byte idx_1 in _e.IdxsCellsAround(cell_0, DistanceFromCellTypes.First))
                {
                    if (_e.UnitT(idx_1).HaveUnit())
                    {
                        if (!_e.UnitPlayerT(idx_1).Is(_e.UnitPlayerT(cell_0)))
                        {
                            if (!_e.ShiftingInfoForUnitC(idx_1).IsShiftingUnit)
                            {
                                if (_e.ExtraToolWeaponT(idx_1).Is(ToolsWeaponsWarriorTypes.Shield))
                                {
                                    _s.UnitSs.AttackShield(1f, idx_1);
                                }
                                else if (_e.UnitEffectsC(idx_1).HaveAnyProtectionRainyMagicShield)
                                {
                                    _e.UnitEffectsC(idx_1).ProtectionRainyMagicShield--;
                                }

                                else
                                {
                                    _s.AttackUnitOnCell(HpValues.MAX / 4, _e.UnitPlayerT(cell_0), idx_1);
                                }
                            }
                        }
                    }
                }

                _s.RpcSs.AnimationCellToGeneral(_e.SkinInfoUnitC(cell_0).SkinIdxCell, AnimationCellTypes.CircularAttackKing, RpcTarget.All);

                _e.SetUnitConditionT(cell_0, ConditionUnitTypes.None);

                _s.RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.AttackMelee);
            }

            else _s.RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.Mistake);
        }
    }
}