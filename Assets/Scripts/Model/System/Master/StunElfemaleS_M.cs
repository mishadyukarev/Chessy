﻿using Chessy.Model.Values;
using Photon.Pun;
using Photon.Realtime;
namespace Chessy.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModel
    {
        internal void TryStunEnemyWithElfemaleM(in byte cell_from, in byte cell_to, in AbilityTypes abilityT, in Player sender)
        {
            if (!_e.UnitCooldownAbilitiesC(cell_from).HaveCooldown(abilityT))
            {
                if (_e.AdultForestC(cell_to).HaveAnyResources)
                {
                    if (_e.EnergyUnitC(cell_from).Energy >= StepValues.STUN_ELFEMALE)
                    {
                        if (!_e.UnitPlayerT(cell_from).Is(_e.UnitPlayerT(cell_to)))
                        {
                            _e.UnitEffectsC(cell_to).StunHowManyUpdatesNeedStay = StunValues.ELFEMALE;
                            _e.UnitCooldownAbilitiesC(cell_from).Set(abilityT, AbilityCooldownValues.NeedAfterAbility(abilityT));

                            _e.EnergyUnitC(cell_from).Energy -= StepValues.STUN_ELFEMALE;

                            _s.SoundToGeneral(RpcTarget.All, abilityT);


                            foreach (var idx_1 in _e.AroundCellsE(cell_to).CellsAround)
                            {
                                if (_e.AdultForestC(idx_1).HaveAnyResources)
                                {
                                    if (_e.UnitT(idx_1).HaveUnit() && _e.UnitPlayerT(idx_1).Is(_e.UnitPlayerT(cell_to)))
                                    {
                                        _e.UnitEffectsC(idx_1).StunHowManyUpdatesNeedStay = StunValues.ELFEMALE;
                                    }
                                }
                            }
                        }
                    }

                    else _s.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }

            else _s.ExecuteSoundActionToGeneral(sender, ClipTypes.Mistake);
        }
    }
}