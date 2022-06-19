using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModel
    {
        internal void CurcularAttackKingM(in byte cell_0, in AbilityTypes abilityT, in Player sender)
        {
            if (!_eMG.UnitCooldownAbilitiesC(cell_0).HaveCooldown(abilityT))
            {
                if (_eMG.StepUnitC(cell_0).Steps >= StepValues.Need(abilityT))
                {
                    _sMG.ExecuteSoundActionToGeneral(RpcTarget.All, ClipTypes.AttackMelee);

                    _eMG.UnitCooldownAbilitiesC(cell_0).Set(abilityT, AbilityCooldownValues.NeedAfterAbility(abilityT));
                    _eMG.StepUnitC(cell_0).Steps -= StepValues.Need(abilityT);


                    foreach (byte idx_1 in _eMG.AroundCellsE(cell_0).CellsAround)
                    {
                        if (_eMG.UnitTC(idx_1).HaveUnit)
                        {
                            if (!_eMG.UnitPlayerTC(idx_1).Is(_eMG.UnitPlayerTC(cell_0).PlayerT))
                            {
                                if (_eMG.ExtraToolWeaponTC(idx_1).Is(ToolWeaponTypes.Shield))
                                {
                                    _sMG.UnitSs.AttackShield(1f, idx_1);
                                }
                                else if (_eMG.ShieldUnitEffectC(idx_1).HaveAnyProtection)
                                {
                                    _eMG.ShieldUnitEffectC(idx_1).Protection--;
                                }

                                else
                                {
                                    _sMG.UnitSs.Attack(HpValues.MAX / 4, _eMG.UnitPlayerTC(cell_0).PlayerT, idx_1);
                                }
                            }
                        }
                    }

                    _eMG.RpcPoolEs.AnimationCell_ToGeneral(cell_0, AnimationCellTypes.CircularAttackKing, RpcTarget.All);

                    _eMG.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.None;

                    _sMG.ExecuteSoundActionToGeneral(sender, ClipTypes.AttackMelee);
                }

                else
                {
                    _eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }

            else _sMG.ExecuteSoundActionToGeneral(sender, ClipTypes.Mistake);
        }
    }
}