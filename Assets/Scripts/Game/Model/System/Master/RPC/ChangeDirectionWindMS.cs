using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.System.Model
{
    sealed class ChangeDirectionWindMS : SystemModelGameAbs
    {
        readonly CellEs _cellEs;

        internal ChangeDirectionWindMS(in CellEs cellEs, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _cellEs = cellEs;
        }

        internal void Change(in byte idx_to, in AbilityTypes abilityT, in Player sender)
        {
            if (_cellEs.UnitStatsE.StepC.Steps >= StepValues.Need(abilityT))
            {
                eMGame.WeatherE.WindC.Direct = eMGame.CellEs(eMGame.WeatherE.CloudC.Center).AroundCellsEs.Direct(idx_to);
                _cellEs.UnitStatsE.StepC.Steps -= StepValues.Need(abilityT);
                _cellEs.UnitEs.CoolDownC(abilityT).Cooldown = AbilityCooldownValues.NeedAfterAbility(abilityT);

                eMGame.RpcPoolEs.SoundToGeneral(RpcTarget.All, abilityT);
            }

            else eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
        }
    }
}