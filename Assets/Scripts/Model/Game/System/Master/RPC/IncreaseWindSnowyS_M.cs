using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModel
    {
        internal void IncreaseWindSnowyM(in bool needIncrese, in byte cell_0, in AbilityTypes abilityT, in Player sender)
        {
            if (!_eMG.UnitCooldownAbilitiesC(cell_0).HaveCooldown(abilityT))
            {
                if (_eMG.StepUnitC(cell_0).Steps >= StepValues.Need(abilityT))
                {
                    if (needIncrese)
                    {
                        if (!_eMG.WeatherE.WindC.IsMaxSpeed)
                        {
                            _eMG.StepUnitC(cell_0).Steps -= StepValues.Need(abilityT);
                            _eMG.UnitCooldownAbilitiesC(cell_0).Set(abilityT, AbilityCooldownValues.NeedAfterAbility(abilityT));

                            _eMG.WeatherE.WindC.Speed++;

                            _eMG.RpcPoolEs.SoundToGeneral(Photon.Pun.RpcTarget.All, AbilityTypes.ChangeDirectionWind);
                        }
                        else
                        {
                            _eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.MaxSpeedWind, sender);
                        }
                    }

                    else
                    {
                        if (!_eMG.WeatherE.WindC.IsMinSpeed)
                        {
                            _eMG.StepUnitC(cell_0).Steps -= StepValues.Need(abilityT);
                            _eMG.UnitCooldownAbilitiesC(cell_0).Set(abilityT, AbilityCooldownValues.NeedAfterAbility(abilityT));

                            _eMG.WeatherE.WindC.Speed--;

                            _eMG.RpcPoolEs.SoundToGeneral(Photon.Pun.RpcTarget.All, AbilityTypes.ChangeDirectionWind);
                        }
                        else
                        {
                            _eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.MinSpeedWind, sender);
                        }
                    }

                }
                else
                {
                    _eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}