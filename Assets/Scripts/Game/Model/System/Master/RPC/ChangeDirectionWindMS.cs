using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.System.Model
{
    public sealed class ChangeDirectionWindMS : SystemModelGameAbs
    {
        public ChangeDirectionWindMS(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Change(in byte idx_from, in byte idx_to, in AbilityTypes abilityT, in Player sender)
        {
            if (eMGame.UnitStepC(idx_from).Steps >= StepValues.Need(abilityT))
            {
                eMGame.WeatherE.WindC.Direct = eMGame.CellEs(eMGame.WeatherE.CloudC.Center).Direct(idx_to);
                eMGame.UnitStepC(idx_from).Steps -= StepValues.Need(abilityT);
                eMGame.UnitEs(idx_from).CoolDownC(abilityT).Cooldown = AbilityCooldownValues.NeedAfterAbility(abilityT);

                eMGame.RpcPoolEs.SoundToGeneral(RpcTarget.All, abilityT);
            }

            else eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
        }
    }
}