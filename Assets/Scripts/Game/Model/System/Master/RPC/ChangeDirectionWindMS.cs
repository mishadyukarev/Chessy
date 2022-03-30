using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.System.Model
{
    sealed class ChangeDirectionWindMS : SystemModelGameAbs
    {
        internal ChangeDirectionWindMS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Change(in byte cell_from, in byte idx_to, in AbilityTypes abilityT, in Player sender)
        {
            if (e.UnitStepC(cell_from).Steps >= StepValues.Need(abilityT))
            {
                e.WeatherE.WindC.Direct = e.CellEs(e.WeatherE.CloudC.Center).AroundCellsEs.Direct(idx_to);
                e.UnitStepC(cell_from).Steps -= StepValues.Need(abilityT);
                e.UnitEs(cell_from).CoolDownC(abilityT).Cooldown = AbilityCooldownValues.NeedAfterAbility(abilityT);

                e.RpcPoolEs.SoundToGeneral(RpcTarget.All, abilityT);
            }

            else e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
        }
    }
}