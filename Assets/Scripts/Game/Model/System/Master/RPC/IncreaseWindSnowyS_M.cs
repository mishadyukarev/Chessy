using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.System.Model.Master
{
    public sealed class IncreaseWindSnowyS_M : SystemModelGameAbs
    {
        public IncreaseWindSnowyS_M(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Execute(in bool needIncrese, in byte cell_0, in AbilityTypes abilityT, in Player sender)
        {
            if (!eMGame.UnitEs(cell_0).CoolDownC(abilityT).HaveCooldown)
            {
                if (eMGame.UnitStepC(cell_0).Steps >= StepValues.Need(abilityT))
                {
                    if (needIncrese)
                    {
                        if (!eMGame.WeatherE.WindC.IsMaxSpeed)
                        {
                            eMGame.UnitStepC(cell_0).Steps -= StepValues.Need(abilityT);
                            eMGame.UnitEs(cell_0).CoolDownC(abilityT).Cooldown = AbilityCooldownValues.NeedAfterAbility(abilityT);

                            eMGame.WeatherE.WindC.Speed++;

                            eMGame.RpcPoolEs.SoundToGeneral(Photon.Pun.RpcTarget.All, AbilityTypes.ChangeDirectionWind);
                        }
                        else
                        {
                            eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.MaxSpeedWind, sender);
                        }
                    }

                    else
                    {
                        if (!eMGame.WeatherE.WindC.IsMinSpeed)
                        {
                            eMGame.UnitStepC(cell_0).Steps -= StepValues.Need(abilityT);
                            eMGame.UnitEs(cell_0).CoolDownC(abilityT).Cooldown = AbilityCooldownValues.NeedAfterAbility(abilityT);

                            eMGame.WeatherE.WindC.Speed--;

                            eMGame.RpcPoolEs.SoundToGeneral(Photon.Pun.RpcTarget.All, AbilityTypes.ChangeDirectionWind);
                        }
                        else
                        {
                            eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.MinSpeedWind, sender);
                        }
                    }

                }
                else
                {
                    eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}