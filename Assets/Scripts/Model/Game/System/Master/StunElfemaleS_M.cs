using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Effect;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModel
    {
        internal void TryStunEnemyWithElfemaleM(in byte cell_from, in byte cell_to, in AbilityTypes abilityT, in Player sender)
        {
            if (!_eMG.UnitCooldownAbilitiesC(cell_from).HaveCooldown(abilityT))
            {
                if (_eMG.AdultForestC(cell_to).HaveAnyResources)
                {
                    if (_eMG.StepUnitC(cell_from).Steps >= StepValues.STUN_ELFEMALE)
                    {
                        if (!_eMG.UnitPlayerTC(cell_from).Is(_eMG.UnitPlayerTC(cell_to).PlayerT))
                        {
                            _eMG.StunUnitC(cell_to).Stun = StunValues.ELFEMALE;
                            _eMG.UnitCooldownAbilitiesC(cell_from).Set(abilityT, AbilityCooldownValues.NeedAfterAbility(abilityT));

                            _eMG.StepUnitC(cell_from).Steps -= StepValues.STUN_ELFEMALE;

                            _eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, abilityT);


                            foreach (var idx_1 in _eMG.AroundCellsE(cell_to).CellsAround)
                            {
                                if (_eMG.AdultForestC(idx_1).HaveAnyResources)
                                {
                                    if (_eMG.UnitTC(idx_1).HaveUnit && _eMG.UnitPlayerTC(idx_1).Is(_eMG.UnitPlayerTC(cell_to).PlayerT))
                                    {
                                        _eMG.StunUnitC(idx_1).Stun = StunValues.ELFEMALE;
                                    }
                                }
                            }
                        }
                    }

                    else _eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }

            else _sMG.ExecuteSoundActionToGeneral(sender, ClipTypes.Mistake);
        }
    }
}