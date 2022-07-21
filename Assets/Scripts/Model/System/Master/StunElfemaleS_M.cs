﻿using Chessy.Model.Enum;
using Chessy.Model.Values;
using Photon.Pun;
using Photon.Realtime;
namespace Chessy.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModelAbstract
    {
        internal void TryStunEnemyWithElfemaleM(in byte cell_from, in byte cell_to, in AbilityTypes abilityT, in Player sender)
        {
            if (!_cooldownAbilityCs[cell_from].HaveCooldown(abilityT))
            {
                if (_e.AdultForestC(cell_to).HaveAnyResources)
                {
                    if (_unitCs[cell_from].PlayerT != _unitCs[cell_to].PlayerT)
                    {
                        _effectsUnitCs[cell_to].StunHowManyUpdatesNeedStay = StunUnitValues.AMOUNT_STUN_AFTER_ABILITY_ELFEMALE;
                        _cooldownAbilityCs[cell_from].Set(abilityT, AbilityCooldownUnitValues.NeedAfterAbility(abilityT));

                        _s.RpcSs.SoundToGeneral(RpcTarget.All, abilityT);


                        foreach (var idx_1 in _e.IdxsCellsAround(cell_to))
                        {
                            if (_e.AdultForestC(idx_1).HaveAnyResources)
                            {
                                if (_e.UnitT(idx_1).HaveUnit() && _unitCs[idx_1].PlayerT == _unitCs[cell_to].PlayerT)
                                {
                                    _effectsUnitCs[idx_1].StunHowManyUpdatesNeedStay = StunUnitValues.AMOUNT_STUN_AFTER_ABILITY_ELFEMALE;
                                }
                            }
                        }
                    }
                }
            }

            else _s.RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.Mistake);
        }
    }
}