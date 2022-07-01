using Chessy.Model.Values;
using Photon.Realtime;
namespace Chessy.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModel
    {
        internal void IncreaseWindWithRainyM(in byte cell_0, in AbilityTypes abilityT, in Player sender, in bool needIncrese = true)
        {
            if (!_e.UnitCooldownAbilitiesC(cell_0).HaveCooldown(abilityT))
            {
                if (_e.EnergyUnitC(cell_0).Energy >= StepValues.Need(abilityT))
                {
                    if (needIncrese)
                    {
                        if (!_e.WeatherE.WindC.IsMaxSpeed())
                        {
                            _e.EnergyUnitC(cell_0).Energy -= StepValues.Need(abilityT);
                            _e.UnitCooldownAbilitiesC(cell_0).Set(abilityT, AbilityCooldownValues.NeedAfterAbility(abilityT));

                            _e.SpeedWind++;

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
                            _e.EnergyUnitC(cell_0).Energy -= StepValues.Need(abilityT);
                            _e.UnitCooldownAbilitiesC(cell_0).Set(abilityT, AbilityCooldownValues.NeedAfterAbility(abilityT));

                            _e.SpeedWind--;

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