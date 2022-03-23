using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.System.Model
{
    public struct ChangeDirectionWindMS
    {
        public ChangeDirectionWindMS(in byte idx_from, in byte idx_to, in AbilityTypes abilityT, in Player sender, in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            if (e.UnitStepC(idx_from).Steps >= StepValues.Need(abilityT))
            {
                e.WeatherE.WindC.Direct = e.CellEs(e.WeatherE.CloudC.Center).Direct(idx_to);

                e.UnitStepC(idx_from).Steps -= StepValues.Need(abilityT);

                e.UnitEs(idx_from).CoolDownC(abilityT).Cooldown = AbilityCooldownValues.NeedAfterAbility(abilityT);

                e.RpcPoolEs.SoundToGeneral(RpcTarget.All, abilityT);
            }

            else e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
        }
    }
}