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
            if (!eMG.UnitCooldownAbilitiesC(cell_0).HaveCooldown(abilityT))
            {
                if (eMG.StepUnitC(cell_0).Steps >= StepValues.Need(abilityT))
                {
                    if (needIncrese)
                    {
                        if (!eMG.WeatherE.WindC.IsMaxSpeed)
                        {
                            eMG.StepUnitC(cell_0).Steps -= StepValues.Need(abilityT);
                            eMG.UnitCooldownAbilitiesC(cell_0).Set(abilityT, AbilityCooldownValues.NeedAfterAbility(abilityT));

                            eMG.WeatherE.WindC.Speed++;

                            eMG.RpcPoolEs.SoundToGeneral(Photon.Pun.RpcTarget.All, AbilityTypes.ChangeDirectionWind);
                        }
                        else
                        {
                            eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.MaxSpeedWind, sender);
                        }
                    }

                    else
                    {
                        if (!eMG.WeatherE.WindC.IsMinSpeed)
                        {
                            eMG.StepUnitC(cell_0).Steps -= StepValues.Need(abilityT);
                            eMG.UnitCooldownAbilitiesC(cell_0).Set(abilityT, AbilityCooldownValues.NeedAfterAbility(abilityT));

                            eMG.WeatherE.WindC.Speed--;

                            eMG.RpcPoolEs.SoundToGeneral(Photon.Pun.RpcTarget.All, AbilityTypes.ChangeDirectionWind);
                        }
                        else
                        {
                            eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.MinSpeedWind, sender);
                        }
                    }

                }
                else
                {
                    eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}