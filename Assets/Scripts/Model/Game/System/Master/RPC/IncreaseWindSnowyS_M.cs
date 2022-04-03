using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.Model.System.Master
{
    sealed class IncreaseWindSnowyS_M : SystemModelGameAbs
    {
        internal IncreaseWindSnowyS_M(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Execute(in bool needIncrese, in byte cell_0, in AbilityTypes abilityT, in Player sender)
        {
            if (!eMG.UnitAbilityE(cell_0).HaveCooldown(abilityT))
            {
                if (eMG.UnitStepC(cell_0).Steps >= StepValues.Need(abilityT))
                {
                    if (needIncrese)
                    {
                        if (!eMG.WeatherE.WindC.IsMaxSpeed)
                        {
                            eMG.UnitStepC(cell_0).Steps -= StepValues.Need(abilityT);
                            eMG.UnitAbilityE(cell_0).Cooldown(abilityT) = AbilityCooldownValues.NeedAfterAbility(abilityT);

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
                            eMG.UnitStepC(cell_0).Steps -= StepValues.Need(abilityT);
                            eMG.UnitAbilityE(cell_0).Cooldown(abilityT) = AbilityCooldownValues.NeedAfterAbility(abilityT);

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