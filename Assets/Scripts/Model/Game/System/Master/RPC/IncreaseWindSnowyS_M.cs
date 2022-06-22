using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModel
    {
        internal void IncreaseWindWithRainyM(in byte cell_0, in AbilityTypes abilityT, in Player sender, in bool needIncrese = true)
        {
            if (!_e.UnitCooldownAbilitiesC(cell_0).HaveCooldown(abilityT))
            {
                if (_e.StepUnitC(cell_0).Steps >= StepValues.Need(abilityT))
                {
                    if (needIncrese)
                    {
                        if (!_e.WeatherE.WindC.IsMaxSpeed())
                        {
                            _e.StepUnitC(cell_0).Steps -= StepValues.Need(abilityT);
                            _e.UnitCooldownAbilitiesC(cell_0).Set(abilityT, AbilityCooldownValues.NeedAfterAbility(abilityT));

                            _e.WeatherE.WindC.Speed++;

                            _s.SoundToGeneral(Photon.Pun.RpcTarget.All, AbilityTypes.ChangeDirectionWind);
                        }
                        else
                        {
                            _s.SimpleMistakeToGeneral(MistakeTypes.MaxSpeedWind, sender);
                        }
                    }

                    else
                    {
                        if (!_e.WeatherE.WindC.IsMinSpeed())
                        {
                            _e.StepUnitC(cell_0).Steps -= StepValues.Need(abilityT);
                            _e.UnitCooldownAbilitiesC(cell_0).Set(abilityT, AbilityCooldownValues.NeedAfterAbility(abilityT));

                            _e.WeatherE.WindC.Speed--;

                            _s.SoundToGeneral(Photon.Pun.RpcTarget.All, AbilityTypes.ChangeDirectionWind);
                        }
                        else
                        {
                            _s.SimpleMistakeToGeneral(MistakeTypes.MinSpeedWind, sender);
                        }
                    }

                }
                else
                {
                    _s.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }

        internal void DecreaseWindWithRainyM(in byte cell_0, in AbilityTypes abilityT, in Player sender)
        {
            IncreaseWindWithRainyM(cell_0, abilityT, sender, false);
        }
    }
}