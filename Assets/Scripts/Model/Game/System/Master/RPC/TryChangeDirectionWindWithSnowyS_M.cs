using Chessy.Game.Enum;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModel
    {
        internal void TryChangeDirectWindWithSnowyM(in byte cell_from, in byte idx_to, in AbilityTypes abilityT, in Player sender)
        {
            if (_e.StepUnitC(cell_from).Steps >= StepValues.Need(abilityT))
            {
                _e.WeatherE.WindC.DirectT = _e.AroundCellsE(_e.WeatherE.CloudC.Center).Direct(idx_to);
                _e.StepUnitC(cell_from).Steps -= StepValues.Need(abilityT);
                _e.UnitCooldownAbilitiesC(cell_from).Set(abilityT, AbilityCooldownValues.NeedAfterAbility(abilityT));

                _s.SoundToGeneral(RpcTarget.All, abilityT);

                if (_e.LessonT == LessonTypes.ChangeDirectionWind)
                {
                    _e.LessonT.SetNextLesson();
                }

            }

            else _s.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
        }
    }
}