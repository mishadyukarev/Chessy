using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.System.Model.Master
{
    public struct IncreaseWindSnowyS_M
    {
        public void Execute(in bool needIncrese, in byte idx_0, in AbilityTypes abilityT, in Player sender, in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            if (!e.UnitEs(idx_0).CoolDownC(abilityT).HaveCooldown)
            {
                if (e.UnitStepC(idx_0).Steps >= StepValues.Need(abilityT))
                {
                    if (needIncrese)
                    {
                        if (!e.WeatherE.WindC.IsMaxSpeed)
                        {
                            e.UnitStepC(idx_0).Steps -= StepValues.Need(abilityT);
                            e.UnitEs(idx_0).CoolDownC(abilityT).Cooldown = AbilityCooldownValues.NeedAfterAbility(abilityT);

                            e.WeatherE.WindC.Speed++;

                            e.RpcPoolEs.SoundToGeneral(Photon.Pun.RpcTarget.All, AbilityTypes.ChangeDirectionWind);
                        }
                        else
                        {
                            e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.MaxSpeedWind, sender);
                        }
                    }

                    else
                    {
                        if (!e.WeatherE.WindC.IsMinSpeed)
                        {
                            e.UnitStepC(idx_0).Steps -= StepValues.Need(abilityT);
                            e.UnitEs(idx_0).CoolDownC(abilityT).Cooldown = AbilityCooldownValues.NeedAfterAbility(abilityT);

                            e.WeatherE.WindC.Speed--;

                            e.RpcPoolEs.SoundToGeneral(Photon.Pun.RpcTarget.All, AbilityTypes.ChangeDirectionWind);
                        }
                        else
                        {
                            e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.MinSpeedWind, sender);
                        }
                    }

                }
                else
                {
                    e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}