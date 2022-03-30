using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.System.Model.Master
{
    sealed class IncreaseWindSnowyS_M : SystemModelGameAbs
    {
        internal IncreaseWindSnowyS_M(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Execute(in bool needIncrese, in byte cell_0, in AbilityTypes abilityT, in Player sender)
        {
            if (!e.UnitEs(cell_0).CoolDownC(abilityT).HaveCooldown)
            {
                if (e.UnitStepC(cell_0).Steps >= StepValues.Need(abilityT))
                {
                    if (needIncrese)
                    {
                        if (!e.WeatherE.WindC.IsMaxSpeed)
                        {
                            e.UnitStepC(cell_0).Steps -= StepValues.Need(abilityT);
                            e.UnitEs(cell_0).CoolDownC(abilityT).Cooldown = AbilityCooldownValues.NeedAfterAbility(abilityT);

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
                            e.UnitStepC(cell_0).Steps -= StepValues.Need(abilityT);
                            e.UnitEs(cell_0).CoolDownC(abilityT).Cooldown = AbilityCooldownValues.NeedAfterAbility(abilityT);

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