using Chessy.Model.Enum;
using Chessy.Model.Values;
using Photon.Pun;
using Photon.Realtime;
namespace Chessy.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModelAbstract
    {
        internal void TryStunEnemyWithElfemaleM(in byte cell_from, in byte cell_to, in AbilityTypes abilityT, in Player sender)
        {
            if (!_e.UnitCooldownAbilitiesC(cell_from).HaveCooldown(abilityT))
            {
                if (_e.AdultForestC(cell_to).HaveAnyResources)
                {
                    //if (_e.EnergyUnitC(cell_from).Energy >= StepValues.STUN_ELFEMALE)
                    //{
                        if (!_e.UnitPlayerT(cell_from).Is(_e.UnitPlayerT(cell_to)))
                        {
                            _e.UnitEffectsC(cell_to).StunHowManyUpdatesNeedStay = StunUnitValues.AMOUNT_STUN_AFTER_ABILITY_ELFEMALE;
                            _e.UnitCooldownAbilitiesC(cell_from).Set(abilityT, AbilityCooldownUnitValues.NeedAfterAbility(abilityT));

                            //_e.EnergyUnitC(cell_from).Energy -= StepValues.STUN_ELFEMALE;

                            _s.RpcSs.SoundToGeneral(RpcTarget.All, abilityT);


                            foreach (var idx_1 in _e.IdxsCellsAround(cell_to, DistanceFromCellTypes.First))
                            {
                                if (_e.AdultForestC(idx_1).HaveAnyResources)
                                {
                                    if (_e.UnitT(idx_1).HaveUnit() && _e.UnitPlayerT(idx_1).Is(_e.UnitPlayerT(cell_to)))
                                    {
                                        _e.UnitEffectsC(idx_1).StunHowManyUpdatesNeedStay = StunUnitValues.AMOUNT_STUN_AFTER_ABILITY_ELFEMALE;
                                    }
                                }
                            }
                        }
                    //}

                    //else _s.RpcSs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }

            else _s.RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.Mistake);
        }
    }
}