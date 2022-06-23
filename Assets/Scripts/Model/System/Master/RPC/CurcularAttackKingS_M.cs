using Chessy.Model.Values.Cell.Unit;
using Chessy.Model.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Model.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModel
    {
        internal void CurcularAttackKingM(in byte cell_0, in AbilityTypes abilityT, in Player sender)
        {
            if (!_e.UnitCooldownAbilitiesC(cell_0).HaveCooldown(abilityT))
            {
                if (_e.StepUnitC(cell_0).Steps >= StepValues.Need(abilityT))
                {
                    _s.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.AttackMelee);

                    _e.UnitCooldownAbilitiesC(cell_0).Set(abilityT, AbilityCooldownValues.NeedAfterAbility(abilityT));
                    _e.StepUnitC(cell_0).Steps -= StepValues.Need(abilityT);


                    foreach (byte idx_1 in _e.AroundCellsE(cell_0).CellsAround)
                    {
                        if (_e.UnitT(idx_1).HaveUnit())
                        {
                            if (!_e.UnitPlayerT(idx_1).Is(_e.UnitPlayerT(cell_0)))
                            {
                                if (_e.ExtraToolWeaponT(idx_1).Is(ToolWeaponTypes.Shield))
                                {
                                    _s.UnitSs.AttackShield(1f, idx_1);
                                }
                                else if (_e.ShieldUnitEffectC(idx_1).HaveAnyProtection())
                                {
                                    _e.ShieldUnitEffectC(idx_1).Protection--;
                                }

                                else
                                {
                                    _s.UnitSs.Attack(HpValues.MAX / 4, _e.UnitPlayerT(cell_0), idx_1);
                                }
                            }
                        }
                    }

                    _s.AnimationCellToGeneral(cell_0, AnimationCellTypes.CircularAttackKing, RpcTarget.All);

                    _e.SetUnitConditionT(cell_0, ConditionUnitTypes.None);

                    _s.ExecuteSoundActionToGeneral(sender, ClipTypes.AttackMelee);
                }

                else
                {
                    _s.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }

            else _s.ExecuteSoundActionToGeneral(sender, ClipTypes.Mistake);
        }
    }
}